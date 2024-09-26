using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{
    #region Variáveis
    [Header("Stats:")]
    [SerializeField] private float projectileSpeed = 60f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] private int projectileCount = 5;
    [SerializeField] private float spreadAngle = 15f;

    [Header("GFX:")]
    [SerializeField] private SpriteRenderer playerHeadSpr;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite shootSprite;
    [SerializeField] private GameObject crosshair;

    [Header("Referências:")]
    [SerializeField] private GameObject headHolder;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;

    private Vector3 _target;
    private float upTreshold = 10f;
    private bool canShoot = true;
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
        _target = FindObjectOfType<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(FindObjectOfType<Camera>().transform.position.z)));
        crosshair.transform.position = new Vector2(_target.x, _target.y);

        Vector3 difference = _target - headHolder.transform.position;

        if (_target.y > headHolder.transform.position.y + upTreshold)
        {
            //mouse encima
            headHolder.transform.localScale = new Vector3(1, 1, 1);
            headHolder.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            headHolder.transform.rotation = Quaternion.identity;

            if (_target.x > headHolder.transform.position.x)
            {
                //mouse na direita
                headHolder.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_target.x < headHolder.transform.position.x)
            {
                //mouse na esquerda
                headHolder.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference.normalized;
            direction.Normalize();
            Shoot(direction);

            Debug.Log("BALA NA AGULHA");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            StartCoroutine(ChangeSprite());
            Invoke("ReloadSFX", 0.5f);

            StartCoroutine(ShootCooldown());
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

    void Shoot(Vector2 direction)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle); //dispersão aleatória definida
            Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * direction;

            GameObject _projectile = Instantiate(projectilePrefab) as GameObject;
            _projectile.transform.position = projectileSpawnpoint.transform.position;
            _projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }
    }

    IEnumerator ChangeSprite()
    {
        playerHeadSpr.sprite = shootSprite;

        yield return new WaitForSeconds(1f);

        playerHeadSpr.sprite = idleSprite;
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void ReloadSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("reload");
    }
}
