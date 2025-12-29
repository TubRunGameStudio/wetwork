using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject HEALTH_BAR;
    [SerializeField] public TextMeshProUGUI AMMO_TXT;
    [SerializeField] public Tilemap OBSTACLES;
    [SerializeField] public LayerMask OBSTACLE_LAYERMASK;
    [SerializeField] public PlayerController PLAYER;
    [SerializeField] public GameObject RETICULE;
    [SerializeField] private GameObject GAMEOVER;

    public CameraScript mainCamera;
    public GameObject player;
    public GameObject playerPrefab;
    public Vector3 spawnPosition;

    void Awake()
    {
        spawnPosition = new Vector3(0, 0, 0);

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            //player already exists, so just move it to the spawn location and set the Player gameobject parameter
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawnPosition;

        }
        else
        {
            //instantiate the player
            player = Instantiate(playerPrefab);
            player.transform.position = spawnPosition;
        }
        PLAYER = player.GetComponent<PlayerController>();
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
