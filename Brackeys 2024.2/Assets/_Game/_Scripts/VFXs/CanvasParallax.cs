using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasParallax : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��o:")]
    [SerializeField] private float minThunderTime;
    [SerializeField] private float maxThunderTime;

    [Header("Refer�ncias:")]
    [SerializeField] private Color imgThunderDefaultColor;
    [SerializeField] private RawImage imgThunder;
    #endregion


    // Componentes:
    private Animator _anim;

    #region Fun��es Unity
    private void Awake() => _anim = GetComponent<Animator>();

    private void Start() => SetNewThunder();
    #endregion

    #region Fun��es Pr�prias
    private void SetNewThunder() 
    {
        _anim.enabled = false;
        imgThunder.color = imgThunderDefaultColor;
        Invoke("EnableAnimator", Random.Range(minThunderTime, maxThunderTime));
    }

    private void EnableAnimator() => _anim.enabled = true;
    #endregion
}
