using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Combat Action", menuName = "Combat Action/Ranged Combat Action")]
public class RangedCombatAction : CombatAction
{
    public GameObject projectilePrefab;
    
    public override void Cast(Character caster, Character target)
    {
        if (caster == null)
        {
            return;
        }
        
        GameObject projectile = Instantiate(projectilePrefab, caster.transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(target);
    }
}
