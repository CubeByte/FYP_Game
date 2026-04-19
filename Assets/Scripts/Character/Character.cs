using System;
using System.Collections;
using Combat_Action;
using Managers;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public enum Team
    {
        Player,
        Enemy
    }
    
    [Header("Character Stats")]
    public Team team;
    public string displayName;
    public int currentHP;
    public int maxHP;
    public Archetype weakness;
    public int weaknessMultiplier = 1;

    [Header("Character Defense")]
    public bool immuneToNonWeaknessDamage;
    
    [Header("Persistent Mapping")]
    public int persistentIndex = -1;
    
    [Header("Character Combat Actions")]
    public CombatAction[] combatActions;
    
    [Header("Character Components")]
    public CharacterUI characterUI;
    public GameObject characterSelection;
    public DamageFlash damageFlash;
    
    [Header("Prefabs")]
    public NewScriptableObjectScript WordListPrefab;
    public GameObject healEffectPrefab;
    
    private Vector3 characterPosition;

    void OnEnable()
    {
        TurnManager.instance.OnNewTurn += OnNewTurn;
    }

    void OnDisable()
    {
        TurnManager.instance.OnNewTurn -= OnNewTurn;
    }

    private void Start()
    {
        characterPosition = transform.position;
        characterUI.SetcharacterText(displayName);
        characterUI.UpdateHealthBar(currentHP,maxHP);
    }

    void OnNewTurn()
    {
        characterUI.ToggleTurnVisual(TurnManager.instance.GetCurrentTurnCharacter() == this);
    }

    public void CastCombatAction(CombatAction combatAction, Character targetCharacter = null)
    {
        if (targetCharacter == null)
        {
            targetCharacter = this;
        }
        combatAction.Cast(this, targetCharacter);
    }
    public void TakeDamage(int damage, Archetype damageType)
    {
        if (immuneToNonWeaknessDamage && damageType != weakness)
        {
            Debug.Log(displayName + " is immune to " + damageType);
            return;
        }

        if (damageType == weakness && WordListPrefab.WordIsKnown(weakness.ToString()))
        {
            damage *= weaknessMultiplier;
        }

        currentHP -= damage;
        characterUI.UpdateHealthBar(currentHP, maxHP);

        damageFlash.Flash();

        if (currentHP <= 0)
        {
            Die();
        }

        Debug.Log(displayName + " took " + damage + " damage from " + damageType);
    }

    public void Heal(int heal)
    {
        currentHP += heal;
        
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        
        characterUI.UpdateHealthBar(currentHP, maxHP);
        Instantiate(healEffectPrefab, transform);

        GameManager.instance.CheckTutorialFightHealWin(this);
    }

    public void Die()
    {
        GameManager.instance.OnCharacterDeath(this);
        Destroy(gameObject);
    }

    public void MoveToTarget(Character targetCharacter, UnityAction<Character> arriveCallBack)
    {
        StartCoroutine(MeleeAttackAnimation());
        
        IEnumerator MeleeAttackAnimation()
        {
            while (transform.position != targetCharacter.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetCharacter.transform.position, 10 *Time.deltaTime);
                yield return null;
            }
            
            arriveCallBack?.Invoke(targetCharacter);
            
            while (transform.position != characterPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, characterPosition, 10 *Time.deltaTime);
                yield return null;
            }
        }
    }

    public void ToggleCharacterSelection(bool toggle)
    {
        characterSelection.SetActive(toggle);

        if (team == Team.Enemy && WordListPrefab.WordIsKnown(weakness.ToString()))
        {
            characterUI.UpdateCharacterWeakness(toggle, WordListPrefab.ReturnWordPair(weakness.ToString()));
        }
        else if (team == Team.Enemy)
        {
            characterUI.UpdateCharacterWeakness(toggle, "Unknown");
        }
    }
}