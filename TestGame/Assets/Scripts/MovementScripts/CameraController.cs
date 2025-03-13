using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxYAngle = 80f;
    private float rotationX = 0f;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}