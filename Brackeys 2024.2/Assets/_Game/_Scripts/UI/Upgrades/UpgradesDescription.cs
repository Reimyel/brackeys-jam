using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradesDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Referências:")]
    [SerializeField] private GameObject panelDescription;
    [SerializeField] private GameObject arrowParent;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        panelDescription.SetActive(true);
        arrowParent.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        panelDescription.SetActive(false);
        arrowParent.SetActive(false);
    }
}
