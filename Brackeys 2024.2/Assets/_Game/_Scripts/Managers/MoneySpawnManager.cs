using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawnManager : MonoBehaviour
{
    #region Referências
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject moneyObject;
    [SerializeField] private Transform[] moneySpawnPoints;
    #endregion

    #region Funções Unity
    private void Update()
    {
        StartCoroutine(SpawnMoney(minTime, maxTime));
    }
    #endregion

    #region Funções Próprias
    private IEnumerator SpawnMoney(float minTime, float maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        var instance = Instantiate(moneyObject, moneySpawnPoints[Random.Range(0, moneySpawnPoints.Length)].position, moneyObject.transform.rotation);

        ObstacleBehaviourScript behaviourScript = instance.GetComponent<ObstacleBehaviourScript>();
        if (behaviourScript != null)
        {
            behaviourScript.SetDirection(Vector2.left);
        }

        StartCoroutine(SpawnMoney(minTime, maxTime));
    }
    #endregion
}
