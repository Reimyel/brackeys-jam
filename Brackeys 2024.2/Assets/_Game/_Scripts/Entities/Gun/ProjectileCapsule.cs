using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCapsule : MonoBehaviour
{
    [SerializeField] private float impulseForce;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        ApplyImpulse();
        Destroy(gameObject, lifeTime);
    }

    private void Update() => Rotate();
    private void Rotate() => transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

    private void ApplyImpulse() 
    {
        var direction = new Vector2(1, 1);
        if (Random.Range(0, 100) < 50)
            direction.x *= -1;

        GetComponent<Rigidbody2D>().AddForce(direction.normalized * impulseForce, ForceMode2D.Impulse);
    }
}
