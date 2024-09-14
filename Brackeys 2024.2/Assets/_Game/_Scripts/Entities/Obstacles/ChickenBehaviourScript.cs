using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviourScript : MonoBehaviour
{

    #region Referências
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private GameObject chickenObject;
    [SerializeField] private GameObject cowObject;
    [SerializeField] private Sprite[] chickenSpriteVariations;
    [SerializeField] private Sprite[] cowSpriteVariations;
    [SerializeField] private int instantiateQuantity;
    [SerializeField] private SpriteRenderer _chickenSpriteRenderer;
    [SerializeField] private SpriteRenderer _cowSpriteRenderer;
    [SerializeField] public Transform[] spawnPoint;
    private int[] weightedIndices = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //14 vezes pra 14%
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //14 vezes pra 14%
        2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, //14 vezes pra 14%
        3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, //14 vezes pra 14%
        4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, //14 vezes pra 14%
        5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, //14 vezes pra 14%
        6, 6  //2 vezes pra 2%
    };
    public static bool IsChickenMoment = false;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        ChangeSpriteVariation();
    }

    private void Start() => IsChickenMoment = false;

    #endregion

    #region Funções Próprias
    void ChangeSpriteVariation()
    {
        int randomIndex = weightedIndices[Random.Range(0, weightedIndices.Length)];
        _chickenSpriteRenderer.sprite = chickenSpriteVariations[randomIndex];

        if (randomIndex == 0 | randomIndex == 1 | randomIndex == 2 | randomIndex == 3 | randomIndex == 4 | randomIndex == 5 | randomIndex == 6)
        {
            InvokeRepeating("StartChickenMoment", 0f, 1f);

            Invoke("StopChickenMoment", 5f);
        }
    }

    void StartChickenMoment()
    {
        IsChickenMoment = true;

        int spawnCount = Random.Range(1, spawnPoint.Length + 1);

        //spawn points usados
        List<int> usedSpawnPoints = new List<int>();

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, spawnPoint.Length);
            }
            while (usedSpawnPoints.Contains(randomIndex));

            //lista de usados
            usedSpawnPoints.Add(randomIndex);

            Debug.Log("MOMENTO GALINHA");
            Instantiate(chickenObject, spawnPoint[randomIndex].position + offset, chickenObject.transform.rotation);
        }

        IsChickenMoment = false;
    }

    void StopChickenMoment()
    {
        CancelInvoke("StartChickenMoment");
    }
    #endregion
}
