using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class RestartProgress : MonoBehaviour
{
    [Header("Configurações:")]
    
    [Header("Transição:")]
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float transitionInterval;

    #region Funções Unity
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.F5))
        {
            ResetValues();
            GoToMainMenu();
        }
    }
    #endregion

    #region Funções Próprias
    private void ResetValues()
    {
        BalloonStats.IsFirstTime = true;
        DisplayTutorial.IsFirstRun = true;
        DisplayTutorial.BoughtGun = false;
    }

    private void GoToMainMenu() => TransitionManager.Instance().Transition(mainMenuSceneName, transitionSettings, transitionInterval);       
    #endregion
}
