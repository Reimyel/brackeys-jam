using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [Header("Configuração:")]

    [Header("Movimentação:")]
    [SerializeField] private float upSpeed = 8f;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float tiltAmount = 15f;
    private float tiltSpeed = 5f;
    private float targetRotation = 0f; //Rotação Atual
    private float dirX;

    // Variáveis do vento
    [Header("Ventania")]
    private float windInterval = 5f;
    public float minInterval = 3f;
    public float maxInterval = 5f;
    public float windForce = 10f; 
    public float windDuration = 3f; 
    private float windDirection = 0f;

    [Header("Referências:")]
    [SerializeField] private Transform chickenTransform;

    // Referências:
    private Animator _chickenAnimator;

    // Componentes:
    private Rigidbody2D _rb;

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
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WindEffect());

        horizontalSpeed = BalloonStats.Speed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            targetRotation = -tiltAmount; 
        }
        
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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

    private void FixedUpdate()
    {
        //Faz a subida
        _rb.velocity = new Vector2(0, upSpeed);

        dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(dirX * horizontalSpeed + windDirection * BalloonStats.Stability, _rb.velocity.y);
 
    }

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
            StartCoroutine(WindEffect());
        }
    }

    private void SetNewChickenRotationY(float dir) 
    {
        if (dir > 0)
            _chickenAnimator.Play("Chicken Turning Right Animation");       
        else
            _chickenAnimator.Play("Chicken Turning Left Animation");
    }
}

