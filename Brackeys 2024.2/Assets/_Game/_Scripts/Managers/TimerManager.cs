using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private float timeToStage2;
    [SerializeField] private float timeToStage3;
    [SerializeField] private float timeToEnd;
    [SerializeField] private string musicStage2;
    [SerializeField] private string musicStage3;
    [SerializeField] private float endForce;

    [Header("Transição:")]
    [SerializeField] private string endSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;

    [Header("Referências:")]
    [SerializeField] private Rigidbody2D rbBaloon;
    [SerializeField] private PolygonCollider2D polygonColliderBalloon;
    [SerializeField] private EdgeCollider2D edgeColliderBalloon;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject parallaxParent1;
    [SerializeField] private GameObject parallaxParent2;
    [SerializeField] private GameObject parallaxParent3;


    public float time;
    public float timeCounter;
    private bool stage2Triggered = false;
    private bool stage3Triggered = false;
    #endregion

    #region Funções Unity
    private void Start()
    {
        parallaxParent1.SetActive(true);
        parallaxParent2.SetActive(false);
        parallaxParent3.SetActive(false);
    }

    void Update()
    {
        time = Time.deltaTime;
        timeCounter += time * BalloonStats.Speed;
        timerText.text = Mathf.Floor(timeCounter / 100f).ToString() + "ft";

        float tolerance = 0.1f;

        if (!stage2Triggered && Mathf.Abs(timeCounter - timeToStage2) <= tolerance)
        {
            Stage2Begin();
            stage2Triggered = true;
        }

        if (!stage3Triggered && Mathf.Abs(timeCounter - timeToStage3) <= tolerance)
        {
            Stage3Begin();
            stage3Triggered = true;
        }
    }
    #endregion

    #region Funções Próprias
    void Stage2Begin()
    {
        Debug.Log("FASE 2");
        //lógica de transição de fases

        parallaxParent1.SetActive(false);
        parallaxParent2.SetActive(true);
        parallaxParent3.SetActive(false);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(musicStage2);
    }

    void Stage3Begin()
    {
        Debug.Log("FASE 3");
        //lógica de transição de fases
        parallaxParent1.SetActive(false);
        parallaxParent2.SetActive(false);
        parallaxParent3.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(musicStage3);
    }

    void EndGame() 
    {
        Debug.Log("FIM");
        // Fazer o balão subir
        polygonColliderBalloon.enabled = false;
        edgeColliderBalloon.enabled = false;

        rbBaloon.AddForce(Vector2.up * endForce * Time.deltaTime);
        TransitionManager.Instance().Transition(endSceneName, transitionSettings, loadTime);
    }
    #endregion
}
