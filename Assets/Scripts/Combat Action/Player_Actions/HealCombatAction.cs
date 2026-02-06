using UnityEngine;

[CreateAssetMenu(fileName = "Heal Combat Action", menuName = "Combat Action/Heal Combat Action")]
public class HealCombatAction : CombatAction
{
    public int healAmount;
    
    public override void Cast(Character caster, Character target)
    {
        target.Heal(healAmount);
    }
}
