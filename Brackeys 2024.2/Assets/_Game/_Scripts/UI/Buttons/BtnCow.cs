using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCow : MonoBehaviour
{
    public void SFX() 
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Cow" + Random.Range(1, 6));
    }
}
