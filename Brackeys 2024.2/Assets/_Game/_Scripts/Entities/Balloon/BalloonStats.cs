using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonStats : MonoBehaviour
{
    #region Vari�veis
    public static BalloonStats Instance;

    [Header("Configura��es:")]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minStability;
    [SerializeField] private float maxStability;
    [SerializeField] private int minDurability;
    [SerializeField] private int maxDurability;

    // Atributos atuais do Bal�o
    public static float Speed;
    public static float Stability;
    public static int Durability;
    public static bool HasGun;
    public static bool HasChicken;

    public static int SpeedLevel;
    public static int StabilityLevel;
    public static int DurabilityLevel;
    public static int CurrentMoney;
    #endregion

    #region Fun��es Unity
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

    #region Fun��es Pr�prias
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
