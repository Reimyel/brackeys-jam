using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    #region Vari�veis
    [Header("Refer�ncias:")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickCursor;
    #endregion

    #region Fun��es Unity
    private void Awake() => SetDefault();

    private void Update()
    {
        if (Input.GetMouseButton(0))
            SetClick();

        if (Input.GetMouseButtonUp(0))
            SetDefault();
    }
    #endregion

    #region Fun��es Pr�prias
    private void SetDefault() => Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

    private void SetClick() => Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
    #endregion
}
