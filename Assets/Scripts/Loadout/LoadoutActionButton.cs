using Combat_Action;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutActionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Button button;

    private CombatAction action;
    private DynamicLoadoutMenu menu;

    public void Setup(CombatAction combatAction, DynamicLoadoutMenu ownerMenu)
    {
        action = combatAction;
        menu = ownerMenu;

        if (label != null)
            label.text = action != null ? action.displayName : "Empty";

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClicked);
        }
    }

    private void OnClicked()
    {
        if (menu != null && action != null)
        {
            menu.EquipAction(action);
        }
    }
}