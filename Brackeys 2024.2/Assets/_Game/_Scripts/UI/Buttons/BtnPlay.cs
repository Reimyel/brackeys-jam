using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Refer�ncias:")]
    [SerializeField] private GameObject panelInputs;

    [Header("Transi��o:")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;

    public void Play() => TransitionManager.Instance().Transition(nextSceneName, transitionSettings, loadTime);
   
    public void OnPointerEnter(PointerEventData pointerEventData) => panelInputs.SetActive(true);

    public void OnPointerExit(PointerEventData pointerEventData) => panelInputs.SetActive(false);
}
