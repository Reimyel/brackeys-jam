using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonCollision : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private float gameOverInterval;

    [Header("Layers:")]
    [SerializeField] private int layerObstacle;
    [SerializeField] private int layerChangeSide;

    [Header("Trocando Lados:")]
    [SerializeField] private float changeSideForce;
    [SerializeField] private Transform leftSidePoint;
    [SerializeField] private Transform rightSidePoint;
    [SerializeField] private float resetCanMoveInterval;

    [Header("Transição Game Over:")]
    [SerializeField] private string upgradeSceneName;
    [SerializeField] private TransitionSettings transitionSettings;

    // Componentes:
    private Rigidbody2D _rb;
    private BalloonMovement _balloonMovement;

    private int _initialDurability;

    private bool _IsGameOver = false;
    #endregion

    #region Funções Unity
    private void Awake() => _initialDurability = BalloonStats.Durability;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _balloonMovement = GetComponent<BalloonMovement>();
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        // Não detecctar mais nada, caso estiver no Game Over
        if (_IsGameOver) return;

        if (col.gameObject.layer == layerObstacle)
            ReduceDurability(col.gameObject.GetComponent<ObstacleBehaviourScript>().BallonDamage);
        else if (col.gameObject.layer == layerChangeSide)
            ChangeSide(col.gameObject.tag);
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
            _rb.velocity = Vector2.zero;


            // Mais SFXs 
            Invoke("StartToScream", 4.25f);

            // Cair e Começa GameOver
            Invoke("StartToFall", 6.25f);
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

    private void ChangeSide(string tag) 
    {
        _balloonMovement.CanMove = false;
        _rb.velocity = Vector2.zero;
        Invoke("ResetCanMove", resetCanMoveInterval);

        if (tag == "LeftSide") 
        {
            gameObject.transform.position = rightSidePoint.position;
            _rb.AddForce(Vector2.left * changeSideForce, ForceMode2D.Impulse);
        }
        else // RightSide
        {
            gameObject.transform.position = leftSidePoint.position;
            _rb.AddForce(Vector2.right * changeSideForce, ForceMode2D.Impulse);
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
