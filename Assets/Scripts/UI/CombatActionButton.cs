using Managers;
using UnityEngine;
using TMPro;

public class CombatActionButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private CombatAction combatAction;
    private CombatActionUI ui;

    void Awake()
    {
        //look for this in the scene so we can communicate with it
        ui = FindFirstObjectByType<CombatActionUI>();
    }
    
    //called in combat action ui script, sets a combat action for a button
    public void SetCombatAction(CombatAction ca)
    {
        combatAction = ca;
        nameText.text = ca.displayName;
    }

    public void OnClick()
    {
        PlayerCombatManager.instance.SetCurrentCombatAction(combatAction);
    }

    //when you hover over combatAction button it will display ui else not
    public void OnHoverEnter()
    {
        ui.SetCombatActionDescription(combatAction);
    }

    public void OnHoverExit()
    {
        ui.DisableCombatActionDescription();
    }
}
