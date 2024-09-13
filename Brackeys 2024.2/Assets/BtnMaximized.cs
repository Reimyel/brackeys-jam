using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMaximized : MonoBehaviour
{
    public void LockedSFX() 
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Locked");
    }
}
