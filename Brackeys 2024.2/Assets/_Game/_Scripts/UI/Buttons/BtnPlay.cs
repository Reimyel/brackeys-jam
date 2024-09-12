using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Referências:")]
    [SerializeField] private GameObject panelInputs;

    [Header("Transição:")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;

    public void Play()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Rebuilding");

        Invoke("StartToFillUp", 5.25f);

        // 7.25f
        TransitionManager.Instance().Transition(nextSceneName, transitionSettings, loadTime);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Select");

        panelInputs.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Deselect");

        panelInputs.SetActive(false);
    }

    private void StartToFillUp() 
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Fill up");
    }
}
