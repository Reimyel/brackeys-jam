using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviourScript : MonoBehaviour
{
    [SerializeField] private int projectileSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;
    GameObject _cowObject;

    private void Start()
    {
        _cowObject = GameObject.FindGameObjectWithTag("Cow");
    }

    private void Update()
    {
        if (_cowObject != null)
        {
            transform.LookAt(_cowObject.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _cowObject)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnpoint.position, Quaternion.identity);

            Vector3 direction = (other.transform.position - transform.position).normalized;

            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }
    }
}
