using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IKillable
{
    public void Die();
    public void NotifyDamage();
}

public class DamageableComponent : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth = 100;
    IKillable target;
    bool dead = false;

    private void Start()
    {
        target = GetComponent<IKillable>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("takedamage:" + damage);
        this.currentHealth -= damage;
        if(currentHealth <= 0 && !dead)
        {
            this.dead = true;
            target.Die(); 
        }
        else
        {
            target.NotifyDamage();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth/maxHealth;
    }

}
