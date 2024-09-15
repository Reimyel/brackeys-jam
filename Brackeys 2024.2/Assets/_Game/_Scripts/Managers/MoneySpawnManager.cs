using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawnManager : MonoBehaviour
{
    #region Referências
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private int moneyCount = 3;
    [SerializeField] private GameObject moneyObject;
    [SerializeField] private float distanceBetweenMoney = 1.5f;
    [SerializeField] public AnimationClip[] moneyAnimations;
    [SerializeField] private Animator moneyAnimator;
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
            ChangeMoneyAnimationVariation();
            Vector3 offset = new Vector3(0, i * distanceBetweenMoney, 0);
            GameObject spawnedMoney = Instantiate(moneyObject, spawnPosition + offset, moneyObject.transform.rotation);

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

    void ChangeMoneyAnimationVariation()
    {
        int randomIndex = Random.Range(0, moneyAnimations.Length);
        moneyAnimator.Play(moneyAnimations[randomIndex].name);
    }
    #endregion
}
