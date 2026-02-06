using UnityEngine;
using System.Linq;
using Managers;

public class EnemyCombatManager : MonoBehaviour
{
    [Header("AI")]
    public float minWaitTime = 0.2f;
    public float maxWaitTime = 1.5f;
    
    [Header("Attacking")]
    public float attackWeakestChance = 0.7f;
    
    [Header("Chance Curves")]
    public AnimationCurve healChanceCurve;
    private Character _currentEnemy;

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
        if (TurnManager.instance.GetCurrentTurnCharacter().team == Character.Team.Enemy)
        {
            _currentEnemy = TurnManager.instance.GetCurrentTurnCharacter();
            Invoke(nameof(DecideCombatAction), Random.Range(minWaitTime, maxWaitTime));
        }
    }
    void DecideCombatAction()
    {
        //decide if NPC needs to heal
        if (HasCombatAction(typeof(HealCombatAction)))
        {
            Character weakestCharacter = GetWeakestCharacter(Character.Team.Enemy);

            //generate a random number and compare it to our healChanceCurve, if less, heal character 
            if (Random.value < healChanceCurve.Evaluate(GetHealthPercentage(weakestCharacter)) && weakestCharacter != null)
            {
                CastCombatAction(GetHealCombatAction(), weakestCharacter);
            }
        }
        
        //deal damage to a character
        Character characterToDamage;

        if (Random.value < attackWeakestChance)
        {
            characterToDamage = GetWeakestCharacter(Character.Team.Player);
        }
        else
        {
            characterToDamage = GetRandomCharacter(Character.Team.Player);
        }

        if (characterToDamage != null)
        {
            if (HasCombatAction(typeof(MeleeCombatAction)) || HasCombatAction(typeof(RangedCombatAction)))
            {
                CastCombatAction(GetDamageCombatAction(),characterToDamage);
                return;
            }
        }
        
        Invoke(nameof(EndTurn), Random.Range(minWaitTime, maxWaitTime));
    }
    void CastCombatAction(CombatAction combatAction, Character target)
    {
        if (_currentEnemy == null)
        {
            EndTurn();
            return;
        }
            
        
        _currentEnemy.CastCombatAction(combatAction, target);
        Invoke(nameof(EndTurn), Random.Range(minWaitTime, maxWaitTime));
    }
    void EndTurn()
    {
        TurnManager.instance.EndTurn();
    }
    float GetHealthPercentage(Character character)
    {
        return (float)character.currentHP / (float)character.maxHP;
    }
    bool HasCombatAction(System.Type actionType)
    {
        foreach (CombatAction ca in _currentEnemy.combatActions)
        {
            if(ca.GetType() == actionType)
                return true;
        }
        
        return false;
    }
    CombatAction GetDamageCombatAction()
    {
        CombatAction[] combatActions = _currentEnemy.combatActions.Where(x => x.GetType() == typeof(MeleeCombatAction) || x.GetType() == typeof(RangedCombatAction)).ToArray();
        
        if (combatActions.Length == 0 || combatActions == null)
            return null;
        
        return combatActions[Random.Range(0, combatActions.Length)];
    }
    CombatAction GetHealCombatAction()
    {
        CombatAction[] combatActions = _currentEnemy.combatActions.Where(x => x.GetType() == typeof(HealCombatAction)).ToArray();
        if (combatActions.Length == 0 || combatActions == null)
            return null;
        
        return combatActions[Random.Range(0, combatActions.Length)];
    }

    Character GetWeakestCharacter(Character.Team team)
    {
        int weakestHP = 9999;
        int weakestIndex = 0;

        Character[] characters = team == Character.Team.Player
            ? GameManager.instance.playerTeam.ToArray()
            : GameManager.instance.enemyTeam;

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
                continue;

            if (characters[i].currentHP > weakestHP)
            {
                weakestHP = characters[i].currentHP;
                weakestIndex = i;
            }
        }
        
        return characters[weakestIndex];
    }
    Character GetRandomCharacter(Character.Team team)
    {
        Character[] characters = null;

        if (team == Character.Team.Player)
        {
            characters = GameManager.instance.playerTeam.Where(x => x != null).ToArray();
        }
        else if (team == Character.Team.Enemy)
        {
            characters = GameManager.instance.playerTeam.Where(x => x != null).ToArray();
        }
        
        return characters[Random.Range(0, characters.Length)];
    }
}
