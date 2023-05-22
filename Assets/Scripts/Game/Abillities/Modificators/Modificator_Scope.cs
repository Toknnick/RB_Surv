using System.Collections;
using System.Numerics;
using UnityEngine;

public class Modificator_Scope : Modificator
{
    //Лазерный Прицел

    public override void Use()
    {
        base.Use();

        if (nowCharge > 0)
            UseScope();
        else
            Debug.Log("Нет прицелов!");
    }

    private void UseScope()
    {
        Entity player = GameManager.instance.Player;
        player.AttackRange += range;
        player.DamagePerSec += damage;
        player.GetComponent<PlayerData>().OnXPChanged += StopUseScope;
    }

    private void StopUseScope(float f, float a)
    {
        Entity player = GameManager.instance.Player;
        player.AttackRange -= range;
        player.DamagePerSec -= damage;
        player.GetComponent<PlayerData>().OnXPChanged -= StopUseScope;
    }
}
