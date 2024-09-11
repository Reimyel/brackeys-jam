using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBehaviourScript : MonoBehaviour
{
    #region Vari�veis
    [SerializeField] private Slider chargeBar;
    [SerializeField] private float fillRate = 0.1f;
    [SerializeField] private float drainRate = 0.05f;
    [SerializeField] private float delayBeforeDraining = 1f;
    private bool isFilling = false;
    private float fillDelayTimer = 0f;
    private TimerManager _timerManager;
    #endregion

    #region Fun��es Unity
    void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFilling = true;
            fillDelayTimer = 0f;
        }

        if (isFilling)
        {
            chargeBar.value += fillRate * Time.deltaTime;

            if (chargeBar.value >= chargeBar.maxValue)
            {
                //l�gica pra ele explodir (morrer)
                Debug.Log("MORREU");
                chargeBar.value = chargeBar.maxValue;
                isFilling = false;
            }
        }
        else
        {
            fillDelayTimer += Time.deltaTime;
            if (fillDelayTimer >= delayBeforeDraining)
            {
                chargeBar.value -= drainRate * Time.deltaTime;

                if (chargeBar.value <= chargeBar.minValue)
                {
                    chargeBar.value = chargeBar.minValue;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFilling = false;
        }

        if (chargeBar.value > chargeBar.minValue)
        {
            //l�gica pro timer ir mais r�pido
            _timerManager.timeCounter += _timerManager.time;
        }
    }
    #endregion

    #region Fun��es Pr�prias
    #endregion
}
