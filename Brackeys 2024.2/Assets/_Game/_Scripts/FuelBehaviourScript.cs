using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBehaviourScript : MonoBehaviour
{
    #region Variáveis
    private TimerManager _timerManager;
    #endregion

    #region Funções Unity
    void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
    }

    void Update()
    {
        
    }
    #endregion

    #region Funções Próprias
    #endregion
}
