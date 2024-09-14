using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviourScript : MonoBehaviour
{
    #region Variáveis
    [Header("Shoot Sprite:")]
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
            playerHeadTransform.gameObject.GetComponent<SpriteRenderer>().sprite = shootSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cow") && BalloonStats.HasGun)
        {
            Debug.Log("BALA NA AGULHA");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            Invoke("ReloadSFX", 0.5f);
            Invoke("ResetSprite", 1f);

            for (int i = 0; i <  projectileCount; i++) 
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnpoint.position + offset, Quaternion.identity);
                Vector3 direction = (other.transform.position - transform.position).normalized;
                playerHeadTransform.rotation = Quaternion.LookRotation(direction);

                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
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

    private void ResetSprite() => playerHeadTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    #endregion
}
