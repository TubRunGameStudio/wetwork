using UnityEngine;

public class RadioRange : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawCircle(100, RadioTower.RANGE, transform.position);
    }

    private void DrawCircle(int steps, float radius, Vector3 center)
    {
        lineRenderer.positionCount = steps;
        for(int i = 0; i < steps; i++)
        {
            float circumferenceProgress = (float)i / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;
            Vector3 currentPosition = new Vector3(x, y, 0) + center;
            lineRenderer.SetPosition(i, currentPosition);
        }
    }
}
