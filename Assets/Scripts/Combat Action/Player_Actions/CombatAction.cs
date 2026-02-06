using UnityEngine;

//abstract class mean it cannot be created on its own, has to be extended
public abstract class CombatAction : ScriptableObject
{
    public string displayName;
    public string description;
    
    //this will never run but will be implemented by other actions
    public abstract void Cast(Character caster, Character target);
}
