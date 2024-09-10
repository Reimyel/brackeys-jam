using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeVFX : MonoBehaviour
{
    #region Vari�veis
    [Header("Configura��es:")]
    [SerializeField] private FadeType type;
    [SerializeField] private float speed;
    [SerializeField] private bool isUI;
    
    // Refer�ncias:
    [HideInInspector] public Transform RotationParent;

    private enum FadeType
    {
        FadeOut,
        FadeIn
    }

    // Componentes:
    private SpriteRenderer _spr;
    private Image _img;
    #endregion

    #region Fun��es Unity
    private void Start()
    {
        if (isUI) _img = GetComponent<Image>();
        else _spr = GetComponent<SpriteRenderer>();

        gameObject.transform.rotation = RotationParent.rotation;
    }

    private void Update()
    {
        ApplyFade();
        gameObject.transform.rotation = RotationParent.rotation;
    }
    #endregion

    #region Fun��es Pr�prias
    private void ApplyFade()
    {
        Color color;
        if (isUI) color = _img.color;
        else color = _spr.color;

        var alpha = color.a;

        if (type == FadeType.FadeOut)
        {
            if (alpha > 0.0f)
            {
                alpha -= speed * Time.deltaTime;
                color.a = alpha;
                if (isUI) _img.color = color;
                else _spr.color = color;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (alpha < 1.0f)
            {
                alpha += speed * Time.deltaTime;
                color.a = alpha;
                if (isUI) _img.color = color;
                else _spr.color = color;
            }
            else
            {
                this.enabled = false;
            }
        }
    }
    #endregion
}