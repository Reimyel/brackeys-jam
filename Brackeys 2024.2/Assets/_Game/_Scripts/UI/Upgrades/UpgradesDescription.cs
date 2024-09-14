using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradesDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Referências:")]
    [SerializeField] private GameObject panelDescription;

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
}
