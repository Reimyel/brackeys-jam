using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTutorial : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    
    [Header("Referências:")]
    [SerializeField] private GameObject imgTutorialMovement;
    [SerializeField] private GameObject imgTutorialGun;
    
    public static bool IsFirstRun = true;
    public static bool BoughtGun;
    #endregion
    
    #region  Funções Unity
    private void Start()
    {
        VerifyTutorialMovement();    
        VerifyTutorialGun();
    }
    #endregion

    #region Funções Próprias
    private void VerifyTutorialMovement()
    {
        if (IsFirstRun)
        {
            imgTutorialMovement.SetActive(true);
            IsFirstRun = false;
        }
    }

    private void VerifyTutorialGun()
    {
        if (BoughtGun)
        {
            imgTutorialGun.SetActive(true);
            BoughtGun = false;
        }
    }
    #endregion
}
