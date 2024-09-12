using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBehaviourScript : MonoBehaviour
{
    #region Variáveis
    [SerializeField] private Slider chargeBar;
    [SerializeField] private float fillRate = 0.1f;
    [SerializeField] private float drainRate = 0.05f;
    [SerializeField] private float delayBeforeDraining = 1f;
    private bool isFilling = false;
    private float fillDelayTimer = 0f;
    private TimerManager _timerManager;
    private BalloonCollision _balloonCollision;
    #endregion

    #region Funções Unity
    void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
        _balloonCollision = FindObjectOfType<BalloonCollision>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFilling = true;
            fillDelayTimer = 0f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _timerManager.timeCounter += _timerManager.time * BalloonStats.Speed;
        }

        if (isFilling)
        {
            chargeBar.value += fillRate * Time.deltaTime;

            if (chargeBar.value >= chargeBar.maxValue)
            {
                _balloonCollision.ReduceDurability(BalloonStats.Durability);
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

        /*if (chargeBar.value > chargeBar.minValue)
        {
            //lógica pro timer ir mais rápido
            _timerManager.timeCounter += _timerManager.time;
        }*/
    }
    #endregion

    #region Funções Próprias
    #endregion
}
