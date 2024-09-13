using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesStatus : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]

    [Header("Atributo:")]
    [SerializeField] private TargetStats stats;

    [Header("Pre�o:")]
    [SerializeField] private int baseCost;
    [SerializeField] private int baseCostModifier;

    [Header("Cores:")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disactiveSprite;

    [Header("Refer�ncias:")]
    [SerializeField] private Image[] levelImages;
    [SerializeField] private GameObject btnMaximized;

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

    #region Fun��es Unity
    private void Start()
    {
        if (IsMaximized())
            DisableBtn();
    }

    private void Update() 
    {
        if (IsMaximized())
            DisableBtn();

        SelectStats();
        SetUpgradeLevel();
        SetCurrentCost();
    }
    #endregion

    #region Fun��es Pr�prias
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
        // Ignore o Upgrade
        if (BalloonStats.CurrentMoney <= _currentCost) 
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Locked");
            return;
        }
        else 
        {
            BalloonStats.CurrentMoney -= _currentCost;
            UpgradeSFX();
        }

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
        // Ignore o Upgrade
        if (BalloonStats.CurrentMoney <= _currentCost)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Locked");
            return;
        }
        else
        {
            BalloonStats.CurrentMoney -= _currentCost;
            UpgradeSFX();
        }

        // Aplica o Upgrade

        BalloonStats.Instance.ChangeDurability(value);
    }

    public void EnableConsumable() 
    {
        // Ignore o Upgrade
        if (BalloonStats.CurrentMoney <= _currentCost)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Locked");
            return;
        }
        else
        {
            BalloonStats.CurrentMoney -= _currentCost;
            UpgradeSFX();
        }

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
    
    private bool IsMaximized() 
    {
        // Verificando
        switch (stats)
        {
            case TargetStats.Speed:
                if (BalloonStats.Speed >= BalloonStats.Instance.MaxSpeed)
                    return true;
                break;

            case TargetStats.Stability:
                if (BalloonStats.Stability >= BalloonStats.Instance.MaxStability)
                    return true;
                break;

            case TargetStats.Durability:
                if (BalloonStats.Durability >= BalloonStats.Instance.MaxDurability)
                    return true;
                break;

            case TargetStats.Chicken:
                if (BalloonStats.HasChicken)
                    return true;
                break;

            case TargetStats.Gun:
                if (BalloonStats.HasGun)
                    return true;
                break;
        }

        return false;
    }

    private void UpgradeSFX() 
    {
        if (AudioManager.Instance != null) 
        {
            AudioManager.Instance.PlaySFX("Upgrade");

            switch (stats)
            {
                case TargetStats.Speed:
                    if (_currentLevel < 2)
                        AudioManager.Instance.PlaySFX("Upgrade1");
                    else
                        AudioManager.Instance.PlaySFX("Upgrade2");
                    break;

                case TargetStats.Stability:
                    if (_currentLevel < 2)
                        AudioManager.Instance.PlaySFX("Upgrade1");
                    else
                        AudioManager.Instance.PlaySFX("Upgrade2");
                    break;

                case TargetStats.Durability:
                    if (_currentLevel < 2)
                        AudioManager.Instance.PlaySFX("Upgrade1");
                    else
                        AudioManager.Instance.PlaySFX("Upgrade2");
                    break;

                case TargetStats.Chicken:
                    AudioManager.Instance.PlaySFX("Upgrade2");
                    break;

                case TargetStats.Gun:
                    AudioManager.Instance.PlaySFX("Upgrade2");
                    break;
            }
        }
    }

    private void DisableBtn() 
    {
        btnMaximized.SetActive(true);
        gameObject.SetActive(false);
    }
    #endregion
}
