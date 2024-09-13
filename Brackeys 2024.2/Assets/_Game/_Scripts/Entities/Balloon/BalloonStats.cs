using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonStats : MonoBehaviour
{
    #region Variáveis
    public static BalloonStats Instance;

    [Header("Configurações:")]
    public int InitialMoney;
    public float MinSpeed;
    public float MaxSpeed;
    public float MinStability;
    public float MaxStability;
    public int MinDurability;
    public int MaxDurability;

    [SerializeField] private GameObject gunObject;
    [SerializeField] private GameObject chickenObject;

    // Atributos atuais do Balão
    public static float Speed;
    public static float Stability;
    public static int Durability;
    public static bool HasGun; //botar = true pra testes com a arma
    public static bool HasChicken;

    public static int SpeedLevel;
    public static int StabilityLevel;
    public static int DurabilityLevel;
    public static int CurrentMoney;

    private static bool _isFirstTime = true;
    #endregion

    #region Funções Unity
    private void Awake() => Instance = this;

    private void Start()
    {
        if (_isFirstTime) 
        {
            CurrentMoney = InitialMoney;
            _isFirstTime = false;
        }

        ChangeSpeed(MinSpeed);
        ChangeStability(MinStability);
        ChangeDurability(MinDurability);
    }

    private void Update()
    {
        //Debug.Log("Speed: " + Speed);
        //Debug.Log("Stability: " + Stability);
        //Debug.Log("Durability: " + Durability);
        //Debug.Log("Gun: " + HasGun);
        //Debug.Log("HasChicken: " + HasChicken);
    }
    #endregion

    #region Funções Próprias
    public void ChangeSpeed(float value=0) 
    {
        var newValue = Mathf.Clamp(Speed + value, MinSpeed, MaxSpeed);

        if (newValue < Speed) 
            SpeedLevel--;
        else 
            SpeedLevel++;

        Speed = newValue;
    }

    public void ChangeStability(float value=0)
    {
        var newValue = Mathf.Clamp(Stability + value, MinStability, MaxStability);

        if (newValue < Stability) 
            StabilityLevel--;
        else 
            StabilityLevel++;

        Stability = newValue;
    }

    public void ChangeDurability(int value=0) 
    {
        var newValue = Mathf.Clamp(Durability + value, MinDurability, MaxDurability);

        if (newValue < Durability) 
            DurabilityLevel--;
        else 
            DurabilityLevel++;

        Durability = newValue;
    }

    public void EnableGun()
    {
        gunObject.SetActive(true);
        HasGun = true;
    }

    public void EnableChicken()
    {
        chickenObject.SetActive(true);
        HasChicken = true;
    }
    #endregion
}
