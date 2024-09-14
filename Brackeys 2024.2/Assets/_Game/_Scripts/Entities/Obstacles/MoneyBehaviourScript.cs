using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyBehaviourScript : MonoBehaviour
{
    #region Variáveis Globais
    [SerializeField] private float minVel, maxVel;
    [SerializeField] private int layerPlayer;
    private float _velocity;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private BalloonCollision _balloonCollision;

    private Vector2 _direction;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _balloonCollision = FindObjectOfType<BalloonCollision>();

        _velocity = Random.Range(minVel, maxVel);

        _rb.AddForce(_direction * _velocity);

        if (AudioManager.Instance != null)
        {
            if (gameObject.CompareTag("Cow"))
                AudioManager.Instance.PlaySFX("Cow" + Random.Range(1, 6));
            else if (gameObject.CompareTag("Car"))
                AudioManager.Instance.PlaySFX("Carro");
            else if (gameObject.CompareTag("Chicken"))
            {
                if (!ChickenCowManager.IsChickenMoment && Random.Range(0, 100) < 45)
                    AudioManager.Instance.PlaySFX("Galinha");

                AudioManager.Instance.PlaySFX("Galinha" + Random.Range(1, 3));
            }
        }

        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == layerPlayer)
        {
            BalloonStats.CurrentMoney += 1;
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    #endregion
}
