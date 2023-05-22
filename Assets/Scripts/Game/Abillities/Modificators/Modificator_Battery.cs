using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modificator_Battery : Modificator
{
    //Батарея
    [SerializeField] private Battery preefab;
    [SerializeField] private float addingSpeed;
    [SerializeField] private float addingAttack;

    public override void Use()
    {
        base.Use();

        if (nowCharge > 0)
            Instantiate(preefab, GameManager.instance.PlayerTransform.transform.position, Quaternion.identity).GetComponent<Battery>().TakeParametrs(addingSpeed, timeOfWork, addingAttack);
        else
            Debug.Log("Нет Батарей!");
    }
}