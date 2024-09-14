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

        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == layerPlayer)
        {
            AudioManager.Instance.PlaySFX("Money get");
            Debug.Log(BalloonStats.CurrentMoney);
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    #endregion
}
