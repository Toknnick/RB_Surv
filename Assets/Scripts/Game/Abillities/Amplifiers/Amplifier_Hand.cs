using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Amplifier_Hand : Ability
{
    //Механическая рука

    public override void AddLevel()
    {
        level++;
        Use();
    }

    public override void Use()
    {
        PlayerAI playerAI = GameManager.instance.PlayerAI;

        foreach (Modificator ability in playerAI.Modificators.Cast<Modificator>())
            ability.ChangeRange();
    }
}
