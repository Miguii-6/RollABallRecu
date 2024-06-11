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

	