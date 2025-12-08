using UnityEngine;

public class VisionConeScript : MonoBehaviour
{
    [SerializeField] int speed;

    private Transform rotator;
    private Vector3 eulers;

    private void Awake()
    {
        rotator = transform.parent;
        eulers = new Vector3(0, 0, speed);
    }

    private void FixedUpdate()
    {
        rotator.Rotate(eulers);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        Debug.Log("Player entered vision cone");
    }
}
