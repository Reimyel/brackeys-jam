using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAroundScript : MonoBehaviour
{
    #region Variáveis
    private Camera _mainCamera;
    private Vector2 _screenBounds;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _mainCamera = Camera.main;
        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
    }

    private void Update()
    {
        Vector3 objectPosition = transform.position;

        if (objectPosition.x > _screenBounds.x)
        {
            objectPosition.x = -_screenBounds.x;
        }

        else if (objectPosition.x < -_screenBounds.x)
        {
            objectPosition.x = _screenBounds.x;
        }

        transform.position = objectPosition;
    }
    #endregion
}
