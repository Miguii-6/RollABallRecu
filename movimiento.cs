using UnityEngine;
using System.Collections;

public class TransformFunctions : MonoBehaviour
{
    // Define una variable pública para la velocidad de movimiento
    public float moveSpeed = 10f;
    // Define una variable pública para la velocidad de giro
    public float turnSpeed = 50f;
    
    // Update es llamada una vez por frame
    void Update()
    {
        // Si se mantiene presionada la tecla de flecha hacia arriba
        if (Input.GetKey(KeyCode.UpArrow))
            // Mueve el objeto hacia adelante a la velocidad especificada
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
        // Si se mantiene presionada la tecla de flecha hacia abajo
        if (Input.GetKey(KeyCode.DownArrow))
            // Mueve el objeto hacia atrás a la velocidad especificada
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
        // Si se mantiene presionada la tecla de flecha hacia la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
            // Rota el objeto a la izquierda a la velocidad especificada
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
        // Si se mantiene presionada la tecla de flecha hacia la derecha
        if (Input.GetKey(KeyCode.RightArrow))
            // Rota el objeto a la derecha a la velocidad especificada
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }
}
