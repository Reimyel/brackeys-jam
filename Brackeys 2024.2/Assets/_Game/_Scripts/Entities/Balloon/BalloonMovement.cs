using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    #region Variáveis
    [Header("Configuração:")]

    [Header("Movimentação:")]
    //[SerializeField] private float upSpeed = 8f;
    //[SerializeField] private float horizontalSpeed;
    [SerializeField] private float tiltAmount = 15f;
    private float tiltSpeed = 5f;
    private float targetRotation = 0f; //Rotação Atual
    private float dirX;

    // Variáveis do vento
    [Header("Ventania")]
    public float minInterval = 3f;
    public float maxInterval = 5f;
    public float windForce = 10f;
    public float windDuration = 3f;
    private float windDirection = 0f;

    [Header("Referências:")]
    [SerializeField] private Transform chickenTransform;
    [SerializeField] private GameObject windAlert;
    [SerializeField] private Rigidbody2D rbBasket;
    [SerializeField] private SpriteRenderer playerHeadSpr;

    // Referências:
    private Animator _chickenAnimator;

    // Componentes:
    private Rigidbody2D _rb;

    private float windNewDirection = 0;

    [HideInInspector] public bool CanMove = true;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        if (BalloonStats.HasChicken)
        {
            chickenTransform.gameObject.SetActive(true);
            _chickenAnimator = chickenTransform.gameObject.GetComponent<Animator>();

            if (Random.Range(0, 100) < 50)
                _chickenAnimator.Play("Chicken Turning Right Animation");
            else
                _chickenAnimator.Play("Chicken Turning Left Animation");
        }
    }

    private void Start()
    {
        var newWindTime = Random.Range(minInterval, maxInterval);
        var alertTime = newWindTime - 1.5f;

        Invoke("SetWindAlert", alertTime);
        Invoke("SetWind", Random.Range(minInterval, maxInterval));
        windAlert.gameObject.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();

        //horizontalSpeed = BalloonStats.Speed;
    }

    private void Update()
    {

        MoveInput();
        FlipHead();

        if (!CanMove) return;

        SetRotation();
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;
        
        ApplyMove();
    }
    #endregion

    #region Funções Próprias
    /*
    IEnumerator WindEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(windInterval);
            // Determina uma direção aleatória para a ventania (-1 para esquerda, 1 para direita)
            windDirection = Random.Range(0, 2) == 0 ? -windForce : windForce;

            if (_chickenAnimator != null)
                SetNewChickenRotationY(Mathf.Sign(windDirection));
            // Aplica ventania por um tempo
            yield return new WaitForSeconds(windDuration);

            // Para a ventania após a duração
            windDirection = 0f;
            //Da um novo intervalo
            windInterval = Random.Range(minInterval, maxInterval);

            windAlert.gameObject.SetActive(true);
            StartCoroutine(StopWindAlert(windInterval - 2f));
            StartCoroutine(WindEffect());
        }
    }

    private IEnumerator StopWindAlert(float interval) 
    {
        yield return new WaitForSeconds(interval);
        windAlert.gameObject.SetActive(false);
    }
    */

    private void MoveInput() => dirX = Input.GetAxisRaw("Horizontal");

    private void SetRotation() 
    {
        if (dirX > 0)
        {
            targetRotation = -tiltAmount;
        }
        else if (dirX < 0)
        {
            targetRotation = tiltAmount;
        }
        else
        {
            targetRotation = 0f;
        }

        //Faz a girada
        float currentRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, Time.deltaTime * tiltSpeed);

        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    private void ApplyMove() 
    {
        /*Faz a subida
        _rb.velocity = new Vector2(0, upSpeed);
        */

        var inputMove = dirX * BalloonStats.Speed;
        var windMove = windDirection * windForce / BalloonStats.Stability;

        transform.position += inputMove * Vector3.right * Time.fixedDeltaTime * 1.5f;
        transform.position += windMove * Vector3.right * Time.fixedDeltaTime * 0.75f;
    }

    private void SetWindAlert()
    {
        if (Random.Range(0, 100) < 50) windNewDirection = 1;
        else windNewDirection = -1;

        SetNewChickenRotationY(windNewDirection);

        windAlert.gameObject.SetActive(true);
    }
    
    private void SetWind() 
    {
        windDirection = windNewDirection;
        
        windAlert.gameObject.SetActive(false);

        Invoke("StopWind", windDuration);
    }

    private void StopWind() 
    {
        windDirection = 0f;

        var newWindTime = Random.Range(minInterval, maxInterval);
        var alertTime = newWindTime - 1.5f;

        Invoke("SetWindAlert", alertTime);
        Invoke("SetWind", Random.Range(minInterval, maxInterval));
    }

    private void SetNewChickenRotationY(float dir) 
    {
        if (!BalloonStats.HasChicken) return;

        if (dir > 0)
            _chickenAnimator.Play("Chicken Turning Right Animation");       
        else
            _chickenAnimator.Play("Chicken Turning Left Animation");
    }

    private void FlipHead() 
    {
        if (dirX < 0)
            playerHeadSpr.flipX = true;
        else
            playerHeadSpr.flipX = false;
    }
    #endregion
}

