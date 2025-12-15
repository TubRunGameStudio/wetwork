using UnityEngine;

public class VisionConeScript : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] Enemy enemy;

    private Transform rotator;

    private void Awake()
    {
        rotator = transform.parent;

    }

    private void FixedUpdate()
    {
        Vector3 targetDirection = enemy.GetDestination().transform.position;
        targetDirection.z = 0.0f;

        Vector3 selfPosition = enemy.transform.position;
        Vector2 diff = new Vector2(targetDirection.x - selfPosition.x, targetDirection.y - selfPosition.y);

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        
        // not sure why it is 90 deg off
        angle += 90;
        rotator.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        enemy.SetDestination(player.gameObject);
    }
}
