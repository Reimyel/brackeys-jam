using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoopPlayer : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private string fullMusicName;
    [SerializeField] private string loopMusicName;
    [SerializeField] private float fullMusicDuration;
    #endregion

    #region Fun��es Unity
    private void Start()
    {
        AudioManager.Instance.PlayMusic(fullMusicName, false);
        Invoke("PlayLoop", fullMusicDuration + 0.1f);
    }
    #endregion

    #region Fun��es Pr�prias
    private void PlayLoop() => AudioManager.Instance.PlayMusic(fullMusicName, false);
    #endregion
}