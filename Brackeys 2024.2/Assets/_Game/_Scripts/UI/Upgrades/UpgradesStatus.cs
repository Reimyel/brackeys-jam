using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradesStatus : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private TargetStats stats;

    [Header("Cores:")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color disactiveColor;

    [Header("Referências:")]
    [SerializeField] private Image[] levelImages;

    private int _currentLevel;

    private enum TargetStats 
    {
        Speed,
        Stability,
        Durability,
        Armor,
        Chicken
    }
    #endregion

    #region Funções Unity
    private void Update() 
    {
        SelectStats();
        SetUpgradeLevel(); 
    }
    #endregion

    #region Funções Próprias
    private void SelectStats()
    {
        switch (stats) 
        {
            case TargetStats.Speed:
                _currentLevel = BaloonStats.Speed;
                break;

            case TargetStats.Stability:
                _currentLevel = BaloonStats.Stability;
                break;

            case TargetStats.Durability:
                _currentLevel = BaloonStats.Durability;
                break;

            case TargetStats.Armor:
                _currentLevel = BaloonStats.Armor;
                break;

            case TargetStats.Chicken:
                if (BaloonStats.HasChicken)
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
                levelImages[i].color = activeColor;
            else
                levelImages[i].color = disactiveColor;
        }
    }
    #endregion
}
