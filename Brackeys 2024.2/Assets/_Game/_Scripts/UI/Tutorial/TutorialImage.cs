using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImage : MonoBehaviour
{
    #region Variáveis

    [Header("Configurações:")] 
    [SerializeField] private float fadeOutDelay;
    
    [Header("Referências:")] 
    [SerializeField] private FadeVFX fadeInScript;
    [SerializeField] private FadeVFX fadeOutScript;

    private bool _fadeInEnded = false;
    #endregion

    #region Funções Unity
    private void Update() => CheckFadeInEnd();
    #endregion

    #region Funções Próprias
    private void CheckFadeInEnd()
    {
        if (!fadeInScript.enabled)
            Invoke("StartFadeOut", fadeOutDelay);
    }

    private void StartFadeOut()
    {
        fadeOutScript.enabled = true;
        this.enabled = false;
    }
    #endregion
}
