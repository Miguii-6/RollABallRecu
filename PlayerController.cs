using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
/*
 * Este script se encarga de controlar al jugador en un Roll a Ball
 */
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
	
	// variable para el puntaje de la pelota
	private int count;
    
    public float speed = 0;

	// variable para el texto del puntaje
	public TextMeshProUGUI countText;

	// variable para el texto de victoria
	public GameObject winTextObject;

	// para el prefab de los pickups
	public GameObject pickupPrefab;

	// para el prefab de las columnas
	public GameObject ColumnsPrefab;

	public int minPickups = 5; // Minimum number of pickups to spawn
    public int maxPickups = 10; // limite superior de pickups a spawnear
	
	// fuerza del salto
	private float jumpForce = 5.0f;
	
	// variable local para saber si la pelota está saltando
    private bool isJumping = false;

	// variable para el animator, para poder cambiar y obtener sus valores
	Animator anim;

	void OnJump()
    {
		// le damos un impulso hacia arriba
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        // para ver luego en el debug
		Debug.Log("Dije Salta!");
		// cambiamos el valor de la variable isJumping  a true
		isJumping = true;
		// cambiamos el valor de la variable isJumping en el animator a true
		anim.SetBool("isJumping", true);
    }
        // Start is called before the first frame update
void Start()
{
    // Obtiene el componente Rigidbody del objeto actual y lo asigna a la variable 'rb'
    rb = GetComponent<Rigidbody>();

    // Muestra un mensaje en la consola para depuración
    Debug.Log("Hola, debugueando");

    // Obtiene el componente Animator del objeto actual y lo asigna a la variable 'anim'
    anim = GetComponent<Animator>();

    // Inicializa la variable 'count' a 0
    count = 0;

    // Llama al método para generar pickups aleatoriamente en la escena
    SpawnPickupsRandomly();

    // Llama al método para generar columnas aleatoriamente en la escena
    SpawnColumnsRandomly();

    // Llama al método para actualizar el texto del contador
    SetCountText();

    // Desactiva el objeto de texto de victoria
    winTextObject.SetActive(false);
}

// Cuenta la cantidad de objetos con el tag "PickUp" que quedan en la escena
int CountPickupsLeft(string tag = "PickUp")
{
    // Encuentra todos los objetos en la escena con el tag "PickUp"
    GameObject[] pickups = GameObject.FindGameObjectsWithTag(tag);

    // Obtiene la cantidad de pickups encontrados
    int pickupCount = pickups.Length;

    // Retorna la cantidad de pickups
    return pickupCount;
}


// Se llama a este método a intervalos fijos y se utiliza para la física del juego
private void FixedUpdate()
{
    // Crea un vector de movimiento a partir de los valores de movimiento en X y Y
    Vector3 movement = new Vector3(movementX, 0.0f, movementY);

    // Añade una fuerza al Rigidbody en la dirección del movimiento multiplicada por la velocidad
    rb.AddForce(movement * speed);

    // Si la bola cae por debajo del plano, se reinicia su posición y velocidad
    if (transform.position.y < -10)
    {
        transform.position = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
    }

    // Si la bola está cerca del suelo, se asegura que no esté en estado de salto
    if (transform.position.y < 0.5f)
    {
        // Cambia la variable 'isJumping' a falso
        isJumping = false;

        // Cambia la variable 'isJumping' en el animator a falso
        anim.SetBool("isJumping", false);
    }
}

// Método llamado cuando hay movimiento en el input del jugador
void OnMove(InputValue movementValue)
{
    // Obtiene el vector de movimiento del input
    Vector2 movementVector = movementValue.Get<Vector2>();

    // Muestra el vector de movimiento en la consola para depuración
    Debug.Log(movementVector);

    // Asigna los valores de movimiento en X y Y
    movementX = movementVector.x;
    movementY = movementVector.y;
}

// Actualiza el texto del contador y verifica si se ha ganado el juego
void SetCountText()
{
    // Actualiza el texto del contador con el valor actual de 'count'
    countText.text = "Count: " + count.ToString();

    // Añade al texto la cantidad de pickups que quedan
    countText.text = countText.text + "\nStillAlive: " + CountPickupsLeft().ToString();

    // Si no quedan pickups, muestra el texto de victoria
    if (CountPickupsLeft() == 0)
    {
        winTextObject.SetActive(true);
    }
}

/*
 * Este método se encarga de detectar cuando se presiona la barra espaciadora
 */
void OnFire()
{
    // Muestra un mensaje en la consola cuando se presiona el botón de fuego
    Debug.Log("Fire!");
}

// Método llamado cuando el objeto entra en colisión con otro objeto
void OnTriggerEnter(Collider other)
{
    // Si el objeto con el que colisiona tiene el tag "PickUp"
    if (other.gameObject.CompareTag("PickUp"))
    {
        // Desactiva el objeto "PickUp"
        other.gameObject.SetActive(false);

        // Incrementa el contador de pickups
        count++;

        // Aumenta la velocidad de la bola
        speed = speed + 1;

        // Actualiza el texto del contador
        SetCountText();
    }

    // Si el objeto con el que colisiona tiene el tag "Enemy"
    if (other.gameObject.CompareTag("Enemy"))
    {
        // Disminuye la velocidad de la bola
        speed = speed - 1;
    }

    // Si el objeto con el que colisiona tiene el tag "Columns"
    if (other.gameObject.CompareTag("Columns"))
    {
        // Disminuye la velocidad de la bola
        speed = speed - 1;
    }
}

// Método para generar pickups aleatoriamente en la escena
void SpawnPickupsRandomly()
{
    // Genera un número aleatorio de pickups dentro de un rango especificado
    int numPickupsToSpawn = Random.Range(minPickups, maxPickups + 1);

    // Itera sobre el número de pickups a generar
    for (int i = 0; i < numPickupsToSpawn; i++)
    {
        // Genera una posición aleatoria dentro de un rango especificado
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));

        // Instancia el prefab del pickup en la posición generada
        Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
    }
}