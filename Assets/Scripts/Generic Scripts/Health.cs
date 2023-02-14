using UnityEngine;

/*
 * This script is a generic health component for
 * any item that needs to have health.  This can
 * be added to the player, enemies, pots or grass
 * in the scene.  It can also be extended by
 * inheriting from it for specific interactions desired.
 */

[RequireComponent(typeof(BaseNPC))]
public class Health : MonoBehaviour
{
    [Header("Location of Health Values")]
    [SerializeField] protected NPCStats stats;

    private void Awake() 
    {
        stats = GetComponent<NPCStats>();
    }

    public int GetCurrentHealth()
    {
        return stats.currentHealth;
    }

    public void SetUpHealth(int amount)
    {
        stats.currentHealth = amount;
        stats.maxHealth = amount;
    }

    public void SetHealth(int amount)
    {
        stats.currentHealth = amount;
    }

    public virtual void Damage(int damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            stats.currentHealth = 0;
        }
    }

    public virtual void Heal(int amount)
    {
        stats.currentHealth += amount;
        if (stats.currentHealth > stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }
    }

    public void Kill()
    {
        stats.currentHealth = 0;
    }

    public virtual void FullHeal()
    {
        stats.currentHealth = stats.maxHealth;
    }
}