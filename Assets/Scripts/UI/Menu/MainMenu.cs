using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (SceneManager.GetSceneByName("Menu") != SceneManager.GetActiveScene())
        {
            Transition.Instance.LoadSceneWithFade("Menu");
        }
        
        if (SceneManager.GetSceneByName("Menu") == SceneManager.GetActiveScene())
        {
            Transition.Instance.LoadSceneWithMessage("Battle","You get woken up by a noise");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
