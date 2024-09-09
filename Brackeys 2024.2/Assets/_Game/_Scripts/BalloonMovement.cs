using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField] private int upSpeed = 8;
    [SerializeField] private int horizontalSpeed = 10;
    [SerializeField] private float tiltAmount = 15f;
    private float tiltSpeed = 5f;
    private float targetRotation = 0f; //Rota��o Atual
    private float dirX;
    //Vari�veis do vento
    private float windInterval = 5f;
    public float windForce = 10f; 
    public float windDuration = 3f; 
    private float windDirection = 0f;
    //Component
    private Rigidbody2D _rb;

    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WindEffect());
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
        _rb.velocity = new Vector2(dirX * horizontalSpeed + windDirection, _rb.velocity.y);
 
    }

    IEnumerator WindEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(windInterval);

            // Determina uma dire��o aleat�ria para a ventania (-1 para esquerda, 1 para direita)
            windDirection = Random.Range(0, 2) == 0 ? -windForce : windForce;

            // Aplica ventania por um tempo
            yield return new WaitForSeconds(windDuration);

            // Para a ventania ap�s a dura��o
            windDirection = 0f;
            //Da um novo intervalo
            windInterval = Random.Range(3f, 6f);
        }
    }
}

