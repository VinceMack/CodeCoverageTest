using UnityEngine;
using System.Collections;

public class NonGenericHealthImplementationForTest : Health
{
    public int hp;

    public NonGenericHealthImplementationForTest()
    {
        hp = 10;
        npc = new BaseNPC();
    }
    public NonGenericHealthImplementationForTest(int setter)
    {
        hp = setter;
    }
    public override void Damage(int damage)
    {
        hp -= damage;
    }

    public void SetUp(int defaultHealth)
    {
        hp = defaultHealth;
    }

    public int GetHP()
    {
        return hp;
    }
}