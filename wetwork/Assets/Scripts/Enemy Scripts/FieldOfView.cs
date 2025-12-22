using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Enemy self;
    [SerializeField] Material material;

    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float fov;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 60f;
        origin = Vector3.zero;

        GetComponent<MeshRenderer>().sortingLayerName = "Minimap";
    }

    void LateUpdate()
    {
        transform.position = Vector3.zero;
        int rayCount = 10;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 5f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {
                // No Hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit
                vertex = raycastHit2D.point;
                
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Draw the 2d polygon collider
        PolygonCollider2D poly2d = GetComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            points[i] = new Vector2(vertices[i].x, vertices[i].y);
        poly2d.points = points;
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        return angle;
    }

    public void SetOrigin(Vector3 origin)
    {
       this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        self.SetAlert(player.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;
        GameObject lastKnownLocation = new GameObject();
        lastKnownLocation.transform.position = player.transform.position;

        self.SetCaution(lastKnownLocation);
    }
}
