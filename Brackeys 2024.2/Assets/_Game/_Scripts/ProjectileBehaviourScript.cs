using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cow"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}