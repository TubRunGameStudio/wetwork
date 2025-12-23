using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    [SerializeField] public Tilemap OBSTACLES;
    [SerializeField] public LayerMask OBSTACLE_LAYERMASK;
    [SerializeField] public PlayerController PLAYER;
    [SerializeField] public GameObject RETICULE;
    [SerializeField] private GameObject GAMEOVER;

    public void EndGame()
    {
        Time.timeScale = 0;
        Destroy(PLAYER.gameObject);
        GAMEOVER.SetActive(true);
    }
}
