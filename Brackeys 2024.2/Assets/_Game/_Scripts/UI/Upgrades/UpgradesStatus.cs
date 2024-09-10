using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesStatus : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]

    [Header("Atributo:")]
    [SerializeField] private TargetStats stats;

    [Header("Preço:")]
    [SerializeField] private int baseCost;
    [SerializeField] private int baseCostModifier;

    [Header("Cores:")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disactiveSprite;

    [Header("Referências:")]
    [SerializeField] private Image[] levelImages;

    private int _currentLevel;
    private int _currentCost;

    private enum TargetStats 
    {
        Speed,
        Stability,
        Durability,
        Gun,
        Chicken
    }
    #endregion

    #region Funções Unity
    private void Update() 
    {
        SelectStats();
        SetUpgradeLevel();
        SetCurrentCost();
    }
    #endregion

    #region Funções Próprias
    private void SelectStats()
    {
        switch (stats) 
        {
            case TargetStats.Speed:
                _currentLevel = BalloonStats.SpeedLevel;
                break;

            case TargetStats.Stability:
                _currentLevel = BalloonStats.StabilityLevel;
                break;

            case TargetStats.Durability:
                _currentLevel = BalloonStats.DurabilityLevel;
                break;

            case TargetStats.Gun:
                if (BalloonStats.HasGun)
                    _currentLevel = 1;
                else
                    _currentLevel = 0;
                break;

            case TargetStats.Chicken:
                if (BalloonStats.HasChicken)
                    _currentLevel = 1;
                else
                    _currentLevel = 0;
                break;
        }
    }

    private void SetUpgradeLevel() 
    {
        for (int i = 0; i < levelImages.Length; i++) 
        {
            if (i < _currentLevel)
                levelImages[i].sprite = activeSprite;
            else
                levelImages[i].sprite = disactiveSprite;
        }
    }

    public void ChangeDecimalStats(float value)
    {
        if (BalloonStats.CurrentMoney >= _currentCost) // Paga o Upgrade
            BalloonStats.CurrentMoney -= _currentCost;
        else // Ignore o Upgrade
            return;

        // Aplica o Upgrade

        switch (stats)
        {
            case TargetStats.Speed:
                BalloonStats.Instance.ChangeSpeed(value);
                break;

            case TargetStats.Stability:
                BalloonStats.Instance.ChangeStability(value);
                break;
        }
    }

    public void ChangeIntegerStats(int value)
    {
        if (BalloonStats.CurrentMoney >= _currentCost) // Paga o Upgrade
            BalloonStats.CurrentMoney -= _currentCost;
        else // Ignore o Upgrade
            return;

        // Aplica o Upgrade

        BalloonStats.Instance.ChangeDurability(value);
    }

    public void EnableConsumable() 
    {
        if (BalloonStats.CurrentMoney >= _currentCost) // Paga o Upgrade
            BalloonStats.CurrentMoney -= _currentCost;
        else // Ignore o Upgrade
            return;
        
        // Aplica o Upgrade
        switch (stats)
        {
            case TargetStats.Chicken:
                BalloonStats.Instance.EnableChicken();
                break;

            case TargetStats.Gun:
                BalloonStats.Instance.EnableGun();
                break;
        }
    }

    private void SetCurrentCost() => _currentCost = _currentLevel * baseCostModifier * baseCost;
    #endregion
}
