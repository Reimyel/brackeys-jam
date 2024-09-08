using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaloonStats : MonoBehaviour
{
    #region Variáveis
    public static BaloonStats Instance;

    [Header("Configurações:")]
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;
    [SerializeField] private int minStability;
    [SerializeField] private int maxStability;
    [SerializeField] private int minDurability;
    [SerializeField] private int maxDurability;
    [SerializeField] private int maxArmor;
    
    // Atributos atuais do Balão
    public static int Speed { get; private set; }
    public static int Stability { get; private set; }
    public static int Durability { get; private set; }
    public static int Armor { get; private set; }
    public static bool HasChicken { get; private set; }
    #endregion

    #region Funções Unity
    private void Awake() => Instance = this;

    private void Start()
    {
        ChangeSpeed();
        ChangeStability();
        ChangeDurability();
        ChangeArmor();
    }

    private void Update()
    {
        Debug.Log("Speed: " + Speed);
        Debug.Log("Stability: " + Stability);
        Debug.Log("Durability: " + Durability);
        Debug.Log("Armor: " + Armor);
        Debug.Log("HasChicken: " + HasChicken);
    }
    #endregion

    #region Funções Próprias
    public void ChangeSpeed(int value=0) 
    {
        Speed = Mathf.Clamp(Speed + value, minSpeed, maxSpeed);
    }

    public void ChangeStability(int value=0)
    {
        Stability = Mathf.Clamp(Stability + value, minStability, maxStability);
    }

    public void ChangeDurability(int value=0) 
    {
        Durability = Mathf.Clamp(Durability + value, minDurability, maxDurability);
    }

    public void ChangeArmor(int value=0) 
    {
        Armor = Mathf.Clamp(Armor + value, 0, maxArmor);
    }

    public void EnableChicken() 
    {
        HasChicken = true;
        //TODO: ATIVAR OBJETO DEPOIS
    }
    #endregion
}
