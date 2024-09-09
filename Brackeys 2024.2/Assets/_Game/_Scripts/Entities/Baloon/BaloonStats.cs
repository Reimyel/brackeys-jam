using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaloonStats : MonoBehaviour
{
    #region Variáveis
    public static BaloonStats Instance;

    [Header("Configurações:")]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minStability;
    [SerializeField] private float maxStability;
    [SerializeField] private int minDurability;
    [SerializeField] private int maxDurability;
    
    // Atributos atuais do Balão
    public static float Speed { get; private set; }
    public static float Stability { get; private set; }
    public static int Durability { get; private set; }
    public static bool HasGun { get; private set; }
    public static bool HasChicken { get; private set; }

    public static int SpeedLevel;
    public static int StabilityLevel;
    public static int DurabilityLevel;
    #endregion

    #region Funções Unity
    private void Awake() => Instance = this;

    private void Start()
    {
        ChangeSpeed();
        ChangeStability();
        ChangeDurability();
    }

    private void Update()
    {
        Debug.Log("Speed: " + Speed);
        Debug.Log("Stability: " + Stability);
        Debug.Log("Durability: " + Durability);
        Debug.Log("Gun: " + HasGun);
        Debug.Log("HasChicken: " + HasChicken);
    }
    #endregion

    #region Funções Próprias
    public void ChangeSpeed(float value=0) 
    {
        var newValue = Mathf.Clamp(Speed + value, minSpeed, maxSpeed);

        if (newValue < Speed)
            SpeedLevel--;
        else
            SpeedLevel++;

        Speed = newValue;
    }

    public void ChangeStability(float value=0)
    {
        var newValue = Mathf.Clamp(Stability + value, minStability, maxStability);

        if (newValue < Stability)
            StabilityLevel--;
        else
            StabilityLevel++;

        Stability = newValue;
    }

    public void ChangeDurability(int value=0) 
    {
        var newValue = Mathf.Clamp(Durability + value, minDurability, maxDurability);

        if (newValue < Durability)
            DurabilityLevel--;
        else
            DurabilityLevel++;

        Durability = newValue;
    }

    public void EnableGun() 
    {
        HasGun = true;
        //TODO: ATIVAR ARMA DEPOIS
    }

    public void EnableChicken() 
    {
        HasChicken = true;
        //TODO: ATIVAR OBJETO DEPOIS
    }
    #endregion
}
