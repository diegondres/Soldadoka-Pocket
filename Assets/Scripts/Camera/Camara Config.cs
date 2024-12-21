using UnityEngine;

[CreateAssetMenu(fileName = "Config/CamaraConfiguration", menuName = "Config/Camara Configuration")]
public class CamaraConfiguration : ScriptableObject
{
    public float fastSpeed = 5;
    public float normalSpeed = 1;
    public float scrollSpeed = 5;
    public float minHeight = 50;
    public float maxHeight = 400;
    public float movementTime = 0.2f;
    public float rotationAmount = 10;
    public Vector3 zoomAmount = new Vector3(0, 1, -1);

    public float GetFastSpeed() => fastSpeed;
    public float GetNormalSpeed() => normalSpeed;
    public float GetScrollSpeed() => scrollSpeed;
    public Vector3 GetZoomAmount() => zoomAmount;
    public float GetMinHeight() => minHeight;
    public float GetMaxHeight() => maxHeight;
    public float GetMovementTime() => movementTime;
    public float GetRotationAmount() => rotationAmount;
}