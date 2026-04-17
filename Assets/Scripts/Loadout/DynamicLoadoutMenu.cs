using Combat_Action;
using CharacterData;
using TMPro;
using UnityEngine;

public class DynamicLoadoutMenu : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerPersistantData playerPersistantData;

    [Header("UI")]
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text[] slotTexts;

    [Header("Available Actions Grid")]
    [SerializeField] private Transform actionGridParent;
    [SerializeField] private LoadoutActionButton actionButtonPrefab;

    private const int SelectedPlayerIndex = 0;
    private int selectedSlotIndex = 0;

    private void OnEnable()
    {
        RefreshAll();
    }

    public void SelectSlot(int slotIndex)
    {
        selectedSlotIndex = slotIndex;

        CombatAction[] equipped = playerPersistantData.characters[SelectedPlayerIndex].combatActions;
        string currentAction = "Empty";

        if (equipped != null && slotIndex >= 0 && slotIndex < equipped.Length && equipped[slotIndex] != null)
        {
            currentAction = string.IsNullOrEmpty(equipped[slotIndex].displayName)
                ? equipped[slotIndex].name
                : equipped[slotIndex].displayName;
        }

        UpdateFeedback($"Selected Slot {selectedSlotIndex + 1}: {currentAction}");
    }

    public void EquipAction(CombatAction action)
    {
        if (playerPersistantData == null || action == null)
            return;

        if (SelectedPlayerIndex < 0 || SelectedPlayerIndex >= playerPersistantData.characters.Length)
            return;

        CombatAction[] equipped = playerPersistantData.characters[SelectedPlayerIndex].combatActions;

        if (equipped == null || selectedSlotIndex < 0 || selectedSlotIndex >= equipped.Length)
            return;

        equipped[selectedSlotIndex] = action;

        string actionLabel = string.IsNullOrEmpty(action.displayName) ? action.name : action.displayName;
        UpdateFeedback($"Equipped {actionLabel} to Slot {selectedSlotIndex + 1}");

        RefreshSlotTexts();
    }

    public void RefreshAll()
    {
        RefreshSlotTexts();
        RebuildLearnedActionsGrid();
        UpdateFeedback("Select a slot, then choose an action.");
    }

    private void RefreshSlotTexts()
    {
        if (playerPersistantData == null)
            return;

        if (SelectedPlayerIndex < 0 || SelectedPlayerIndex >= playerPersistantData.characters.Length)
            return;

        CombatAction[] equipped = playerPersistantData.characters[SelectedPlayerIndex].combatActions;

        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (equipped != null && i < equipped.Length)
            {
                slotTexts[i].text = equipped[i].displayName != null ? equipped[i].displayName : "Empty";
            }
            else
            {
                slotTexts[i].text = "-";
            }
        }
    }

    private void RebuildLearnedActionsGrid()
    {
        if (actionGridParent == null || actionButtonPrefab == null || playerPersistantData == null)
            return;

        for (int i = actionGridParent.childCount - 1; i >= 0; i--)
        {
            Destroy(actionGridParent.GetChild(i).gameObject);
        }

        if (SelectedPlayerIndex < 0 || SelectedPlayerIndex >= playerPersistantData.characters.Length)
            return;

        CombatAction[] learnedActions = playerPersistantData.characters[SelectedPlayerIndex].unlockedActions;

        if (learnedActions == null)
            return;

        for (int i = 0; i < learnedActions.Length; i++)
        {
            CombatAction action = learnedActions[i];

            if (action == null)
                continue;

            LoadoutActionButton newButton = Instantiate(actionButtonPrefab, actionGridParent);
            newButton.Setup(action, this);
        }
    }

    private void UpdateFeedback(string message)
    {
        if (feedbackText != null)
            feedbackText.text = message;
    }
}