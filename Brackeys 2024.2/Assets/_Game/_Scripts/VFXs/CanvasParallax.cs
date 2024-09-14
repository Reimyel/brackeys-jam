using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasParallax : MonoBehaviour
{
    #region Variáveis
    [Header("Configuração:")]
    [SerializeField] private float minThunderTime;
    [SerializeField] private float maxThunderTime;

    [Header("Referências:")]
    [SerializeField] private Color imgThunderDefaultColor;
    [SerializeField] private RawImage imgThunder;
    #endregion


    // Componentes:
    private Animator _anim;

    #region Funções Unity
    private void Awake() => _anim = GetComponent<Animator>();

    private void Start() => SetNewThunder();
    #endregion

    #region Funções Próprias
    private void SetNewThunder() 
    {
        _anim.enabled = false;
        imgThunder.color = imgThunderDefaultColor;
        Invoke("EnableAnimator", Random.Range(minThunderTime, maxThunderTime));
    }

    private void EnableAnimator() => _anim.enabled = true;
    #endregion
}
