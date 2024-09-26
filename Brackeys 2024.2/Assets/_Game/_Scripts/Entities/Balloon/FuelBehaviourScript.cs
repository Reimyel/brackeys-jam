using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FuelBehaviourScript : MonoBehaviour
{
    #region Variáveis
    [SerializeField] private Slider chargeBar;
    [SerializeField] private float fillRate = 0.5f;
    [SerializeField] private float drainRate = 0.05f;
    [SerializeField] private float delayBeforeDraining = 1f;

    [SerializeField] private BackgroundScroller cloudBackgroundScroller1;
    [SerializeField] private BackgroundScroller skyBackgroundScroller1;

    [SerializeField] private BackgroundScroller cloudBackgroundScroller2;

    [SerializeField] private BackgroundScroller cloudBackgroundScroller3;
    [SerializeField] private BackgroundScroller skyBackgroundScroller3;

    private bool isFilling = false;
    private float fillDelayTimer = 0f;
    private TimerManager _timerManager;
    private BalloonCollision _balloonCollision;

    private bool _canFill = true;
    #endregion

    #region Funções Unity
    void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
        _balloonCollision = FindObjectOfType<BalloonCollision>();
    }

    void LateUpdate()
    {
        if (!_canFill) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFilling = true;
            fillDelayTimer = 0f;
            cloudBackgroundScroller1.ySpeed *= 1.5f;
            skyBackgroundScroller1.ySpeed *= 1.25f;

            cloudBackgroundScroller2.ySpeed *= 1.5f;

            cloudBackgroundScroller3.ySpeed *= 1.5f;
            skyBackgroundScroller3.ySpeed *= 1.25f;
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
                _canFill = false;
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
            cloudBackgroundScroller1.ySpeed *= 1 / 1.5f;
            skyBackgroundScroller1.ySpeed *= 1 / 1.25f ;

            cloudBackgroundScroller2.ySpeed *= 1 / 1.5f;

            cloudBackgroundScroller3.ySpeed *= 1 / 1.5f;
            skyBackgroundScroller3.ySpeed *= 1 / 1.25f;
        }
    }
    #endregion

    #region Funções Próprias
    #endregion
}
