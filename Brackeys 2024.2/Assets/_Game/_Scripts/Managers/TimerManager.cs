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
    [SerializeField] private string musicStage1;
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
    [SerializeField] private GameObject bgTransition;


    public float time;
    public float timeCounter;
    private bool stage2Triggered = false;
    private bool stage3Triggered = false;
    private bool _gameEnded = false;

    private GameObject _objMusic1;
    private GameObject _objMusic2;
    private GameObject _objMusic3;
    #endregion

    #region Funções Unity
    private void Start()
    {
        parallaxParent1.SetActive(true);
        parallaxParent2.SetActive(false);
        parallaxParent3.SetActive(false);

        if (AudioManager.Instance != null)
            _objMusic1 = AudioManager.Instance.PlayMusicGet(musicStage1);
    }

    void Update()
    {
        time = Time.deltaTime;
        if (BalloonStats.SpeedLevel < 1) 
            timeCounter += time * BalloonStats.Speed * 0.35f;
        else
            timeCounter += time * BalloonStats.Speed * 0.15f;

        var txt = (int)timeCounter;
        timerText.text = txt + "ft";

        float tolerance = 1f;

        if (!stage2Triggered && Mathf.Abs(timeCounter - timeToStage2) <= tolerance)
        {
            Stage2Begin();
            stage2Triggered = true;
        }
        else if (!stage3Triggered && Mathf.Abs(timeCounter - timeToStage3) <= tolerance)
        {
            Stage3Begin();
            stage3Triggered = true;
        }
        else if (!_gameEnded && Mathf.Abs(timeCounter - timeToEnd) <= tolerance)
        {
            EndGame();
        }
    }

    private void FixedUpdate()
    {
        if (_gameEnded)
            rbBaloon.gameObject.transform.position += endForce * Vector3.up * Time.fixedDeltaTime * 0.75f;
    }

    #endregion

    #region Funções Próprias
    void Stage2Begin()
    {
        bgTransition.SetActive(true);
        Debug.Log("FASE 2");
        //lógica de transição de fases

        parallaxParent1.SetActive(false);
        parallaxParent2.SetActive(true);
        parallaxParent3.SetActive(false);

        Destroy(_objMusic1);

        if (AudioManager.Instance != null)
        {
            _objMusic2 = AudioManager.Instance.PlayMusicGet(musicStage2);
            Debug.Log("Foi!");
        }
    }

    void Stage3Begin()
    {
        bgTransition.SetActive(true);
        Debug.Log("FASE 3");
        //lógica de transição de fases
        parallaxParent1.SetActive(false);
        parallaxParent2.SetActive(false);
        parallaxParent3.SetActive(true);

        Destroy(_objMusic2);

        if (AudioManager.Instance != null) 
        {
            AudioManager.Instance.PlayMusic(musicStage3);
            Debug.Log("Foi!");
        }

    }

    void EndGame() 
    {
        Debug.Log("FIM");
        // Fazer o balão subir
        polygonColliderBalloon.enabled = false;
        edgeColliderBalloon.enabled = false;

        _gameEnded = true;
        rbBaloon.constraints = RigidbodyConstraints2D.None;
        rbBaloon.gameObject.GetComponent<BalloonMovement>().enabled = false;
        TransitionManager.Instance().Transition(endSceneName, transitionSettings, loadTime);
    }
    #endregion
}
