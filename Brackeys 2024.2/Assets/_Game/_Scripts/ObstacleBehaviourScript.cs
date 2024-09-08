using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviourScript : MonoBehaviour
{
    #region Variáveis Globais
    [SerializeField] private float minRot, maxRot;
    [SerializeField] private float minVel, maxVel;
    private float _rotationSpeed;
    private float _velocity;
    private Rigidbody2D _rb;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _velocity = Random.Range(minVel, maxVel);

        _rb.AddForce(-transform.right * _velocity);
    }

    void Update()
    {
        _rotationSpeed = Random.Range(minRot, maxRot);

        transform.Rotate(0, 0, _rotationSpeed);

        Destroy(gameObject, 8f);
    }
    #endregion
}
