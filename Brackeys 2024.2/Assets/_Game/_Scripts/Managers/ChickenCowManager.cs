using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenCowManager : MonoBehaviour
{

    #region Referências
    [SerializeField] private GameObject chickenObject;
    [SerializeField] private GameObject cowObject;
    [SerializeField] private Sprite[] chickenSpriteVariations;
    [SerializeField] private Sprite[] cowSpriteVariations;
    [SerializeField] private int instantiateQuantity;
    [SerializeField] private SpriteRenderer _chickenSpriteRenderer;
    [SerializeField] private SpriteRenderer _cowSpriteRenderer;
    private ObstacleManagerScript _obstacleManagerScript;
    private int[] chickenWeightedIndices = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //14 vezes pra 14%
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //14 vezes pra 14%
        2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, //14 vezes pra 14%
        3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, //14 vezes pra 14%
        4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, //14 vezes pra 14%
        5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, //14 vezes pra 14%
        6, 6  //2 vezes pra 2%
    };
    private int[] cowWeightedIndices = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //14 vezes pra 14%
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //24 vezes pra 24%
        2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, //24 vezes pra 24%
        3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, //24 vezes pra 24%
        4, 4, 4, 4 //4 vezes pra 4%
    };
    public static bool IsChickenMoment = false;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _obstacleManagerScript = FindObjectOfType<ObstacleManagerScript>();
    }

    private void Start()
    {
        IsChickenMoment = false;

        ChangeChickenSpriteVariation();
        ChangeCowSpriteVariation();
    }
    #endregion

    #region Funções Próprias
    void ChangeChickenSpriteVariation()
    {
        int randomIndex = chickenWeightedIndices[Random.Range(0, chickenWeightedIndices.Length)];
        _chickenSpriteRenderer.sprite = chickenSpriteVariations[randomIndex];

        if (randomIndex == 6)
        {
            //SOM DO MOMENTO GALINHA PRO DUCA VER

            InvokeRepeating("StartChickenMoment", 0f, 1f);

            Invoke("StopChickenMoment", 5f);
        }
    }

    void ChangeCowSpriteVariation()
    {
        int randomIndex = cowWeightedIndices[Random.Range(0, cowWeightedIndices.Length)];
        _cowSpriteRenderer.sprite = cowSpriteVariations[randomIndex];
    }

    void StartChickenMoment()
    {
        IsChickenMoment = true;

        int spawnCount = Random.Range(1, _obstacleManagerScript.UspawnPoint.Length + 1);

        //spawn points usados
        List<int> usedSpawnPoints = new List<int>();

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, _obstacleManagerScript.UspawnPoint.Length);
            }
            while (usedSpawnPoints.Contains(randomIndex));

            //lista de usados
            usedSpawnPoints.Add(randomIndex);

            //variação ao redor do spawn
            Vector3 randomOffset = new Vector3(
                Random.Range(-3f, 3f),
                Random.Range(-3f, 3f),
                0f
            );

            Debug.Log("MOMENTO GALINHA");
            Instantiate(chickenObject, _obstacleManagerScript.UspawnPoint[randomIndex].position + randomOffset, chickenObject.transform.rotation);
        }

        IsChickenMoment = false;
    }

    void StopChickenMoment()
    {
        CancelInvoke("StartChickenMoment");
    }
    #endregion
}
