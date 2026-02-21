using UnityEngine;

public class EffectCombatAction : CombatAction
{
    //public Effect effectToCast;
    public bool canEffectSelf;
    public bool canEffectTeam;
    public bool canEffectEnemy;
    
    public override void Cast(Character caster, Character target)
    {
        //target.characterEffects.AddNewEffect(effectToCast);
    }
}
