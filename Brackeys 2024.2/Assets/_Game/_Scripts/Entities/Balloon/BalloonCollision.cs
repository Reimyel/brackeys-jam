using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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

    [Header("Shake:")]
    [SerializeField] private float hitShakeIntensity;
    [SerializeField] private float hitShakeInterval;
    [SerializeField] private float gameOverShakeIntensity;
    [SerializeField] private float gameOverShakeInterval;

    [Header("Transição Game Over:")]
    [SerializeField] private Color damageColor;
    [SerializeField] private string upgradeSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Referências:")]
    [SerializeField] private Rigidbody2D rbBasket;
    [SerializeField] private SpriteRenderer sprDurability;
    [SerializeField] private SpriteRenderer sprDamageDurability;
    [SerializeField] private Animator animGameOver;
    [SerializeField] private Sprite[] spritesDamageDurability;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private Transform weatherChickenTransform;
    [SerializeField] private GameObject movementTrail;
    
    // Componentes:
    private Rigidbody2D _rb;

    public static int InitialDurability;

    public bool _IsGameOver = false;

    private BalloonMovement _balloonMovement;

    private bool _startToFall = false;
    #endregion

    #region Funções Unity
    private void Start()
    {
        InitialDurability = BalloonStats.Durability;
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
        // N�o detecctar mais nada, caso estiver no Game Over
        if (_IsGameOver) return;

        if (col.gameObject.layer == layerObstacle)
        {
            ReduceDurability(col.gameObject.GetComponent<ObstacleBehaviourScript>().BalloonDamage);
            StartCoroutine(Blink());
        }
        else if (col.gameObject.layer == layerChangeSide)
            ChangeSide(col.gameObject.tag);
        else if (col.gameObject.layer == layerMoney && !_IsGameOver)
            BalloonStats.CurrentMoney += 1;
    }
    #endregion

    #region Funções Próprias
    public void ReduceDurability(int damage) 
    {
        var newValue = BalloonStats.Durability - damage;

        if (newValue <= 0) 
        {
            // Parar efeito de rastro de movimentação
            movementTrail.SetActive(false);
            
            // Galinha Cair
            if (BalloonStats.HasChicken) 
            {
                var chickenRb = weatherChickenTransform.gameObject.GetComponent<Rigidbody2D>();
                chickenRb.simulated = true;
                weatherChickenTransform.Rotate(Vector3.forward * 45f);
            }

            cameraShake.ApplyShake(gameOverShakeIntensity, gameOverShakeInterval);
            sprDurability.gameObject.SetActive(false);
            sprDamageDurability.gameObject.SetActive(false);
            animGameOver.gameObject.SetActive(true);
            //animGameOver.speed = 0f;
            animGameOver.Play("Game Over " + BalloonStats.DurabilityLevel + " Animation");
            // GameOver
            _IsGameOver = true;
            
            // Bal�o Parar
            _balloonMovement.enabled = false;
            GetComponent<FuelBehaviourScript>().enabled = false;

            // Parar Música
            Destroy(GameObject.FindGameObjectWithTag("Music"));

            // Efeito Sonoro do Bal�o Murchando
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Death");


            // Bal�o Cair
            Invoke("UnfreezeY", 2.75f);


            // Mais SFXs 
            Invoke("StartToScream", 2.25f);

            // Cair e Come�a GameOver
            Invoke("StartToFall", 2.75f);
        }
        else 
        {
            cameraShake.ApplyShake(hitShakeIntensity, hitShakeInterval);
            BalloonStats.Durability -= damage;

            if (BalloonStats.DurabilityLevel > 0 && BalloonStats.Durability - 1 <= 0)
                sprDurability.color = damageColor;
            else 
            {
                sprDurability.gameObject.SetActive(false);

                if (BalloonStats.HasChicken)
                    weatherChickenTransform.localPosition = new Vector3(weatherChickenTransform.localPosition.x, 9.24f, weatherChickenTransform.localPosition.z);

                sprDamageDurability.sprite = spritesDamageDurability[BalloonStats.DurabilityLevel];
            }

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

        //animGameOver.speed = 1f;
    }

    private void StartToFall() 
    {
        _rb.gravityScale = 4f;

        if (AudioManager.Instance != null) 
            AudioManager.Instance.PlaySFX("Fall");

        // Tela Escurecer
        ExitGameOver();
    }

    private void ExitGameOver() => TransitionManager.Instance().Transition(upgradeSceneName, transitionSettings, gameOverInterval);
    #endregion
}
