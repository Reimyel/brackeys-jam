using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawnManager : MonoBehaviour
{
    #region Refer�ncias
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private int moneyCount = 3;
    [SerializeField] private GameObject moneyObject;
    [SerializeField] private float distanceBetweenMoney = 1.5f;
    private ObstacleManagerScript _obstacleManagerScript;
    private float _nextSpawnTime;
    #endregion

    #region Fun��es Unity
    private void Awake() => _obstacleManagerScript = FindObjectOfType<ObstacleManagerScript>();

    private void Start() => DefineNextSpawn();

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnMoney();
            DefineNextSpawn();
        }
    }
    #endregion


    #region Fun��es Pr�prias
    private void DefineNextSpawn() => _nextSpawnTime = Time.time + Random.Range(minTime, maxTime);

    private void SpawnMoney()
    {
        int randomIndex = Random.Range(0, _obstacleManagerScript.UspawnPoint.Length);
        Vector3 spawnPosition = _obstacleManagerScript.UspawnPoint[randomIndex].position; //posi��o aleat�ria dos spawnpoints

        for (int i = 0; i < moneyCount; i++)
        {
            Vector3 offset = new Vector3(0, i * distanceBetweenMoney, 0);
            GameObject spawnedMoney = Instantiate(moneyObject, spawnPosition + offset, moneyObject.transform.rotation);

            ChangeMoneyAnimationVariation(spawnedMoney.GetComponent<Animator>());

            Collider2D moneyCollider = spawnedMoney.GetComponent<CircleCollider2D>();
            if (moneyCollider != null)
            {
                moneyCollider.enabled = false; // Desativa o colisor
                StartCoroutine(EnableColliderAfterDelay(moneyCollider, 1f));
            }
        }
    }

    private IEnumerator EnableColliderAfterDelay(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    private void ChangeMoneyAnimationVariation(Animator moneyAnim)
    {
        int randomIndex = Random.Range(0, 2) + 1;
        moneyAnim.Play("Money " + randomIndex + " Flip Animation");
    }
    #endregion
}
