using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject player;

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void FixedUpdate()
    {
        if (player == null)
            return;

        Vector3 cameraPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = cameraPos;
    }

}
