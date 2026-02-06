using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class PlayerCombatManager : MonoBehaviour
    {
        public float selectionCheckRate = 0.02f;
        private float lastSelectionCheckTime;
        public LayerMask selectionLayerMask;

        private bool isActive;
        private RaycastHit hit;
    
        [SerializeField]
        private CombatAction currentCombatAction;
        [SerializeField]
        private Character currentlySelectedCharacter;
    
        //selection flags
        [SerializeField]
        private bool canSelectSelf;
        [SerializeField]
        private bool canSelectTeam;
        [SerializeField]
        private bool canSelectEnemy;
    
        //singleton
        public static PlayerCombatManager instance;

        [Header("components")]
        public CombatActionUI combatActionUI;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        void OnEnable()
        {
            TurnManager.instance.OnNewTurn += OnNewTurn;
        }

        void OnDisable()
        {
            TurnManager.instance.OnNewTurn -= OnNewTurn;
        }

        void OnNewTurn()
        {
            if (TurnManager.instance.GetCurrentTurnCharacter().team == Character.Team.Player)
            {
                EnablePlayerCombat();
            }
            else
            {
                DisablePlayerCombat();
            }
        }

        void EnablePlayerCombat()
        {
            //resets all the values
            currentlySelectedCharacter = null;
            currentCombatAction = null;
            isActive = true;
        }

        void DisablePlayerCombat()
        {
            isActive = false;
        }

        //limits the call for Selection check in a specific timeframe to reduce lag
        void Update()
        {
            if (!isActive || currentCombatAction == null)
            {
                return;
            }

            if (Time.time - lastSelectionCheckTime > selectionCheckRate)
            {
                lastSelectionCheckTime = Time.time;
                SelectionCheck();
            }

            if (Mouse.current.leftButton.isPressed && currentlySelectedCharacter != null)
            {
                CastCombatAction();
            }
        }
        
        void SelectionCheck()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (Physics.Raycast(ray, out hit, 100000 , selectionLayerMask))
            {
                Character character = hit.collider.GetComponent<Character>();

                if (currentlySelectedCharacter != null && currentlySelectedCharacter == character)
                {
                    return;
                }
                if (canSelectSelf && character == TurnManager.instance.GetCurrentTurnCharacter())
                {
                    SelectCharacter(character);
                    return;
                }
                else if (canSelectTeam && character.team == Character.Team.Player)
                {
                    SelectCharacter(character);
                    return;
                }
                else if (canSelectEnemy && character.team == Character.Team.Enemy)
                {
                    SelectCharacter(character);
                    return;
                }
            }
            
            UnSelectCharacter();
        }
        
        void CastCombatAction()
        {
            //get current character and gather the information of what action has been selected as well as the target
            TurnManager.instance.GetCurrentTurnCharacter().CastCombatAction(currentCombatAction, currentlySelectedCharacter);
            currentCombatAction = null;
            
            //clears selection of combatActions, currentPlayer and target for combatAction
            UnSelectCharacter();
            DisablePlayerCombat();
            combatActionUI.DisableCombatActions();
            TurnManager.instance.endTurnButton.SetActive(false);
            
            //A delay for the character to complete there action then end Turn 
            Invoke(nameof(NextTurnDelay), 1.0f);
        }

        void NextTurnDelay()
        {
            TurnManager.instance.EndTurn();
        }

        void SelectCharacter(Character character)
        {
            UnSelectCharacter();
            currentlySelectedCharacter = character;
            character.ToggleCharacterSelection(true);
        }

        void UnSelectCharacter()
        {
            if (currentlySelectedCharacter == null)
                return;
            
            currentlySelectedCharacter.ToggleCharacterSelection(false);
            currentlySelectedCharacter = null;
        }

        //check selected combatAction from selected CombatActionButtons and what characters can be affected by said action
        public void SetCurrentCombatAction(CombatAction combatAction)
        {
            currentCombatAction = combatAction;
            
            canSelectSelf = false;
            canSelectTeam = false;
            canSelectEnemy = false;

            //decides what each combatAction allows the user to select
            if (combatAction as MeleeCombatAction || combatAction as RangedCombatAction)
            {
                canSelectEnemy = true;
            }
            else if (combatAction as HealCombatAction)
            {
                canSelectSelf = true;
                canSelectTeam = true;
            }
            else if (combatAction as EffectCombatAction)
            {
                canSelectSelf = (combatAction as EffectCombatAction).canEffectSelf;
                canSelectTeam = (combatAction as EffectCombatAction).canEffectTeam;
                canSelectEnemy = (combatAction as EffectCombatAction).canEffectEnemy;
            }
        }
    }
}
