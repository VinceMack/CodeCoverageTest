using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AnimatorController))]
[RequireComponent(typeof(SpriteRenderer))]
public class BaseNPC : SaveableEntity
{
    [SerializeField] private float damagedAnimationDuration = 1f;
    [SerializeField] private float deathAnimationDuration = 1f;
    [SerializeField] private float immuneFrameDuration = 2f;
    [SerializeField] private float timeBetweenFlashes = 0.5f;
    [SerializeField] private Color flashColor;
    private Color originalColor;
    public NPCStats stats = new NPCStats();
    StateMachine myState;
    Health myHealth;
    AnimatorController myAnim;

    private void Awake() 
    {
        myState = GetComponent<StateMachine>();
        myHealth = GetComponent<Health>();
        myAnim = GetComponent<AnimatorController>();
        stats = new NPCStats();
        myHealth.SetNPC(this);
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    public StateMachine GetNPCState()
    {
        return myState;
    }

    public NPCStats GetNPCStats()
    {
        return stats;
    }

    public override void SaveMyData(int saveSlot)
    {
        SaveData<NPCStats>(stats, saveSlot);
    }

    public override void LoadMyData(int saveSlot)
    {
        stats = LoadData<NPCStats>(saveSlot);
        // Do whatever we want with stats
        // I.e. change transform.position, update any components, etc.
    }

    [ContextMenu("TakeDam")]
    public void DealDamage()
    {
        TakeDamage(200);
    }

    [ContextMenu("SetupHealth")]
    public void SetupNPCHealth()
    {
        myHealth.SetUpHealth(5);
    }

    public virtual void TakeDamage(int damageAmount)
    {
        if(myState.myState != GenericState.dead)
        {
            if(myState.myState != GenericState.stun)
            {
                myHealth.Damage(damageAmount);
            }
            if(myHealth.GetCurrentHealth() <= 0)
            {
                StartCoroutine("DeathCoroutine");
                myState.ChangeState(GenericState.dead);
            }
            if(myState.myState != GenericState.dead)
            {
                StartCoroutine("TakeDamageCoroutine");
                StartCoroutine("ImmunityFramesCoroutine");
            }
        }
    }

    public IEnumerator DeathCoroutine()
    {
        myAnim.SetAnimParameter("dead", true);
        yield return new WaitForSeconds(deathAnimationDuration);
        GlobalInstance.Instance.entityDictionary.DestroySaveableObject(GetComponent<SaveableEntity>());
    }

    public IEnumerator TakeDamageCoroutine()
    {
        myAnim.SetAnimParameter("damaged", true);
        yield return new WaitForSeconds(damagedAnimationDuration);
        myAnim.SetAnimParameter("damaged", false);
    }

    public IEnumerator ImmunityFramesCoroutine()
    {
        myState.ChangeState(GenericState.stun /*Non-GenericState.Immune*/);
        for(int i = 0; i < immuneFrameDuration / timeBetweenFlashes; i++)
        {
            if(i % 2 == 0)
            {
                GetComponent<SpriteRenderer>().color = flashColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = originalColor;
            }
            yield return new WaitForSeconds(timeBetweenFlashes);
        }
        GetComponent<SpriteRenderer>().color = originalColor;
        myState.ChangeState(GenericState.idle);
    }
}
