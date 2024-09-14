using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonRagdoll : MonoBehaviour
{
    public Transform[] balloonAnchors; 
    public Transform[] basketAnchors; 
    public Transform[] ropeSprites; 

    void Update()
    {
        for (int i = 0; i < ropeSprites.Length; i++)
        {
            //Ajusta a posição da corda para estar entre os pontos de ancoragem
            ropeSprites[i].position = (balloonAnchors[i].position + basketAnchors[i].position) / 2;

            //Calcula a direção entre os pontos de ancoragem
            Vector2 direction = (basketAnchors[i].position - balloonAnchors[i].position).normalized;

            //Ajusta a rotação da corda para apontar na direção correta
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ropeSprites[i].rotation = Quaternion.Euler(0, 0, angle - 90); // Subtrai 90 para alinhar corretamente
        }
    }
}
