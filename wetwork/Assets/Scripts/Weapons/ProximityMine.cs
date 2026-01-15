using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
            Explode();
    }

    private void Explode()
    {
        GameObject expl = GameObject.Instantiate(explosion);
        expl.transform.position = transform.position;
        Destroy(gameObject);
    }
}
