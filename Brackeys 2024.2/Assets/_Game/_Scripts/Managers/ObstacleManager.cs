using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    #region Vari�veis Globais
    [SerializeField] private float windForce;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] public Transform[] spawnPoint;
    #endregion

    #region Fun��es Unity
    private void Start() => StartCoroutine(SpawnNextObstacle(minTime, maxTime));
    #endregion

    #region Fun��es Pr�prias
    private IEnumerator SpawnNextObstacle(float minTime, float maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        var obstacle = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];

        Instantiate(obstacle, spawnPoint[Random.Range(0, spawnPoint.Length)].position,
        obstacle.transform.rotation);

        StartCoroutine(SpawnNextObstacle(minTime, maxTime));
    }
    #endregion
}
