using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : Damage
{
    [SerializeField] private string otherTagName;
    [SerializeField] private int damageAmount;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTagName))
        {
            BaseNPC temp = other.gameObject.GetComponent<BaseNPC>();
            if (temp)
            {
                temp.TakeDamage(damageAmount);
            }
        }
    }
}