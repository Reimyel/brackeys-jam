using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviourScript : MonoBehaviour
{
    #region Variáveis Globais
    [Header("Configurações:")]
    [SerializeField] public int BalloonDamage;
    [SerializeField] private float jumpForce;
    [SerializeField] private float minRot, maxRot;
    [SerializeField] private float minVel, maxVel;
    [SerializeField] private Color fadeColor;

    [Header("Referências:")]
    [SerializeField] private BalloonCollision balloonCollision;
    [SerializeField] private int layerPlayer;
    [SerializeField] private FadeVFX fadePrefab;

    private float _rotationSpeed;
    private float _velocity;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private Vector2 _direction;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _velocity = Random.Range(minVel, maxVel);

        _rb.AddForce(_direction * _velocity);

        StartCoroutine(ApplyEffect(0.01f));

        if (AudioManager.Instance != null) 
        {
            if (gameObject.CompareTag("Cow"))
                AudioManager.Instance.PlaySFX("Cow" + Random.Range(1, 6));
            else if (gameObject.CompareTag("Car"))
                AudioManager.Instance.PlaySFX("Carro");
            else if (gameObject.CompareTag("Chicken")) 
            {
                if (!ChickenBehaviourScript.IsChickenMoment  && Random.Range(0, 100) < 45)
                    AudioManager.Instance.PlaySFX("Galinha");

                AudioManager.Instance.PlaySFX("Galinha" + Random.Range(1, 3));
            }
        }

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        _rotationSpeed = Random.Range(minRot, maxRot);

        transform.Rotate(0, 0, _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == layerPlayer)
        {
            _rb.AddForce(transform.up * jumpForce);

            _collider.enabled = false;
            _rb.isKinematic = false;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private IEnumerator ApplyEffect(float t) 
    {
        yield return new WaitForSeconds(t);
        var effect = Instantiate(fadePrefab, transform.position, Quaternion.identity);
        var effectSpr = effect.GetComponent<SpriteRenderer>();

        // Configurando Sprite do Fade
        effectSpr.sprite = GetComponent<SpriteRenderer>().sprite;
        effectSpr.color = fadeColor;
        effectSpr.gameObject.transform.localScale = gameObject.transform.localScale;

        // Colocando a referência para rotação
        effect.GetComponent<FadeVFX>().RotationParent = gameObject.transform;

        StartCoroutine(ApplyEffect(0.01f));
    }

    private void OnDestroy()
    {
        if (!balloonCollision._IsGameOver)
        {
            //TESTAR
            if (gameObject.CompareTag("Cow") && AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Vaca");
        }
    }
    #endregion
}
