using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector3 moveVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * moveVector.z + transform.right * moveVector.x;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
    }
}