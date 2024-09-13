using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonRagdoll : MonoBehaviour
{
    public Transform balloon;  // Refer�ncia ao bal�o
    public Transform basket;   // Refer�ncia ao cesto
    public Transform leftRopeAnchor;  // Ponto de ancoragem da corda esquerda no bal�o
    public Transform rightRopeAnchor; // Ponto de ancoragem da corda direita no bal�o
    public GameObject leftRopeSprite;  // Sprite da corda esquerda
    public GameObject rightRopeSprite; // Sprite da corda direita

    void Update()
    {
        UpdateRope(leftRopeAnchor, leftRopeSprite);
        UpdateRope(rightRopeAnchor, rightRopeSprite);
    }

    void UpdateRope(Transform ropeAnchor, GameObject ropeSprite)
    {
        Vector3 direction = basket.position - ropeAnchor.position;  // Dire��o da corda
        float distance = direction.magnitude;  // Comprimento da corda

        ropeSprite.transform.position = ropeAnchor.position + direction / 2f; // Posicionar a corda entre bal�o e cesto
        ropeSprite.transform.localScale = new Vector3(1, distance, 1); // Ajustar o comprimento da sprite

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // Calcular o �ngulo da corda
        ropeSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);  // Rotacionar a sprite para seguir a corda
    }
}
