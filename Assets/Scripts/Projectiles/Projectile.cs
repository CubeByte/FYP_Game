using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public int heal;
    public float speed;
    private Character target;

    public void Initialize(Character targetCharacter)
    {
        target = targetCharacter;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0,0.5f,0), speed * Time.deltaTime);
        }
    }

    void ImpactTarget()
    {
        if (damage > 0)
        {
            target.TakeDamage(damage);
        }
        if (heal > 0)
        {
            target.Heal(heal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null && other.gameObject == target.gameObject)
        {
            ImpactTarget();
            Destroy(gameObject);
        }
    }
}
