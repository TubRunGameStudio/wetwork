using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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

        InitiateCCTVs();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        mainCamera.SetPlayer(player);
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        Destroy(PLAYER.gameObject);
        GAMEOVER.SetActive(true);
    }

    private void InitiateCCTVs()
    {
        GameObject container = GameObject.Find("CCTVs");

        string scene = SceneManager.GetActiveScene().name;
        List<Vector3> cctvs = SceneState.GetCCTVs(scene);
        if(cctvs != null && cctvs.Count > 0)
        {
            foreach(Vector3 pos in cctvs)
            {
                GameObject projectile = (GameObject)Resources.Load("projectile", typeof(GameObject));
                GameObject obj = GameObject.Instantiate(projectile);
                obj.transform.SetParent(container.transform);
                CCTV_Projectile proj = obj.GetComponent<CCTV_Projectile>();
                proj.Load(pos);
            }
        }
    }
}
