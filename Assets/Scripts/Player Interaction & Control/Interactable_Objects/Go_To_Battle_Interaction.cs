using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_To_Battle_Interaction : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }
    [SerializeField] private string battleSceneName = "Battle";

    public bool Interact(Interactor interactor)
    {
        LoadBattle(interactor);
        return true;
    }

    public void ResetInteraction()
    {
    }

    public void LoadBattle(Interactor interactor)
    {
        LoadBattle(interactor, battleSceneName);
    }

    public void LoadBattle(Interactor interactor, string sceneName)
    {
        if (interactor == null)
        {
            Debug.LogError("Cannot load battle without an interactor.");
            return;
        }

        ExplorationPlayerState.Save(interactor.transform);
        Transition.Instance.LoadSceneWithFade(sceneName);
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
