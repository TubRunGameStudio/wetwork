using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CCTV_Projectile : MonoBehaviour
{

    [SerializeField] private GameObject cutout;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    private Vector3 startPos;
    public Vector3 Target { get; set; }

    public void Activate()
    {
        startPos = transform.position + new Vector3(0, .75f, 0);
        StartCoroutine(ArcMove());
        gameObject.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Doodads");
    }

    IEnumerator ArcMove()
    {
        float timer = 0;
        while (timer <= speed)
        {
            transform.position = MathParabola.Parabola(startPos, Target, jumpHeight, timer / speed);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = Target;
        cutout.SetActive(true);
    }
}
