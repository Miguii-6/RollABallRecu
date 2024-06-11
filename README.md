# Roll a Ball en Unity

"Roll a Ball" es un proyecto introductorio en Unity que enseña a los desarrolladores principiantes cómo crear un juego simple. En este proyecto, los jugadores controlan una esfera que rueda sobre una superficie recolectando objetos. Y además hay un tutorial paso a para para hacer el Roll a Ball básico, que en cuestión es [Roll a ball](https://learn.unity.com/project/roll-a-ball). En mi proyecto tiene varias modificación que iré explicando a medida que vo haciendo junto al Readme, cada apartado será un script explicando que hace ese script.

## Movimiento.cs

Este script de Unity controla el movimiento y la rotación de un objeto en función de las teclas de flecha presionadas por el usuario. Utiliza transform.Translate para mover el objeto hacia adelante y hacia atrás, y transform.Rotate para girarlo hacia la izquierda y la derecha. Las velocidades de movimiento y giro son ajustables mediante las variables moveSpeed y turnSpeed, respectivamente. 

```C#
public class TransformFunctions : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    
    
    void Update ()
    {
        if(Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }
}
```

## CameraController

Este script controla la cámara en un juego tipo "Roll a Ball". La cámara sigue al jugador manteniendo una distancia constante (desplazamiento) calculada al inicio del juego.

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Este script se encarga de controlar la cámara en un Roll a Ball
 */
public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        
    }
}

```
## Rotator

Este script controla la rotación de un objeto en Unity. La rotación se realiza continuamente en cada frame. En este caso la rotación esta asignada a las "monedas" que son los cuadraditos azules.

```C#

public class Rotator : MonoBehaviour
{
    

    void Update()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}

```