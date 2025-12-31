using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerHealthBar HEALTH_BAR;
    [SerializeField] public TextMeshProUGUI AMMO_TXT;
    [SerializeField] public Tilemap OBSTACLES;
    [SerializeField] public LayerMask OBSTACLE_LAYERMASK;
    [SerializeField] public PlayerController PLAYER;
    [SerializeField] public GameObject RETICULE;
    [SerializeField] private GameObject GAMEOVER;

    public CameraScript mainCamera;
    public GameObject player;
    public GameObject playerPrefab;

    void Awake()
    {

        if (PlayerController.PLAYER != null)
        {
            // player already exists, so just move it to the spawn location and set the Player gameobject parameter
            player = PlayerController.PLAYER.gameObject;
        }
        else
        {
            //instantiate the player
            player = Instantiate(playerPrefab);
        }
        player.transform.position = PlayerState.PlayerLoadPosition;
        PlayerState.PlayerLoadPosition = PlayerState.PlayerReturnPosition;
        PLAYER = player.GetComponent<PlayerController>();
        PLAYER.Initiate(this);
        RETICULE = PLAYER.reticule;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        mainCamera.SetPlayer(player);
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        Destroy(PLAYER.gameObject);
        GAMEOVER.SetActive(true);
    }
}
