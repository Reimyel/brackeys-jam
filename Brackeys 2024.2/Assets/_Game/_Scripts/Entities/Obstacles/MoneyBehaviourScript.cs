using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyBehaviourScript : MonoBehaviour
{
    #region Variáveis Globais
    [SerializeField] private int layerPlayer;
    [SerializeField] private float velocity;
    private Rigidbody2D _rb;

    private Vector2 _direction;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.AddForce(_direction * velocity);

        AudioManager.Instance.PlaySFX("Money get");

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
