using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_To_Battle_Interaction : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor)
    {
        LoadBattleFor(interactor, "Final_Battle", "You climb back from whence you fell");
        return true;
    }

    public void ResetInteraction()
    {
    }

    public static void LoadBattleFor(Interactor interactor, string sceneName, string text)
    {
        if (interactor == null)
        {
            Debug.LogError("Cannot load battle without an interactor.");
            return;
        }

        ExplorationPlayerState.Save(interactor.transform);
        Transition.Instance.LoadSceneWithMessage(sceneName, text);
    }

    public static void LoadBattleFor(Interactor interactor, string sceneName = "Battle")
    {
        if (interactor == null)
        {
            Debug.LogError("Cannot load battle without an interactor.");
            return;
        }

        ExplorationPlayerState.Save(interactor.transform);
        Transition.Instance.LoadSceneWithFade(sceneName);
    }
}
