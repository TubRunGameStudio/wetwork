using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    [SerializeField] public Tilemap OBSTACLES;
    [SerializeField] public LayerMask OBSTACLE_LAYERMASK;
    [SerializeField] public PlayerController PLAYER;
    [SerializeField] public GameObject RETICULE;
}
