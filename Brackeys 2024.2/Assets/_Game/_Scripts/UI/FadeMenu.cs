using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMenu : MonoBehaviour
{
    [SerializeField] private GameObject imgBtnPlay;
    [SerializeField] private GameObject btnPlayParent;

    private void EndAnimation()
    {
        imgBtnPlay.SetActive(false);
        btnPlayParent.SetActive(true);
    }
}
