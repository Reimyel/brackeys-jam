using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviourScript : MonoBehaviour
{
    #region Vari�veis Globais
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _velocity;
    //[SerializeField] private float minRot, maxRot;
    //[SerializeField] private float minVel, maxVel;
    private Rigidbody2D _rb;
    #endregion

    #region Fun��es Unity
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.AddForce(-transform.right * _velocity);
    }

    void Update()
    {
        //_rotationSpeed = Random.Range(minRot, maxRot);
        //_velocity = Random.Range(minVel, maxVel);
        transform.Rotate(0, 0, _rotationSpeed);
    }
    #endregion

    #region Fun��es Pr�prias
    
    #endregion
}
