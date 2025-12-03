using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private float movementX;
    private float movementY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * 1.6f;
        movementY = movementVector.y * .9f;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementY);
        rb.linearVelocity = movement * speed;
    }
}
