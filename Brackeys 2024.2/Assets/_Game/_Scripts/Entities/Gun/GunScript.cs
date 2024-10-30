using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class GunScript : MonoBehaviour
{
    #region Variáveis
    [Header("Stats:")]
    public int AmmoCount;
    public int MaxAmmo;

    [SerializeField] private float projectileSpeed = 60f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] private int projectileCount = 5;
    [SerializeField] private float spreadAngle = 15f;

    [Header("GFX:")]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private SpriteRenderer playerHeadSpr;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite shootSprite;

    [Header("Shake:")]
    [SerializeField] private float shootShakeIntensity;
    [SerializeField] private float shootShakeInterval;

    [Header("Referências:")]
    [SerializeField] private GameObject headHolder;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject capsulePrefab;
    [SerializeField] private Transform projectileSpawnpoint;
    [SerializeField] private Transform capsuleSpawnPoint;
    [SerializeField] private CameraShake cameraShake;

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
            ammoText.gameObject.SetActive(false);
        }
        else if (BalloonStats.HasGun)
        {
            gameObject.SetActive(true);
            ammoText.gameObject.SetActive(true);
        }

        _originalRotation = headHolder.transform.rotation;
    }

    private void Update()
    {
        SetAmmoText();

        if (!BalloonStats.HasGun) return;

        VerifyShootInput();
    }
    
    private void SetAmmoText()  => ammoText.text = (AmmoCount.ToString() + "/" + MaxAmmo.ToString());

    private void VerifyShootInput() 
    {
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            cameraShake.ApplyShake(shootShakeIntensity, shootShakeInterval);
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && AmmoCount == 0)
        {
            StartCoroutine(FlickerAmmoText(0.1f));
        }
    }

    private void Shoot()
    {
        if (AmmoCount <= MaxAmmo && AmmoCount > 0)
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

            Instantiate(capsulePrefab, capsuleSpawnPoint.position, Quaternion.identity);

            AmmoCount--;
        }

        if (AmmoCount == 0)
        {
            ammoText.color = Color.red;
            StartCoroutine(NoAmmoLeft());
        }
    }

    private IEnumerator ChangeSprite()
    {
        playerHeadSpr.sprite = shootSprite;

        yield return new WaitForSeconds(1f);

        playerHeadSpr.sprite = idleSprite;
        headHolder.transform.rotation = _originalRotation;
    }

    private IEnumerator ShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    private IEnumerator NoAmmoLeft()
    {
        yield return new WaitForSeconds(1f);
        _canShoot = false;
    }

    private IEnumerator FlickerAmmoText(float timeToFlicker)
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
        _target = FindObjectOfType<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(FindObjectOfType<Camera>().transform.position.z)));

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
