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
    [SerializeField] protected BaseNPC npc;

    public Health(BaseNPC baseNPC = null)
    {
        npc = baseNPC;
    }

    private void Awake() 
    {
        npc = GetComponent<BaseNPC>();
    }

    public int GetCurrentHealth()
    {
        return npc.GetNPCStats().currentHealth;
    }

    public void SetUpHealth(int amount)
    {
        npc.GetNPCStats().currentHealth = amount;
        npc.GetNPCStats().maxHealth = amount;
    }

    public void SetHealth(int amount)
    {
        npc.GetNPCStats().currentHealth = amount;
    }

    public virtual void Damage(int damage)
    {
        npc.GetNPCStats().currentHealth -= damage;
        if (npc.GetNPCStats().currentHealth <= 0)
        {
            npc.GetNPCStats().currentHealth = 0;
        }
    }

    public virtual void Heal(int amount)
    {
        npc.GetNPCStats().currentHealth += amount;
        if (npc.GetNPCStats().currentHealth > npc.GetNPCStats().maxHealth)
        {
            npc.GetNPCStats().currentHealth = npc.GetNPCStats().maxHealth;
        }
    }

    public void Kill()
    {
        npc.GetNPCStats().currentHealth = 0;
    }

    public virtual void FullHeal()
    {
        npc.GetNPCStats().currentHealth = npc.GetNPCStats().maxHealth;
    }
}