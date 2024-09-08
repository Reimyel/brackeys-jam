using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonStats : MonoBehaviour
{
    #region Variáveis
    public static BaloonStats Instance;

    [Header("Configurações:")]
    [SerializeField] private float minSpeed, maxSpeed;
    [SerializeField] private float minStability, maxStability;
    [SerializeField] private float minDurability, maxDurability;
    [SerializeField] private float maxArmor;
    
    // Atributos atuais do Balão
    public static float Speed;
    public static float Stability;
    public static float Durability;
    public static float Armor;
    public static bool HasChicken;
    #endregion

    #region Funções Unity
    private void Awake() => Instance = this;
    #endregion


    #region Funções Próprias
    public void ChangeSpeed(float value) 
    {
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
    }

    public void ChangeStability(float value)
    {
        Stability = Mathf.Clamp(Stability, minStability, maxStability);
    }

    public void ChangeDurability(float value) 
    {
        Durability = Mathf.Clamp(Durability, minDurability, maxDurability);
    }

    public void ChangeArmor(float value) 
    {
        Armor = Mathf.Clamp(Armor, 0, maxArmor);
    }

    public void EnableChicken() 
    {
        HasChicken = true;
        //TODO: ATIVAR OBJETO DEPOIS
    }
    #endregion
}
