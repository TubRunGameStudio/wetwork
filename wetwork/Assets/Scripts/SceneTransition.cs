using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, Interactable
{
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController.PLAYER.SetExitSceneTransition(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.PLAYER.SetExitSceneTransition(null);
        }
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneTransitionName, LoadSceneMode.Single);
    }
}
