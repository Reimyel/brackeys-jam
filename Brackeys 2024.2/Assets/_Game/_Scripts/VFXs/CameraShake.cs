using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    #region Variáveis
    [SerializeField] private CinemachineVirtualCamera defaultVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera shakeVirtualCameraData;

    // Componentes:
    private Animator _anim;

    private CinemachineBasicMultiChannelPerlin _defaultBasicMultiChannel;

    private NoiseSettings _shakeNoiseProfile;
    private NoiseSettings _initialNoiseProfile;

    private float _defaultIntensity;

    public enum ShakeType 
    {
        Default,
        Wind
    }
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _anim = GetComponent<Animator>();

        _defaultBasicMultiChannel = defaultVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _defaultIntensity = _defaultBasicMultiChannel.m_AmplitudeGain;

        _initialNoiseProfile = _defaultBasicMultiChannel.m_NoiseProfile;
        _shakeNoiseProfile = shakeVirtualCameraData.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile;

        shakeVirtualCameraData.gameObject.SetActive(false);
    }
    #endregion

    #region Funções Próprias
    public void ApplyShake(float intensity, float interval)
    {
        _defaultBasicMultiChannel.m_NoiseProfile = _shakeNoiseProfile;
        _defaultBasicMultiChannel.m_AmplitudeGain = intensity;
        StartCoroutine(StopShake(interval));
    }

    private IEnumerator StopShake(float interval) 
    {
        yield return new WaitForSeconds(interval);
        _defaultBasicMultiChannel.m_NoiseProfile = _initialNoiseProfile;
        _defaultBasicMultiChannel.m_AmplitudeGain = _defaultIntensity;
    }
    #endregion
}
