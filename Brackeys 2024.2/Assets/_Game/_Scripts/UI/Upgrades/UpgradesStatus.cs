using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradesStatus : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private TargetStats stats;

    [Header("Cores:")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disactiveSprite;

    [Header("Refer�ncias:")]
    [SerializeField] private Image[] levelImages;

    private int _currentLevel;

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
    private void Update() 
    {
        SelectStats();
        SetUpgradeLevel(); 
    }
    #endregion

    #region Fun��es Pr�prias
    private void SelectStats()
    {
        switch (stats) 
        {
            case TargetStats.Speed:
                _currentLevel = BaloonStats.SpeedLevel;
                break;

            case TargetStats.Stability:
                _currentLevel = BaloonStats.StabilityLevel;
                break;

            case TargetStats.Durability:
                _currentLevel = BaloonStats.DurabilityLevel;
                break;

            case TargetStats.Gun:
                if (BaloonStats.HasGun)
                    _currentLevel = 1;
                else
                    _currentLevel = 0;
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
                levelImages[i].sprite = activeSprite;
            else
                levelImages[i].sprite = disactiveSprite;
        }
    }
    #endregion
}
