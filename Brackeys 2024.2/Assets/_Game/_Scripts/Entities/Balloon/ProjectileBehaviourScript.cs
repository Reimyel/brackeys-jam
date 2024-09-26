using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        StartCoroutine(DestroyProjectile(3f));
    }

    IEnumerator DestroyProjectile(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
