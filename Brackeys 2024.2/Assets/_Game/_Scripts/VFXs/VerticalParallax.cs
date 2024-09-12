using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    [Header("Configurações:")]
    [SerializeField] private float verticalSpeed;

    // Componentes:
    private Rigidbody2D _rb;

    private Vector3 _startPos;

    private void Start() => _startPos = gameObject.transform.position;
    
    private void Update() => transform.position += verticalSpeed * Vector3.down * Time.deltaTime;

    private void OnTriggerEnter2D(Collider2D collision) => gameObject.transform.position = _startPos;
}