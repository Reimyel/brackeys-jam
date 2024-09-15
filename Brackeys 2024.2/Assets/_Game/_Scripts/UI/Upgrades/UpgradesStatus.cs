using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject btnMaximized;

    [Header("Descrição:")]
    [SerializeField] private TextMeshProUGUI txtCurrentUpgrade;
    [SerializeField] private TextMeshProUGUI txtNextUpgrade;
    [SerializeField] private TextMeshProUGUI txtNextCost;
    [SerializeField] private string[] txtUpgrades;

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
        SetUpgradeTexts();
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

    private void SetCurrentCost()
    {
        _currentCost = (_currentLevel + 1) * baseCostModifier * baseCost;
    }
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

    private void SetUpgradeTexts() 
    {
        txtNextCost.text = _currentCost.ToString();

        if (stats == TargetStats.Gun || stats == TargetStats.Chicken) return;

        if (txtCurrentUpgrade.enabled && txtNextUpgrade.enabled) 
        {
            switch (stats)
            {
                case TargetStats.Speed:
                    txtCurrentUpgrade.text = "Current: \n" + txtUpgrades[BalloonStats.SpeedLevel];

                    if (BalloonStats.SpeedLevel < 3)
                        txtNextUpgrade.text = "Next: \n" + txtUpgrades[BalloonStats.SpeedLevel + 1];
                    break;

                case TargetStats.Stability:
                    txtCurrentUpgrade.text = "Current: \n" + txtUpgrades[BalloonStats.StabilityLevel];
                    
                    if (BalloonStats.StabilityLevel < 3)
                        txtNextUpgrade.text = "Next: \n" + txtUpgrades[BalloonStats.StabilityLevel + 1];
                    break;

                case TargetStats.Durability:
                    txtCurrentUpgrade.text = "Current: \n" + txtUpgrades[BalloonStats.DurabilityLevel];

                    if (BalloonStats.DurabilityLevel < 3)
                        txtNextUpgrade.text = "Next: \n" + txtUpgrades[BalloonStats.DurabilityLevel + 1];
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
