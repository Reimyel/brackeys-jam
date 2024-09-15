using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtThankYou : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Start() => Invoke("EnableAnimator", 20f);

    private void EnableAnimator() => anim.enabled = true;
}
