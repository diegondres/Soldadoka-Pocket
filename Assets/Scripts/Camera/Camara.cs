using System;
using Cinemachine;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject cameraCiybuilder;
    [SerializeField]
    private CamaraConfiguration config;
    private Camera camarita;
    private float movementSpeed;
    private Vector3 newZoom;

    [SerializeField]
    private Vector3 newPosition;
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    public void Awake()
    {
        newZoom = cameraCiybuilder.transform.localPosition;
        camarita = FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    public void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * config.GetScrollSpeed() * config.GetZoomAmount();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new(Vector3.up, Vector3.zero);
            Ray ray = camarita.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new(Vector3.up, Vector3.zero);
            Ray ray = camarita.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = config.GetFastSpeed();
        }
        else
        {
            movementSpeed = config.GetNormalSpeed();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += config.GetZoomAmount();
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= config.GetZoomAmount();
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * config.GetMovementTime());
        cameraCiybuilder.transform.localPosition = Vector3.Lerp(cameraCiybuilder.transform.localPosition, newZoom, Time.deltaTime * config.GetMovementTime());
        CheckZoomLimits();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        newPosition = position;
    }

    private void CheckZoomLimits()
    {
        if (cameraCiybuilder.transform.localPosition.y < config.GetMinHeight())
        {
            Vector3 newLocalPosition = cameraCiybuilder.transform.localPosition;
            newLocalPosition.y = config.GetMinHeight();
            newLocalPosition.z = -config.GetMinHeight();
            cameraCiybuilder.transform.localPosition = newLocalPosition;
            newZoom -= config.GetZoomAmount();

        }
        if (cameraCiybuilder.transform.localPosition.y > config.GetMaxHeight())
        {
            Vector3 newLocalPosition = cameraCiybuilder.transform.localPosition;
            newLocalPosition.y = config.GetMaxHeight();
            newLocalPosition.z = -config.GetMaxHeight();
            cameraCiybuilder.transform.localPosition = newLocalPosition;
            newZoom += config.GetZoomAmount();
        }
    }


}
