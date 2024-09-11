using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScreenObstacle : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;

    [Header("Refer�ncias:")]
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private Transform minPoint;
    [SerializeField] private Transform maxPoint;
    #endregion

    #region Fun��es Unity
    private void Start() => Invoke("Spawn", Random.Range(minSpawnInterval, maxSpawnInterval));
    #endregion

    #region Fun��es Pr�prias
    private void Spawn() 
    {
        var spawnPos = new Vector3(Random.Range(minPoint.position.x, maxPoint.position.x), Random.Range(minPoint.position.y, maxPoint.position.y), 0f);
        var initialRotation = Quaternion.Euler(45f, 45f, 0f);
        Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPos, initialRotation);

        Invoke("Spawn", Random.Range(minSpawnInterval, maxSpawnInterval));
    }
    #endregion
}
