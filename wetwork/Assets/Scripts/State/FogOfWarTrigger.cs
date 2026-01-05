using UnityEngine;

public class FogOfWarTrigger : MonoBehaviour
{
    [SerializeField] FogOfWar hidden;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            hidden.Reveal();
    }
}
