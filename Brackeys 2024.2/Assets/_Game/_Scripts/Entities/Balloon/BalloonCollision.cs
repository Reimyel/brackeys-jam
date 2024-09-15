using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class BalloonCollision : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private float gameOverInterval;

    [Header("Layers:")]
    [SerializeField] private int layerObstacle;
    [SerializeField] private int layerChangeSide;
    [SerializeField] private int layerMoney;

    [Header("Trocando Lados:")]
    [SerializeField] private float changeSideForce;
    [SerializeField] private Transform leftSidePoint;
    [SerializeField] private Transform rightSidePoint;
    [SerializeField] private float resetCanMoveInterval;

    [Header("Transição Game Over:")]
    [SerializeField] private string upgradeSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Referências:")]
    [SerializeField] private Rigidbody2D rbBasket;

    // Componentes:
    private Rigidbody2D _rb;

    private int _initialDurability;

    public bool _IsGameOver = false;

    private BalloonMovement _balloonMovement;

    private bool _startToFall = false;
    #endregion

    #region Funções Unity
    private void Awake() => _initialDurability = BalloonStats.Durability;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _balloonMovement = GetComponent<BalloonMovement>();
    }

    private void FixedUpdate()
    {
        if (_startToFall)
            transform.position += 25f * Vector3.down * Time.fixedDeltaTime * 1.5f;
    }


    private void OnCollisionEnter2D(Collision2D col) 
    {
        // Não detecctar mais nada, caso estiver no Game Over
        if (_IsGameOver) return;

        if (col.gameObject.layer == layerObstacle)
        {
            ReduceDurability(col.gameObject.GetComponent<ObstacleBehaviourScript>().BalloonDamage);
            StartCoroutine(Blink());
        }
        //else if (col.gameObject.layer == layerChangeSide)
            //ChangeSide(col.gameObject.tag);
        else if (col.gameObject.layer == layerMoney)
            BalloonStats.CurrentMoney += 1;
    }
    #endregion

    #region Funções Próprias
    public void ReduceDurability(int damage) 
    {
        var newValue = BalloonStats.Durability - damage;

        if (newValue <= 0) 
        {
            // GameOver
            _IsGameOver = true;
            
            // Balão Parar
            _balloonMovement.enabled = false;

            // Parar Música
            Destroy(GameObject.FindGameObjectWithTag("Music"));

            // Efeito Sonoro do Balão Murchando
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Death");


            // Balão Cair
            Invoke("UnfreezeY", 4.75f);


            // Mais SFXs 
            Invoke("StartToScream", 4.25f);

            // Cair e Começa GameOver
            Invoke("StartToFall", 4.75f);
        }
        else 
        {
            BalloonStats.Durability -= damage;


            if (AudioManager.Instance != null) 
            {
                AudioManager.Instance.PlaySFX("Dano" + Random.Range(1, 5));

                if (Random.Range(0, 100) < 50)
                    AudioManager.Instance.PlaySFX("Hitsound");
                else
                    AudioManager.Instance.PlaySFX("Damage");
            }
        }
    }

    void UnfreezeY()
    {
        rbBasket.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.None;
        _startToFall = true;
    }

    private IEnumerator Blink()
    {
        int blinkTimes = 3;
        float blinkDuration = 0.1f;

        for (int i = 0; i < blinkTimes; i++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkDuration);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void ChangeSide(string tag) 
    {
        if (tag == "LeftSide") 
        {
            gameObject.transform.position = rightSidePoint.position;
            //_rb.AddForce(Vector2.left * changeSideForce, ForceMode2D.Impulse);
        }
        else // RightSide
        {
            gameObject.transform.position = leftSidePoint.position;
            //_rb.AddForce(Vector2.right * changeSideForce, ForceMode2D.Impulse);
        }
    }

    private void ResetCanMove() => _balloonMovement.CanMove = true;

    private void StartToScream()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Morte");
    }

    private void StartToFall() 
    {
        _rb.gravityScale = 4f;

        if (AudioManager.Instance != null) 
            AudioManager.Instance.PlaySFX("Fall");

        // Tela Escurecer
        ExitGameOver();
    }

    private void ExitGameOver()
    {
        BalloonStats.Durability = _initialDurability;
        TransitionManager.Instance().Transition(upgradeSceneName, transitionSettings, gameOverInterval);
    }
    #endregion
}
