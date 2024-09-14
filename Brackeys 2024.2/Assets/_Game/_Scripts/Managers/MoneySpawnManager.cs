using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawnManager : MonoBehaviour
{
    #region Referências
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject moneyObject;
    private ObstacleManagerScript _obstacleManagerScript;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _obstacleManagerScript = FindObjectOfType<ObstacleManagerScript>();
    }

    private void Update()
    {
        StartCoroutine(SpawnMoney(minTime, maxTime));
    }
    #endregion

    #region Funções Próprias
    private IEnumerator SpawnMoney(float minTime, float maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        var instance = Instantiate(moneyObject, _obstacleManagerScript.UspawnPoint[Random.Range(0, _obstacleManagerScript.UspawnPoint.Length)].position, moneyObject.transform.rotation);

        ObstacleBehaviourScript behaviourScript = instance.GetComponent<ObstacleBehaviourScript>();
        if (behaviourScript != null)
        {
            behaviourScript.SetDirection(Vector2.down);
        }

        StartCoroutine(SpawnMoney(minTime, maxTime));
    }
    #endregion
}
