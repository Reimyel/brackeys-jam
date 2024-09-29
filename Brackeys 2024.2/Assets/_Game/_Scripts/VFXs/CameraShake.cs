using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    #region Variáveis
    // Componentes:
    private CinemachineBasicMultiChannelPerlin _basicMultiChannel;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        var virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _basicMultiChannel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    #endregion

    #region Funções Próprias
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
