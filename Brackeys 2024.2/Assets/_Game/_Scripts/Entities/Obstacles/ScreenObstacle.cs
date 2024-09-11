using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ScreenObstacle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float disappearSpeed;

    private bool _canRotate = true;
    private Vector3 _rotateDir;

    // Componentes:
    private SpriteRenderer _spr;

    private void Start()
    {
        _rotateDir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2));
        _spr = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (_canRotate)
            Rotate();
        else 
            Disappear();       
    }

    private void Rotate() => transform.Rotate(_rotateDir * rotateSpeed * Time.deltaTime);

    private void DisableRotation()
    {
        _canRotate = false;
        transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        gameObject.GetComponent<Animator>().enabled = false;
        transform.localScale = new Vector3(10f, 10f, 1f);
    }

    private void Disappear() 
    {
        var color = _spr.color;

        var alpha = color.a;

        if (alpha > 0.0f)
        {
            alpha -= disappearSpeed * Time.deltaTime;
            transform.position += Vector3.down * disappearSpeed * Time.deltaTime;
            color.a = alpha;
            _spr.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
