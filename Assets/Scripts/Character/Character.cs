using System;
using System.Collections;
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
    
    [Header("Character Combat Actions")]
    public CombatAction[] combatActions;
    
    [Header("Character Components")]
    public CharacterUI characterUI;
    public GameObject characterSelection;
    public DamageFlash damageFlash;
    
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject healEffectPrefab;
    
    //initial position of current turn character
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        characterUI.UpdateHealthBar(currentHP,maxHP);
        
        damageFlash.Flash();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        currentHP += heal;
        
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        
        characterUI.UpdateHealthBar(currentHP,maxHP);
        Instantiate(healEffectPrefab, transform);
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
    }
}
