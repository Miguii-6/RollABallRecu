using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update se llama una vez por frame
    void Update()
    {
        // Rota el objeto en el que este script está adjunto
        // La rotación es de 15 grados por segundo alrededor del eje X, 
        // 30 grados por segundo alrededor del eje Y, y 45 grados por segundo alrededor del eje Z
        // Time.deltaTime se usa para hacer que la rotación sea independiente de la velocidad de fotogramas
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
