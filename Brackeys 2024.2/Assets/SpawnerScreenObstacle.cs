using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScreenObstacle : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;

    [Header("Referências:")]
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private Transform minPoint;
    [SerializeField] private Transform maxPoint;
    #endregion

    #region Funções Unity
    private void Start() => Invoke("Spawn", Random.Range(minSpawnInterval, maxSpawnInterval));
    #endregion

    #region Funções Próprias
    private void Spawn() 
    {
        var spawnPos = new Vector3(Random.Range(minPoint.position.x, maxPoint.position.x), Random.Range(minPoint.position.y, maxPoint.position.y), 0f);
        var initialRotation = Quaternion.Euler(45f, 45f, 0f);
        Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPos, initialRotation);

        Invoke("Spawn", Random.Range(minSpawnInterval, maxSpawnInterval));
    }
    #endregion
}
