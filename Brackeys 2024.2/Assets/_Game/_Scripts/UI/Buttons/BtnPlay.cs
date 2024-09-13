using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variáveis
    [Header("Referências:")]
    [SerializeField] private GameObject panelInputs;

    [Header("Transição:")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;
    #endregion

    #region Funções Próprias
    public void Play()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Rebuilding");

        GameObject.FindGameObjectWithTag("Garage FG").GetComponent<Animator>().Play("Foreground Upgrade Play Animation");

        Invoke("StartToFillUp", 5.25f);

        // 7.25f
        TransitionManager.Instance().Transition(nextSceneName, transitionSettings, loadTime);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        /*
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Select");
        */

        panelInputs.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        /*
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Deselect");
        */

        panelInputs.SetActive(false);
    }

    private void StartToFillUp() 
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Fill up");
    }

    private void StopAnimation() 
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
    }
    #endregion
}
