using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private Attribute targetAttribute;
    [SerializeField] private float changeValue;

    private enum Attribute
    {
        Speed,
        Stability,
        Durability,
        Armor,
        Chicken
    }
    #endregion

    #region Fun��es Pr�prias
    private void Upgrade()
    {
        switch (targetAttribute) 
        {
            case Attribute.Speed:
                BaloonStats.Instance.ChangeSpeed(changeValue);
                break;

            case Attribute.Stability:
                BaloonStats.Instance.ChangeStability(changeValue);
                break;

            case Attribute.Durability:
                BaloonStats.Instance.ChangeDurability(changeValue);
                break;

            case Attribute.Armor:
                BaloonStats.Instance.ChangeArmor(changeValue);
                break;

            case Attribute.Chicken:
                BaloonStats.Instance.EnableChicken();
                break;
        }
    }
    #endregion
}
