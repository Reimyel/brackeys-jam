using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviourScript : MonoBehaviour
{
    #region Vari�veis Globais
    [Header("Configura��es:")]
    [SerializeField] private float minRot, maxRot;
    [SerializeField] private float minVel, maxVel;
    [SerializeField] private Color fadeColor;

    [Header("Refer�ncias:")]
    [SerializeField] private FadeVFX fadePrefab;

    private float _rotationSpeed;
    private float _velocity;
    private Rigidbody2D _rb;
    #endregion

    #region Fun��es Unity
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _velocity = Random.Range(minVel, maxVel);

        _rb.AddForce(-transform.right * _velocity);

        StartCoroutine(ApplyEffect(0.01f));

        Destroy(gameObject, 8f);
    }

    private void Update()
    {
        _rotationSpeed = Random.Range(minRot, maxRot);

        transform.Rotate(0, 0, _rotationSpeed);
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

        // Colocando a refer�ncia para rota��o
        effect.GetComponent<FadeVFX>().RotationParent = gameObject.transform;

        StartCoroutine(ApplyEffect(0.01f));
    }
    #endregion
}
