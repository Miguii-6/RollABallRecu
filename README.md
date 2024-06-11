# Roll a Ball en Unity

"Roll a Ball" es un proyecto introductorio en Unity que enseña a los desarrolladores principiantes cómo crear un juego simple. En este proyecto, los jugadores controlan una esfera que rueda sobre una superficie recolectando objetos. Y además hay un tutorial paso a para para hacer el Roll a Ball básico, que en cuestión es [Roll a ball](https://learn.unity.com/project/roll-a-ball). En mi proyecto tiene varias modificación que iré explicando a medida que vo haciendo junto al Readme, cada apartado será un script explicando que hace ese script.


## PlayerController
Este código gestiona el movimiento y las interacciones de un objeto esférico en un juego desarrollado en Unity, incluyendo la generación de pickups y columnas aleatorias, la actualización de un contador y la detección de colisiones.

### Variables

Aqui presento todas las variables que uso con el jugador.

```C#
private Rigidbody rb;
    private float movementX;
    private float movementY;
	
    private int count;
    public float speed = 0;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public GameObject pickupPrefab;
	public GameObject ColumnsPrefab;

	public int minPickups = 5; 
    public int maxPickups = 10; 
	private float jumpForce = 5.0f;
    private bool isJumping = false;
	// variable para el animator, para poder cambiar y obtener sus valores
	Animator anim;

```
### Función onJump

Funcion que buscara el input system cuando se le de a la barra espaciadora

```C#
void OnJump()
    {
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
		Debug.Log("Dije Salta!");
		isJumping = true;
		anim.SetBool("isJumping", true);
    }
```

### Función Start

La función Start() en el controlador del jugador configura la escena al inicio del juego, asignando componentes y variables necesarias. Luego, llama a métodos para generar objetos aleatorios y actualizar la interfaz de usuario. Finalmente, desactiva un objeto de texto de victoria hasta que se cumplan ciertas condiciones para ganar.

```C#
void Start()
    rb = GetComponent<Rigidbody>();
    Debug.Log("Hola, debugueando");
    anim = GetComponent<Animator>();
    count = 0;
    SpawnPickupsRandomly();
    SpawnColumnsRandomly();
    SetCountText();
    winTextObject.SetActive(false);
}
```

### Función CountPickupsLeft

Cuenta la cantidad de objetos con el tag "PickUp" que quedan en la escena

```C#

int CountPickupsLeft(string tag = "PickUp")
{
    GameObject[] pickups = GameObject.FindGameObjectsWithTag(tag);
    int pickupCount = pickups.Length;
    return pickupCount;
}
```

### Función FixedUpdate

El método `FixedUpdate()` controla la física del juego. Primero, calcula un vector de movimiento basado en las entradas de movimiento en X e Y. Luego, aplica una fuerza al Rigidbody en la dirección del movimiento multiplicada por la velocidad. Si la bola cae por debajo del plano, se reinicia su posición y velocidad. Además, si la bola está cerca del suelo, se asegura de que no esté en un estado de salto, cambiando las variables correspondientes.

```C#
private void FixedUpdate()
{
    Vector3 movement = new Vector3(movementX, 0.0f, movementY);
    rb.AddForce(movement * speed);
    if (transform.position.y < -10)
    {
        transform.position = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
    }
    if (transform.position.y < 0.5f)
    {
        isJumping = false;

        anim.SetBool("isJumping", false);
    }
}
```

### Función onMove

La función `OnMove()` se activa cuando hay movimiento en el input del jugador. Obtiene el vector de movimiento del input y lo muestra en la consola para depuración. Luego, asigna los valores de movimiento en X e Y a variables correspondientes.

```C#
void OnMove(InputValue movementValue)
{
    Vector2 movementVector = movementValue.Get<Vector2>();

    Debug.Log(movementVector);

    movementX = movementVector.x;
    movementY = movementVector.y;
}
```
### Función SetCountText

La función `SetCountText()` actualiza el texto del contador en la interfaz de usuario con el valor actual de 'count'. Luego, añade la cantidad de pickups restantes al texto. Si no quedan pickups, activa un objeto de texto de victoria en la interfaz de usuario.

```C#

void SetCountText()
{

    countText.text = "Count: " + count.ToString();
    countText.text = countText.text + "\nStillAlive: " + CountPickupsLeft().ToString();

    if (CountPickupsLeft() == 0)
    {
        winTextObject.SetActive(true);
    }
}
```

### Función onFire

Esta función se encarga de detectar cuando se presiona la barra espaciadora

```C#
void OnFire()
{
    Debug.Log("Fire!");
}
```

### Función OnTriggerEnter

El método `OnTriggerEnter(Collider other)` se activa cuando el objeto entra en colisión con otro objeto. Si el objeto colisionado tiene el tag "PickUp", se desactiva y se incrementa el contador de pickups. Además, se aumenta la velocidad de la bola y se actualiza el texto del contador. Si el objeto colisionado tiene el tag "Enemy" o "Columns", se reduce la velocidad de la bola.

```C#
void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("PickUp"))
    {
        other.gameObject.SetActive(false);
        count++;

        speed = speed + 1;

        SetCountText();
    }

    if (other.gameObject.CompareTag("Enemy"))
    {
        speed = speed - 1;
    }
    if (other.gameObject.CompareTag("Columns"))
    {
        speed = speed - 1;
    }
}
```
### Función SpawnPickupsRandomly

El método `SpawnPickupsRandomly()` genera pickups aleatoriamente en la escena. Primero, determina un número aleatorio de pickups dentro de un rango especificado. Luego, itera sobre este número y genera posiciones aleatorias dentro de un rango predefinido. Por último, instancia el prefab del pickup en las posiciones generadas.

```C#
void SpawnPickupsRandomly()
{

    int numPickupsToSpawn = Random.Range(minPickups, maxPickups + 1);
    for (int i = 0; i < numPickupsToSpawn; i++)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));

        Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
    }
}
```

## Movimiento

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