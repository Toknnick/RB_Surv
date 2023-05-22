using System;
using UnityEngine;

[Serializable]
public class Modificator : Ability
{
    [SerializeField] protected float addingDamageForBot = 0;
    [SerializeField] private float Damage = 0;
    [SerializeField] private float Radius = 0;
    [SerializeField] private float Range = 0;
    [SerializeField] private float TimeOfWork = 0;
    [SerializeField] protected float chargeForWave = 0;

    protected float damage;
    protected float radius;
    protected float range;
    protected float timeOfWork;

    protected float nowCharge = 0;

    [SerializeField] protected float addDamageByLevel = 0;
    [SerializeField] protected float addRadiusByLevel = 0;
    [SerializeField] protected float addRangeByLevel = 0;
    [SerializeField] protected float addTimeOfWorkByLevel = 0;

    public void ChangeRange()
    {
        range += 1;
    }

    public void ChangeRadius()
    {
        radius += radius * 0.2f;
    }

    public void SetDefault()
    {
        level = 1;
        damage = Damage;
        radius = Radius;
        range = Range;
        timeOfWork = TimeOfWork;
        nowCharge = 0;
    }

    public override void SetDefLevel()
    {
        base.SetDefLevel();
        ResetCharge();
    }

    public void ResetCharge()
    {
        nowCharge = chargeForWave;
    }

    public override void AddLevel()
    {
        level++;
        nowCharge = chargeForWave;
        damage += addDamageByLevel;
        radius += addRadiusByLevel;
        range += addRangeByLevel;
        timeOfWork += addTimeOfWorkByLevel;
    }

    public override void Use()
    {
        nowCharge--;
        Debug.Log("Use of modificator");
    }

}
