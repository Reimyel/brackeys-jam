using System.Collections;
using UnityEngine;

public class ProjectileBehaviourScript : MonoBehaviour
{
    #region Variáveis
    [SerializeField] private Color fadeColor;
    [SerializeField] private FadeVFX fadePrefab;
    #endregion

    #region Funções Unity
    private void Start() => StartCoroutine(ApplyEffect(0.01f));

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            // Chance de recuperar uma munição
            if (Random.Range(0, 100) <= 25)
            {
                var gunScript = FindObjectOfType<GunScript>();
                
                if (gunScript.AmmoCount < gunScript.MaxAmmo)
                    gunScript.AmmoCount++;
            }
            
            // Destruindo Alvo e Projétil
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        StartCoroutine(DestroyProjectile(3f));
    }
    #endregion

    #region Funções Próprias
    private IEnumerator DestroyProjectile(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private IEnumerator ApplyEffect(float t)
    {
        yield return new WaitForSeconds(t);
        var effect = Instantiate(fadePrefab, transform.position, Quaternion.identity);
        var effectSpr = effect.GetComponent<SpriteRenderer>();

        // Configurando Sprite do Fade
        effectSpr.sprite = GetComponent<SpriteRenderer>().sprite;
        effectSpr.color = fadeColor;
        effectSpr.gameObject.transform.localScale = gameObject.transform.localScale;

        // Colocando a refer�ncia para rota��o
        effect.GetComponent<FadeVFX>().RotationParent = gameObject.transform;

        StartCoroutine(ApplyEffect(0.01f));
    }
    #endregion
}
