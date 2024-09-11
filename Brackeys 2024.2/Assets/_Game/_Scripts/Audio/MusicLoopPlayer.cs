using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoopPlayer : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private string fullMusicName;
    [SerializeField] private string loopMusicName;
    [SerializeField] private float fullMusicDuration;
    #endregion

    #region Funções Unity
    private void Start()
    {
        AudioManager.Instance.PlayMusic(fullMusicName, false);
        Invoke("PlayLoop", fullMusicDuration + 0.1f);
    }
    #endregion

    #region Funções Próprias
    private void PlayLoop() => AudioManager.Instance.PlayMusic(fullMusicName, false);
    #endregion
}