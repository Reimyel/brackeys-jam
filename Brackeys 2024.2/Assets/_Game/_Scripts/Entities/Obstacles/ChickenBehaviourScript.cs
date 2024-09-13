using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviourScript : MonoBehaviour
{
    public static bool IsChickenMoment = false;

    #region Referências
    [SerializeField] private GameObject chickenObject;
    [SerializeField] private GameObject cowObject;
    [SerializeField] private Sprite[] chickenSpriteVariations;
    [SerializeField] private Sprite[] cowSpriteVariations;
    [SerializeField] private int instantiateQuantity;
    [SerializeField] private SpriteRenderer _chickenSpriteRenderer;
    [SerializeField] private SpriteRenderer _cowSpriteRenderer;
    private ObstacleManagerScript _obstacleManagerScript;
    private int[] weightedIndices = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //14 vezes pra 14%
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //14 vezes pra 14%
        2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, //14 vezes pra 14%
        3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, //14 vezes pra 14%
        4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, //14 vezes pra 14%
        5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, //14 vezes pra 14%
        6, 6  //2 vezes pra 2%
    };
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _obstacleManagerScript = FindObjectOfType<ObstacleManagerScript>();
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
            ChickenMoment();
        }
    }

    void ChickenMoment()
    {
        IsChickenMoment = true;

        for (int i = 0; i < instantiateQuantity; i++)
        {
            int randomIndex = Random.Range(0, _obstacleManagerScript.spawnPoint.Length);

            Instantiate(chickenObject, _obstacleManagerScript.spawnPoint[randomIndex].position, chickenObject.transform.rotation);
        }

        IsChickenMoment = false;
    }
    #endregion
}
