using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawnManager : MonoBehaviour
{
    #region Referências
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private int moneyCount = 4;
    [SerializeField] private GameObject moneyObject;
    private ObstacleManagerScript _obstacleManagerScript;
    private float _nextSpawnTime;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _obstacleManagerScript = FindObjectOfType<ObstacleManagerScript>();
    }

    private void Start()
    {
        DefineNextSpawn();
    }

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnMoney();
            DefineNextSpawn();
        }
    }
    #endregion

    void DefineNextSpawn()
    {
        _nextSpawnTime = Time.time + Random.Range(minTime, maxTime);
    }

    #region Funções Próprias
    void SpawnMoney()
    {
        int randomIndex = Random.Range(0, _obstacleManagerScript.UspawnPoint.Length);
        Vector3 spawnPosition = _obstacleManagerScript.UspawnPoint[randomIndex].position; //posição aleatória dos spawnpoints

        for (int i = 0; i < moneyCount; i++)
        {
            Vector3 offset = new Vector3(0, i * 1.5f, 0); //distancia entre moedas
            Instantiate(moneyObject, spawnPosition + offset, moneyObject.transform.rotation);
        }
    }
    #endregion
}
