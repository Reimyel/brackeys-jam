using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviourScript : MonoBehaviour
{
    #region Referências
    [SerializeField] private Sprite[] spriteVariations;
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSpriteVariation();
    }
    #endregion

    #region Funções Próprias
    void ChangeSpriteVariation()
    {
        int randomIndex = weightedIndices[Random.Range(0, weightedIndices.Length)];
        _spriteRenderer.sprite = spriteVariations[randomIndex];
    }
    #endregion
}
