using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMenu : MonoBehaviour
{
    private void EndAnimation() 
    {
        FindObjectOfType<BtnPlay>().transform.parent = null;
        GetComponent<Animator>().enabled = false;
    }
}
