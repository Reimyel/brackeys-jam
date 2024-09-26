using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variáveis
    [Header("Referências:")]
    [SerializeField] private GameObject panelInputs;
    [SerializeField] private GameObject balloonPlay;
    [SerializeField] private RuntimeAnimatorController pressedAnimatorController;

    [Header("Transição:")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;

    private bool _isPlayed = false;
    #endregion

    #region Funções Próprias
    public void Play()
    {
        if (_isPlayed) return;

        gameObject.GetComponent<Animator>().runtimeAnimatorController = pressedAnimatorController;

        if (AudioManager.Instance != null && SceneManager.GetActiveScene().name == "Upgrade Scene")
            AudioManager.Instance.PlaySFX("Rebuilding");

        if (GameObject.FindGameObjectWithTag("Garage FG").GetComponent<Animator>() != null)
            GameObject.FindGameObjectWithTag("Garage FG").GetComponent<Animator>().Play("Foreground Upgrade Play Animation");

        if (SceneManager.GetActiveScene().name == "Main Menu Scene")
            GameObject.FindGameObjectWithTag("Menu FG").GetComponent<Image>().enabled = true;

        Invoke("StartToFillUp", 5.25f);
        Invoke("ActivateBalloonPlay", 5f);

        // 7.25f
        TransitionManager.Instance().Transition(nextSceneName, transitionSettings, loadTime);

        _isPlayed = true;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        /*
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Select");
        */
        if (panelInputs != null)
            panelInputs.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        /*
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Deselect");
        */
        if (panelInputs != null)
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

    private void ActivateBalloonPlay() => balloonPlay.SetActive(true);
    #endregion
}
