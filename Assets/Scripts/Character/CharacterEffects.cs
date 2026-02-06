using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    //list of effects on a character
    public List<EffectInstance> currentEffects = new List<EffectInstance>();
    private Character character;

    void Awake()
    {
        if (character == null)
        {
            Debug.LogError($"Character component missing on {gameObject.name}");
            return;
        }
        character = GetComponent<Character>();
    }

    public void AddNewEffect (Effect effect)
    {
        EffectInstance effectInstance = new EffectInstance(effect);

        if(effect.activePrefab != null)
            effectInstance.currentlyActiveGameObject = Instantiate(effect.activePrefab, transform);

        if(effect.tickPrefab != null)
            effectInstance.currentTickParticle = Instantiate(effect.tickPrefab, transform).GetComponent<ParticleSystem>();
        
        currentEffects.Add(effectInstance);
        ApplyEffect(effectInstance);
    }

    //called at start of each turn
    public void ApplyCurrentEffect()
    {
        for (int i = currentEffects.Count - 1; i >= 0; i--)
        {
            ApplyEffect(currentEffects[i]);
        }
    }

    void ApplyEffect(EffectInstance effect)
    {
        if (effect.currentTickParticle != null)
        {
            effect.currentTickParticle.Play();
        }

        if (effect.effect is DamageEffect damageEffect)
        {
            character.TakeDamage(damageEffect.damage);
        }
        else if (effect.effect is HealEffect healEffect)
        {
            character.Heal(healEffect.healAmountPerTurn);
        }

        effect.turnsRemaining--;

        if (effect.turnsRemaining == 0)
        {
            currentEffects.Remove(effect);
        }
    }

    void RemoveEffect(EffectInstance effect)
    {
        if (effect.currentlyActiveGameObject != null)
        {
            Destroy(effect.currentlyActiveGameObject);
        }
        if (effect.currentTickParticle != null)
        {
            Destroy(effect.currentTickParticle.gameObject);
        }
        currentEffects.Remove(effect);
    }
}
