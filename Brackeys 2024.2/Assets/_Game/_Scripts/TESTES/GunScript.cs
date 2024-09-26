using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{
    #region Variáveis
    [Header("Stats:")]
    //[SerializeField] private int projectileCount = 3;
    [SerializeField] private float projectileSpeed = 60f;
    //[SerializeField] private Vector3 offset = new Vector3(0f, 0.5f, 0f);

    [Header("GFX:")]
    [SerializeField] private SpriteRenderer playerHeadSpr;
    [SerializeField] private Sprite shootSprite;
    //[SerializeField] private Transform playerHeadTransform;
    [SerializeField] private GameObject crosshair;

    [Header("Referências:")]
    [SerializeField] private GameObject headHolder;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;
    private Vector3 _target;
    #endregion

    private void Start()
    {
        /*if (BalloonStats.HasGun)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);*/
    }

    private void Update()
    {
        Cursor.visible = false;

        _target = FindObjectOfType<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(FindObjectOfType<Camera>().transform.position.z)));
        crosshair.transform.position = new Vector2(_target.x, _target.y);

        Vector3 difference = _target - headHolder.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        headHolder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            Shoot(direction, rotationZ);

            Debug.Log("BALA NA AGULHA");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            Invoke("ReloadSFX", 0.5f);
            //Invoke("ResetSprite", 1f);
        }

        /*if (!BalloonStats.HasGun)
        {
            return;
        }
        else if (BalloonStats.HasGun && Input.GetMouseButton(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            Shoot(direction, rotationZ);

            Debug.Log("BALA NA AGULHA");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            Invoke("ReloadSFX", 0.5f);
            //Invoke("ResetSprite", 1f);
        }*/
    }

    void Shoot(Vector2 direction, float rotationZ)
    {
        GameObject _projectile = Instantiate(projectilePrefab) as GameObject;
        _projectile.transform.position = projectileSpawnpoint.transform.position;
        _projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        _projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    private void ReloadSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("reload");
    }
}
