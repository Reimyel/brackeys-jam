using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonCollision : MonoBehaviour
{
    [Header("Configura��es:")]
    [SerializeField] private string upgradeScene;
    [SerializeField] private int layerObstacle;

    private int _initialDurability;

    private void Awake() => _initialDurability = BalloonStats.Durability;

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.layer == layerObstacle) 
        {
            ReduceDurability(col.gameObject.GetComponent<ObstacleBehaviourScript>().BallonDamage);
        }
    }

    private void ReduceDurability(int damage) 
    {
        var newValue = BalloonStats.Durability - damage;

        if (newValue <= 0) 
        {
            // GameOver
            // Bal�o Parar
            // Parar M�sica
            // Efeito Sonoro do Bal�o Murchando
            // Tela Escurecer
            BalloonStats.Durability = _initialDurability;
            SceneManager.LoadScene(upgradeScene);
        }
        else 
        {
            BalloonStats.Durability -= damage;   
        }
    }
}
