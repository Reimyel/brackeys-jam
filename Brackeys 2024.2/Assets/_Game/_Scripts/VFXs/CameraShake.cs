using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    #region Vari�veis
    // Componentes:
    private CinemachineBasicMultiChannelPerlin _basicMultiChannel;
    #endregion

    #region Fun��es Unity
    private void Awake()
    {
        var virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _basicMultiChannel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    #endregion

    #region Fun��es Pr�prias
    public void ApplyShake(float intensity, float interval) 
    {
        _basicMultiChannel.m_AmplitudeGain = intensity;
        StartCoroutine(StopShake(interval));
    }

    private IEnumerator StopShake(float interval) 
    {
        yield return new WaitForSeconds(interval);
        _basicMultiChannel.m_AmplitudeGain = 0f;
    }
    #endregion
}
