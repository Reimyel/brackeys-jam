using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBehaviourScript : MonoBehaviour
{
    #region Vari�veis
    private TimerManager _timerManager;
    #endregion

    #region Fun��es Unity
    void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
    }

    void Update()
    {
        
    }
    #endregion

    #region Fun��es Pr�prias
    #endregion
}
