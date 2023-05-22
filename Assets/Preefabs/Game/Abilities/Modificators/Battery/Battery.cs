using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float addSpeed;
    private float timeOfWork;
    private float addAttack;

    public void TakeParametrs(float addingSpeed, float timeOfWork, float addingAttack)
    {
        this.timeOfWork = timeOfWork;
        addAttack = addingAttack;
        addSpeed= addingSpeed;
        StartCoroutine(AddParametrs());
    }


    private IEnumerator AddParametrs()
    {
        Entity player = GameManager.instance.Player;

        player.Speed += addSpeed;
        player.DamagePerSec += addAttack;

        yield return new WaitForSecondsRealtime(timeOfWork);

        player.Speed -= addSpeed;
        player.DamagePerSec -= addAttack;

        Destroy(gameObject);
    }
}
