using System;
using Cinemachine;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f; // Velocidad de movimiento
    [SerializeField]
    private float scrollSpeed = 5f; // Velocidad de movimiento en el eje Z con el scroll
    [SerializeField]
    private float smoothTime = 0.2f; // Tiempo de suavizado para el movimiento

    private Vector3 targetPosition; // Posición objetivo de la cámara
    private Vector3 velocity = Vector3.zero; // Velocidad para suavizar el movimiento

    private void Awake()
    {
        targetPosition = transform.position; // Inicializar la posición objetivo
    }

    private void Update()
    {
        HandleMovementInput();
        HandleScrollInput();
        SmoothMove();
    }

    private void HandleMovementInput()
    {
        // Movimiento en los ejes X y Y con WASD o flechas
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            targetPosition += movementSpeed * Time.deltaTime * Vector3.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            targetPosition += movementSpeed * Time.deltaTime * - Vector3.up;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            targetPosition += movementSpeed * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            targetPosition += movementSpeed * Time.deltaTime * Vector3.right;
        }
    }

    private void HandleScrollInput()
    {
        // Movimiento en el eje Z con el scroll del mouse
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            targetPosition += scrollDelta * scrollSpeed * Time.deltaTime * Vector3.forward;
        }
    }

    private void SmoothMove()
    {
        // Suavizar el movimiento hacia la posición objetivo
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
