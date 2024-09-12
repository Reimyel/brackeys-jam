using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region Variáveis
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeToStage2;
    [SerializeField] private float timeToStage3;
    public float time;
    public float timeCounter;
    private bool stage2Triggered = false;
    private bool stage3Triggered = false;
    #endregion

    #region Funções Unity
    void Update()
    {
        time = Time.deltaTime;
        timeCounter += time * BalloonStats.Speed;
        timerText.text = ((int)timeCounter).ToString();

        float tolerance = 0.1f;

        //lógicas pros pontos de transição
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

    #region Funções Próprias
    void Stage2Begin()
    {
        Debug.Log("FASE 2");
    }

    void Stage3Begin()
    {
        Debug.Log("FASE 3");
    }
    #endregion
}
