using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradesDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Vari�veis
    [Header("Refer�ncias:")]
    [SerializeField] private GameObject panelDescription;
    #endregion

    #region Fun��es Pr�prias
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        /*
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Select");
        */
        panelDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        /*
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Deselect");
        */

        panelDescription.SetActive(false);;
    }
    #endregion
}
