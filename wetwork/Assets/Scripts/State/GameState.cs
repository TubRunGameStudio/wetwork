using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Reset()
    {
        SceneState.Reset();
        PlayerController.PLAYER.Reset();
        Time.timeScale = 1;
        SceneManager.LoadScene("Test Scene", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
