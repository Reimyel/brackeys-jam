using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    #region Variáveis Globais
    [SerializeField] private float windForce;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] public Transform[] RspawnPoint;
    [SerializeField] public Transform[] LspawnPoint;
    [SerializeField] public Transform[] UspawnPoint;

    [Header("Right Side:")]
    [SerializeField] private float RminTime;
    [SerializeField] private float RmaxTime;

    [Header("Left Side:")]
    [SerializeField] private float LminTime;
    [SerializeField] private float LmaxTime;

    [Header("Up Side:")]
    [SerializeField] private float UminTime;
    [SerializeField] private float UmaxTime;
    #endregion

    #region Funções Unity
    private void Start()
    {
        StartCoroutine(SpawnRightObstacle(RminTime, RmaxTime));
        StartCoroutine(SpawnLeftObstacle(LminTime, LmaxTime));
        StartCoroutine(SpawnUpObstacle(UminTime, UmaxTime));
    }
    #endregion

    #region Funções Próprias
    private IEnumerator SpawnRightObstacle(float minTime, float maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        var obstacle = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];

        Instantiate(obstacle, RspawnPoint[Random.Range(0, RspawnPoint.Length)].position,
        obstacle.transform.rotation);

        StartCoroutine(SpawnRightObstacle(minTime, maxTime));
    }

    private IEnumerator SpawnLeftObstacle(float minTime, float maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        var obstacle = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];

        Instantiate(obstacle, LspawnPoint[Random.Range(0, LspawnPoint.Length)].position,
        obstacle.transform.rotation);

        StartCoroutine(SpawnLeftObstacle(minTime, maxTime));
    }

    private IEnumerator SpawnUpObstacle(float minTime, float maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        var obstacle = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];

        Instantiate(obstacle, UspawnPoint[Random.Range(0, UspawnPoint.Length)].position,
        obstacle.transform.rotation);

        StartCoroutine(SpawnUpObstacle(minTime, maxTime));
    }
    #endregion
}
