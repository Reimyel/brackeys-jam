using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField] private float upSpeed;
    [SerializeField] private float horizontalSpeed;

    private Rigidbody2D _rb;

    private float dirX;

    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(0, upSpeed);

        dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(dirX * horizontalSpeed, _rb.velocity.y);
 
    }
}
