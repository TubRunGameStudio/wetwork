using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CCTV_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject cutout;
    [SerializeField] float speed;

    public Vector3 Target { get; set; }
    private bool hasReached = false;
    private void Activate()
    {
        cutout.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (!hasReached)
        {
            if (transform.position == Target)
            {
                hasReached = true;
                Activate();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, speed);
            }
        }
    }
}
