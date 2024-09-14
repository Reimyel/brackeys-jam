using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private float timeToStage2;
    [SerializeField] private float timeToStage3;
    [SerializeField] private float timeToEnd;
    [SerializeField] private Texture2D spriteStage2;
    [SerializeField] private Texture2D spriteStage3;
    [SerializeField] private float endForce;

    [Header("Transi��o:")]
    [SerializeField] private string endSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;

    [Header("Refer�ncias:")]
    [SerializeField] private Rigidbody2D rbBaloon;
    [SerializeField] private PolygonCollider2D polygonColliderBalloon;
    [SerializeField] private EdgeCollider2D edgeColliderBalloon;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RawImage imgParallax;


    public float time;
    public float timeCounter;
    private bool stage2Triggered = false;
    private bool stage3Triggered = false;
    #endregion

    #region Fun��es Unity
    void Update()
    {
        time = Time.deltaTime;
        timeCounter += time * BalloonStats.Speed;
        timerText.text = ((int)timeCounter).ToString();

        float tolerance = 0.1f;

        if (!stage2Triggered && Mathf.Abs(timeCounter - timeToStage2) <= tolerance)
        {
            Stage2Begin();
            stage2Triggered = true;
        }

        if (!stage3Triggered && Mathf.Abs(timeCounter - timeToStage3) <= tolerance)
        {
            Stage3Begin();
            stage3Triggered = true;
        }
    }
    #endregion

    #region Fun��es Pr�prias
    void Stage2Begin()
    {
        Debug.Log("FASE 2");
        //l�gica de transi��o de fases
        imgParallax.texture = spriteStage2;
    }

    void Stage3Begin()
    {
        Debug.Log("FASE 3");
        //l�gica de transi��o de fases
        imgParallax.texture = spriteStage3;
    }

    void EndGame() 
    {
        Debug.Log("FIM");
        // Fazer o bal�o subir
        polygonColliderBalloon.enabled = false;
        edgeColliderBalloon.enabled = false;

        rbBaloon.AddForce(Vector2.up * endForce * Time.deltaTime);
        TransitionManager.Instance().Transition(endSceneName, transitionSettings, loadTime);
    }
    #endregion
}
