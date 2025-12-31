using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, Interactable
{
    [SerializeField] private string sceneTransitionName;
    [SerializeField] GameObject tooltip;

    void Update()
    {
        tooltip.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController.PLAYER.SetExitSceneTransition(this);
            tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.PLAYER.SetExitSceneTransition(null);
            tooltip.SetActive(false);
        }
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneTransitionName, LoadSceneMode.Single);
    }
}
