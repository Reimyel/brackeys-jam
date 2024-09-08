using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPlay : MonoBehaviour
{
    [Header("Transição:")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;

    public void Play() => TransitionManager.Instance().Transition(nextSceneName, transitionSettings, loadTime);
}
