using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviourScript : MonoBehaviour
{
    [SerializeField] private Color fadeColor;
    [SerializeField] private FadeVFX fadePrefab;

    private void Start()
    {
        StartCoroutine(ApplyEffect(0.01f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        StartCoroutine(DestroyProjectile(3f));
    }

    IEnumerator DestroyProjectile(float time)
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

        // Colocando a referência para rotação
        effect.GetComponent<FadeVFX>().RotationParent = gameObject.transform;

        StartCoroutine(ApplyEffect(0.01f));
    }
}
