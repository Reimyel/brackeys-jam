using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviourScript : MonoBehaviour
{
    #region Variáveis
    [Header("Shoot Sprite:")]
    [SerializeField] private SpriteRenderer playerHeadSpr;
    [SerializeField] private Sprite shootSprite;
    [SerializeField] private Transform playerHeadTransform;

    [SerializeField] private int projectileCount = 3;
    [SerializeField] private int projectileSpeed;

    [Header("Referências:")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;
    
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.5f, 0f);
    #endregion

    #region Funções Unity
    private void Start()
    {
        if (BalloonStats.HasGun) 
            playerHeadSpr.sprite = shootSprite;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cow") || other.gameObject.CompareTag("Chicken") && BalloonStats.HasGun)
        {
            Debug.Log("BALA NA AGULHA");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            var direction = ((Vector2)other.gameObject.transform.position - (Vector2)playerHeadTransform.position).normalized;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            playerHeadTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Invoke("ReloadSFX", 0.5f);
            Invoke("ResetSprite", 1f);

            /*
            if (other.gameObject.transform.position.x > gameObject.transform.position.x)
                playerHeadSpr.flipX = false;
            else
                playerHeadSpr.flipX = true;
            */

            //Destroy(other.gameObject);

            
            for (int i = 0; i <  projectileCount; i++) 
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnpoint.position + offset, Quaternion.identity);
                Vector3 directionBullet = (other.transform.position - transform.position).normalized;

                projectile.GetComponent<Rigidbody2D>().velocity = directionBullet * projectileSpeed;
            }
        }
    }
    #endregion

    #region Funções Próprias
    private void ReloadSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("reload");
    }

    private void ResetSprite()
    {
        playerHeadSpr.flipX = false;
        playerHeadTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    #endregion
}
