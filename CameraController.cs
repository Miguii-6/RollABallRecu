using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Este script se encarga de controlar la cámara en un Roll a Ball
 */
public class CameraController : MonoBehaviour
{
    // Variable pública para referenciar al objeto jugador
    public GameObject player;
    
    // Variable privada para almacenar la distancia (desplazamiento) entre la cámara y el jugador
    private Vector3 offset;
    
    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        // Calcula y almacena el desplazamiento entre la posición inicial de la cámara y la del jugador
        offset = transform.position - player.transform.position;
    }

    // LateUpdate se llama una vez por frame, después de todas las actualizaciones
    void LateUpdate()
    {
        // Actualiza la posición de la cámara para que siga al jugador manteniendo el desplazamiento constante
        transform.position = player.transform.position + offset;
    }
}
