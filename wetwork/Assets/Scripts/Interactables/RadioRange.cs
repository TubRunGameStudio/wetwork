using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RadioRange : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    private GameController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        controller.SetMinimap(true);
        DrawCircle(100, RadioTower.RANGE);

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = RadioTower.RANGE;
    }

    private void DrawCircle(int steps, float radius)
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
            Vector3 currentPosition = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, currentPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out var player))
            return;

        controller.SetMinimap(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out var player))
            return;

        controller.SetMinimap(false);
    }

}
