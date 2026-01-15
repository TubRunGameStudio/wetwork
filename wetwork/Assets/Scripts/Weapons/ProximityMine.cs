using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
            Trigger();
    }

    private void Trigger()
    {
        animator.SetBool("Trigger", true);
    }

    private void Explode()
    {
        GameObject expl = GameObject.Instantiate(explosion);
        expl.transform.position = transform.position;
        Destroy(gameObject);
    }
}
