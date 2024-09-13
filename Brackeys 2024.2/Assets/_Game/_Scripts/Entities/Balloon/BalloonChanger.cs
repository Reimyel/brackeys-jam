using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonChanger : MonoBehaviour
{
    #region Variáveis
    [Header("Configurações:")]
    [SerializeField] private string upgradeSceneName;

    [SerializeField] private SpriteRenderer sprDurability;
    [SerializeField] private SpriteRenderer sprSpeed;
    [SerializeField] private SpriteRenderer sprStability;

    [SerializeField] private Sprite[] spritesDurability;
    [SerializeField] private Sprite[] spritesSpeed;
    [SerializeField] private Sprite[] spritesStability;
    #endregion

    #region Funções Unity
    private void Awake() => SetSprites();

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "UpgradeScene")
            SetSprites();
    }
    #endregion

    #region Funções Próprias
    private void SetSprites()
    {
        sprDurability.sprite = spritesDurability[BalloonStats.DurabilityLevel];
        sprSpeed.sprite = spritesSpeed[BalloonStats.SpeedLevel];
        sprStability.sprite = spritesStability[BalloonStats.StabilityLevel];
    }
    #endregion
}
