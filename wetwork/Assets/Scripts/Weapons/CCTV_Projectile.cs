using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SetSortingLayer();
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

        // Scene state
        string scene = SceneManager.GetActiveScene().name;
        SceneState.Add(scene, this);
    }

    public void Load(Vector3 pos)
    {
        transform.position = pos;
        cutout.SetActive(true);
        SetSortingLayer();
    }

    public void SetSortingLayer()
    {
        gameObject.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Doodads");
    }
}
