using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeToStage2;
    [SerializeField] private float timeToStage3;
    private float time;
    private float timeCounter;

    void Update()
    {
        time = Time.deltaTime;
        timeCounter += time;
        timerText.text = ((int)timeCounter).ToString();

        //lógicas pros pontos de transição
        if (timeCounter == 2f)
        {
            Stage2Begin();
        }

        if (timeCounter == timeToStage3)
        {
            Stage3Begin();
        }
    }

    void Stage2Begin()
    {
        Debug.Log("FASE 2");
    }

    void Stage3Begin()
    {
        Debug.Log("FASE 3");
    }
}
