using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonStats : MonoBehaviour
{
    #region Vari�veis
    public static BaloonStats Instance;

    [Header("Configura��es:")]
    [SerializeField] private float minSpeed, maxSpeed;
    [SerializeField] private float minStability, maxStability;
    [SerializeField] private float minDurability, maxDurability;
    [SerializeField] private float maxArmor;
    
    // Atributos atuais do Bal�o
    public static float Speed;
    public static float Stability;
    public static float Durability;
    public static float Armor;
    public static bool HasChicken;
    #endregion

    #region Fun��es Unity
    private void Awake() => Instance = this;
    #endregion


    #region Fun��es Pr�prias
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
