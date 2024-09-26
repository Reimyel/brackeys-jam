using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    #region Variáveis
    [Header("Referências:")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickCursor;
    [SerializeField] private Texture2D crosshairCursor;
    #endregion

    #region Funções Unity
    private void Start()
    {
        if (SceneManager.GetSceneByName("JOGO").isLoaded && BalloonStats.HasGun)
        {
            ShowCrosshair();
            SetCrosshair();
        }
        else if (SceneManager.GetSceneByName("JOGO").isLoaded && !BalloonStats.HasGun)
        {
            HideCrosshair();
        }
        else
        {
            ShowCrosshair();
            SetDefault();
        }
    }

    private void Update()
    {
        if (SceneManager.GetSceneByName("JOGO").isLoaded && BalloonStats.HasGun)
        {
            ShowCrosshair();

            if (Input.GetMouseButton(0))
                SetCrosshair();

            if (Input.GetMouseButtonUp(0))
                SetCrosshair();
        }
        else if (SceneManager.GetSceneByName("JOGO").isLoaded && !BalloonStats.HasGun)
        {
            HideCrosshair();
        }
        else
        {
            ShowCrosshair();

            if (Input.GetMouseButton(0))
                SetClick();

            if (Input.GetMouseButtonUp(0))
                SetDefault();
        }
    }
    #endregion

    #region Funções Próprias
    private void SetDefault() => Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

    private void SetClick() => Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);

    private void SetCrosshair() => Cursor.SetCursor(crosshairCursor, Vector2.zero, CursorMode.Auto);

    private void HideCrosshair() => Cursor.visible = false;

    private void ShowCrosshair() => Cursor.visible = true;
    #endregion
}
