using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Rendering.CameraUI;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    private Vector3 startPos;
    public Vector3 Target { get; set; }

    public virtual void Activate()
    {
        startPos = transform.position + new Vector3(0, .75f, 0);
        StartCoroutine(ArcMove());
        SetSortingLayer();
    }

    protected virtual IEnumerator ArcMove()
    {
        float timer = 0;
        while (timer <= speed)
        {
            transform.position = MathParabola.Parabola(startPos, Target, jumpHeight, timer / speed);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = Target;
    }

    public void SetSortingLayer()
    {
        gameObject.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Doodads");
    }
}
