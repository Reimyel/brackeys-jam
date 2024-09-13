using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BalloonChanger : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private string upgradeSceneName;
    [SerializeField] private bool isUI = false;

    [Header("UI:")]
    [SerializeField] private Image imgDurability;
    [SerializeField] private Image imgSpeed;
    [SerializeField] private Image imgStability;

    [Header("In Game:")]
    [SerializeField] private SpriteRenderer sprDurability;
    [SerializeField] private SpriteRenderer sprSpeed;
    [SerializeField] private SpriteRenderer sprStability;

    [Header("Sprites:")]
    [SerializeField] private Sprite[] spritesDurability;
    [SerializeField] private Sprite[] spritesSpeed;
    [SerializeField] private Sprite[] spritesStability;
    #endregion

    #region Fun��es Unity
    //private void Awake() => SetSprites();

    private void Update()
    {
        SetSprites();
    }
    #endregion

    #region Fun��es Pr�prias
    private void SetSprites()
    {
        if (!isUI)
        {
            sprDurability.sprite = spritesDurability[BalloonStats.DurabilityLevel];
            sprSpeed.sprite = spritesSpeed[BalloonStats.SpeedLevel];
            sprStability.sprite = spritesStability[BalloonStats.StabilityLevel];
        }
        else
        {
            imgDurability.sprite = spritesDurability[BalloonStats.DurabilityLevel-1];
            imgSpeed.sprite = spritesSpeed[BalloonStats.SpeedLevel-1];
            imgStability.sprite = spritesStability[BalloonStats.StabilityLevel-1];
        }
    }
    #endregion
}
