using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonPlay : MonoBehaviour
{
    [Header("Configurações:")]
    [Header("UI:")]
    [SerializeField] private Image imgDurability;
    [SerializeField] private Image[] imgRopes;
    [SerializeField] private Image imgStability;
    [SerializeField] private Image imgPlayerHead;
    [SerializeField] private Image imgChicken;

    [Header("Sprites:")]
    [SerializeField] private Sprite[] spritesDurability;
    [SerializeField] private Sprite[] spritesRopes;
    [SerializeField] private Sprite[] spritesStability;
    [SerializeField] private Sprite[] spritesPlayerHead;

    private void OnEnable()
    {
        imgDurability.sprite = spritesDurability[BalloonStats.DurabilityLevel];

        for (int i = 0; i < imgRopes.Length; i++)
            imgRopes[i].sprite = spritesRopes[BalloonStats.DurabilityLevel];

        imgStability.sprite = spritesStability[BalloonStats.StabilityLevel];

        if (BalloonStats.HasGun)
            imgPlayerHead.sprite = spritesPlayerHead[1];
        else
            imgPlayerHead.sprite = spritesPlayerHead[0];

        if (BalloonStats.HasChicken)
            imgChicken.enabled = true;
    }
}
