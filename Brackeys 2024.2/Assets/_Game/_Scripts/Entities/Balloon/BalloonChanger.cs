using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BalloonChanger : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private string upgradeSceneName;
    [SerializeField] private bool isUI = false;

    [Header("UI:")]
    [SerializeField] private Image imgDurability;
    [SerializeField] private Image imgSpeed;
    [SerializeField] private Image imgStability;

    [Header("In Game:")]
    [SerializeField] private SpriteRenderer sprDurability;
    [SerializeField] private SpriteRenderer[] sprSpeed;
    [SerializeField] private SpriteRenderer sprStability;
    [SerializeField] private SpriteRenderer sprDamageDurability;

    [Header("Sprites:")]
    [SerializeField] private Sprite[] spritesDurability;
    [SerializeField] private Sprite[] spritesSpeed;
    [SerializeField] private Sprite[] spritesStability;
    [SerializeField] private Sprite[] spritesDamageDurability;
    #endregion

    #region Funções Unity
    //private void Awake() => SetSprites();

    private void Update()
    {
        SetSprites();
    }
    #endregion

    #region Funções Próprias
    private void SetSprites()
    {
        if (!isUI)
        {
            sprDurability.sprite = spritesDurability[BalloonStats.DurabilityLevel];
            
            for (int i = 0; i < sprSpeed.Length; i++)
                sprSpeed[i].sprite = spritesSpeed[BalloonStats.SpeedLevel];

            sprStability.sprite = spritesStability[BalloonStats.StabilityLevel];
        }
        else
        {
            imgDurability.sprite = spritesDurability[BalloonStats.DurabilityLevel];

            imgSpeed.sprite = spritesSpeed[BalloonStats.SpeedLevel];

            imgStability.sprite = spritesStability[BalloonStats.StabilityLevel];

            if (BalloonStats.DurabilityLevel != 0 && BalloonStats.Durability - 1 <= 0)
                sprDamageDurability.sprite = spritesDamageDurability[BalloonStats.DurabilityLevel];
        }
    }
    #endregion
}
