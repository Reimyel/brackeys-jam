using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonCollision : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private string upgradeScene;

    [Header("Layers:")]
    [SerializeField] private int layerObstacle;
    [SerializeField] private int layerChangeSide;

    [Header("Trocando Lados:")]
    [SerializeField] private float changeSideForce;
    [SerializeField] private Transform leftSidePoint;
    [SerializeField] private Transform rightSidePoint;
    [SerializeField] private float resetCanMoveInterval;

    // Componentes:
    private Rigidbody2D _rb;
    private BalloonMovement _balloonMovement;

    private int _initialDurability;
    #endregion

    #region Funções Unity
    private void Awake() => _initialDurability = BalloonStats.Durability;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _balloonMovement = GetComponent<BalloonMovement>();
    }
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.layer == layerObstacle)
            ReduceDurability(col.gameObject.GetComponent<ObstacleBehaviourScript>().BallonDamage);
        else if (col.gameObject.layer == layerChangeSide)
            ChangeSide(col.gameObject.tag);
    }
    #endregion

    #region Funções Próprias
    private void ReduceDurability(int damage) 
    {
        var newValue = BalloonStats.Durability - damage;

        if (newValue <= 0) 
        {
            // GameOver
            // Balão Parar
            // Parar Música
            // Efeito Sonoro do Balão Murchando
            // Tela Escurecer
            BalloonStats.Durability = _initialDurability;
            _rb.velocity = Vector2.zero;
            _balloonMovement.CanMove = false;
            SceneManager.LoadScene(upgradeScene);
        }
        else 
        {
            BalloonStats.Durability -= damage;   
        }
    }

    private void ChangeSide(string tag) 
    {
        _balloonMovement.CanMove = false;
        _rb.velocity = Vector2.zero;
        Invoke("ResetCanMove", resetCanMoveInterval);

        if (tag == "LeftSide") 
        {
            gameObject.transform.position = rightSidePoint.position;
            _rb.AddForce(Vector2.left * changeSideForce, ForceMode2D.Impulse);
        }
        else // RightSide
        {
            gameObject.transform.position = leftSidePoint.position;
            _rb.AddForce(Vector2.right * changeSideForce, ForceMode2D.Impulse);
        }
    }

    private void ResetCanMove() => _balloonMovement.CanMove = true;
    #endregion
}
