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
        target.TakeDamage(meleeDamage);
    }
}
