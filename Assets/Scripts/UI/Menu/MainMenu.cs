using CharacterData;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Data")]
    public PlayerPersistantData playerPersistantData;

    public void PlayGame()
    {
        if (playerPersistantData != null)
        {
            playerPersistantData.ResetCharacters();
        }
        else
        {
            Debug.LogWarning("MainMenu: PlayerPersistantData is not assigned.");
        }

        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Transition.Instance.LoadSceneWithFade("Menu");
        }
        else
        {
            Transition.Instance.LoadSceneWithMessage("Battle", "You get woken up by a noise");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}