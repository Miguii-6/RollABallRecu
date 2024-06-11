using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Declara una variable privada de tipo NavMeshAgent llamada pathfinder. NavMeshAgent es un componente que puede mover objetos a lo largo de la malla de navegación para alcanzar diferentes destinos.
    private NavMeshAgent pathfinder;

// Declara una variable privada de tipo Transform llamada target. Transform es una clase que almacena la posición, rotación y escala de un objeto.
    private Transform target;

// Declara una variable pública de tipo GameObject llamada enemyPrefab. Esta variable se puede asignar en el Inspector de Unity y se utilizará para instanciar nuevos enemigos.
    public GameObject enemyPrefab;
    
  // Declara una variable llamada 'anim' para referenciar el componente Animator del GameObject.
Animator anim;

// Declara una variable pública llamada 'nearThreshold' que representa el umbral de proximidad entre el enemigo y el jugador.
public float nearThreshold = 5.0f;

// Start es una función de Unity que se llama al inicio del juego.
    void Start()
    {
        // Obtiene el componente NavMeshAgent del objeto actual y lo asigna a la variable pathfinder.
        pathfinder = GetComponent<NavMeshAgent>();
        
        anim = GetComponent<Animator>();
        
        // Busca un objeto en la escena llamado "Player", obtiene su componente Transform y lo asigna a la variable target.
        target = GameObject.Find("Player").transform;

        // Llama a la función "SpawnEnemy" repetidamente cada 15 segundos, comenzando después de 15 segundos.
        InvokeRepeating("SpawnEnemy", 15f, 15f);
    }

}