using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class GunScript : MonoBehaviour
{
    #region Variáveis
    [Header("Stats:")]
    [SerializeField] private int ammoCount;
    [SerializeField] private int maxAmmo;

    [SerializeField] private float projectileSpeed = 60f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] private int projectileCount = 5;
    [SerializeField] private float spreadAngle = 15f;

    [Header("GFX:")]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private SpriteRenderer playerHeadSpr;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite shootSprite;

    [Header("Referências:")]
    [SerializeField] private GameObject headHolder;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;

    private Vector3 _target;
    private float _upTreshold = 10f;
    private bool _canShoot = true;
    private Quaternion _originalRotation;
    #endregion

    private void Start()
    {
        if (!BalloonStats.HasGun)
        {
            gameObject.SetActive(false);
            ammoText.enabled = false;
        }
        else if (BalloonStats.HasGun)
        {
            gameObject.SetActive(true);
            ammoText.enabled = true;
        }

        _originalRotation = headHolder.transform.rotation;
    }

    private void Update()
    {
        ammoText.text = (ammoCount.ToString() + "/" + maxAmmo.ToString());

        _target = FindObjectOfType<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(FindObjectOfType<Camera>().transform.position.z)));

        if (!BalloonStats.HasGun)
        {
            return;
        }
        else if (BalloonStats.HasGun && Input.GetMouseButtonDown(0) && _canShoot)
        {
            Shoot();
        }
        else if (BalloonStats.HasGun && Input.GetMouseButtonDown(0) && ammoCount == 0)
        {
            StartCoroutine(FlickerAmmoText(0.1f));
        }
    }

    void Shoot()
    {
        if (ammoCount <= maxAmmo && ammoCount > 0)
        {
            ChangeHeadPosRot();

            for (int i = 0; i < projectileCount; i++)
            {
                float angle = Random.Range(-spreadAngle, spreadAngle);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * (Vector2)(_target - headHolder.transform.position).normalized;

                GameObject _projectile = Instantiate(projectilePrefab, projectileSpawnpoint.position, Quaternion.identity);
                _projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            }

            StartCoroutine(ChangeSprite());
            Invoke("ReloadSFX", 0.5f);
            StartCoroutine(ShootCooldown());
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            ammoCount--;
        }

        if (ammoCount == 0)
        {
            ammoText.color = Color.red;
            StartCoroutine(NoAmmoLeft());
        }
    }

    IEnumerator ChangeSprite()
    {
        playerHeadSpr.sprite = shootSprite;

        yield return new WaitForSeconds(1f);

        playerHeadSpr.sprite = idleSprite;
        headHolder.transform.rotation = _originalRotation;
    }

    IEnumerator ShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    IEnumerator NoAmmoLeft()
    {
        yield return new WaitForSeconds(1f);
        _canShoot = false;
    }

    IEnumerator FlickerAmmoText(float timeToFlicker)
    {
        ammoText.enabled = false;
        yield return new WaitForSeconds(timeToFlicker);
        ammoText.enabled = true;
        yield return new WaitForSeconds(timeToFlicker);
        ammoText.enabled = false;
        yield return new WaitForSeconds(timeToFlicker);
        ammoText.enabled = true;
    }

    private void ChangeHeadPosRot()
    {
        headHolder.transform.rotation = Quaternion.identity;

        if (_target.x > headHolder.transform.position.x)
        {
            //mouse na direita
            headHolder.transform.localScale = new Vector3(1, 1, 1);

            if (_target.y > headHolder.transform.position.y + _upTreshold)
            {
                //mouse encima
                headHolder.transform.localScale = new Vector3(1, 1, 1);
                headHolder.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }
        else if (_target.x < headHolder.transform.position.x)
        {
            //mouse na esquerda
            headHolder.transform.localScale = new Vector3(-1, 1, 1);

            if (_target.y > headHolder.transform.position.y + _upTreshold)
            {
                //mouse encima
                headHolder.transform.localScale = new Vector3(1, 1, 1);
                headHolder.transform.rotation = Quaternion.Euler(0f, -180f, 90f);
            }
        }
    }

    private void ReloadSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("reload");
    }
}
