using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviourScript : MonoBehaviour
{
    #region Referências
    [SerializeField] private int instantiateQuantity;
    [SerializeField] private GameObject chickenObject;
    [SerializeField] private Sprite[] spriteVariations;
    private ObstacleManagerScript _obstacleManagerScript;
    private SpriteRenderer _spriteRenderer;
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
    void Awake()
    {
        _obstacleManagerScript = FindObjectOfType<ObstacleManagerScript>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSpriteVariation();
    }
    #endregion

    #region Funções Próprias
    void ChangeSpriteVariation()
    {
        int randomIndex = weightedIndices[Random.Range(0, weightedIndices.Length)];
        _spriteRenderer.sprite = spriteVariations[randomIndex];

        if (randomIndex == 0 | randomIndex == 1 | randomIndex == 2 | randomIndex == 3 | randomIndex == 4 | randomIndex == 5 | randomIndex == 6)
        {
            ChickenMoment();
        }
    }

    void ChickenMoment()
    {
        for (int i = 0; i < instantiateQuantity; i++)
        {
            int randomIndex = Random.Range(0, _obstacleManagerScript.spawnPoint.Length);

            Instantiate(chickenObject, _obstacleManagerScript.spawnPoint[randomIndex].position, chickenObject.transform.rotation);
        }
    }
    #endregion
}
