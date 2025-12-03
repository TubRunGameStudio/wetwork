using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void FixedUpdate()
    {
        Vector3 cameraPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = cameraPos;
    }

}
