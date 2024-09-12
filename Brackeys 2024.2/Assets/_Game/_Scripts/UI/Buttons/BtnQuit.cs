using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuit : MonoBehaviour
{
    private void Quit() 
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Saindo1");

        Invoke("QuitInterval", 2.25f);
    }

    private void QuitInterval() => Application.Quit();
}
