using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CombatActionUI : MonoBehaviour
{
    public GameObject panel;
    public CombatActionButton[] buttons;
    public GameObject descriptionPanel;
    public TextMeshProUGUI combatActionDescription;

    //on enable and disable we want to subscribe and unsubscribe, as we dont want to call a function if we are disabled as problems may occure
    void OnEnable()
    {
        TurnManager.instance.OnNewTurn += OnNewTurn;
    }

    void OnDisable()
    {
        TurnManager.instance.OnNewTurn -= OnNewTurn;
    }

    //Every Turn Check what character is taking its turn. display combatActions if Player otherwise disable
    void OnNewTurn()
    {
        if (TurnManager.instance.GetCurrentTurnCharacter().team == Character.Team.Player)
        {
            DisplayCombatActions(TurnManager.instance.GetCurrentTurnCharacter());
        }
        else
        {
            DisableCombatActions();
        }
    }

    //goes through combat actions set for player and activates buttons for display
    public void DisplayCombatActions(Character character)
    {
        panel.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < character.combatActions.Length)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].SetCombatAction(character.combatActions[i]);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    public void DisableCombatActions()
    {
        panel.SetActive(false);
        DisableCombatActionDescription();
    }

    public void SetCombatActionDescription(CombatAction combatAction)
    {
        descriptionPanel.SetActive(true);
        combatActionDescription.text = combatAction.description;
    }

    public void DisableCombatActionDescription()
    {
        descriptionPanel.SetActive(false);
    }
}
