using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Reveal();
    }

    public void Reveal()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }    
}
