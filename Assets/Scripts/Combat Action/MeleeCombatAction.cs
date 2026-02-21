using UnityEngine;

[CreateAssetMenu(fileName = "Melee Combat Action", menuName = "Combat Action/Melee Combat Action")]
public class MeleeCombatAction : CombatAction
{
    public int meleeDamage;
    
    //overrides the base class
    public override void Cast(Character caster, Character target)
    {
        caster.MoveToTarget(target,OnDamageTakenCallback);
    }

    //deal damage to target
    void OnDamageTakenCallback(Character target)
    {
        if (target.weakness == archetype && wordList.WordIsKnown(target.weakness.ToString()))
        {
            var weaknessDamage = meleeDamage * 2;
            target.TakeDamage(weaknessDamage);
        }
        else
        {
            target.TakeDamage(meleeDamage);
        }
    }
}
