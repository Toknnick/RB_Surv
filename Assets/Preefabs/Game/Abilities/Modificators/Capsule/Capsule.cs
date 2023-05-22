using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    private float timeOfWork;
    private float defaultSlope;
    private int Level;

    private int nowLevel = 1;

    public void TakeParametrs(float timeOfWork, int level,float defSlope)
    {
        this.timeOfWork = timeOfWork;
        Level = level;
        defaultSlope = defSlope;
        StartCoroutine(AddParametrs());
    }

    private IEnumerator AddParametrs()
    {
        Entity player = GameManager.instance.Player;

        if (nowLevel <= Level)
        {
            player.SlopeChance += defaultSlope;
            nowLevel++;
        }

        float nowSlope = player.SlopeChance;
        player.SlopeChance = 101;

        yield return new WaitForSecondsRealtime(timeOfWork);

        player.SlopeChance = nowSlope;
        Destroy(gameObject);
    }
}
