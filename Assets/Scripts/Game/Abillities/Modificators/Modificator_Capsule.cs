using UnityEngine;

public class Modificator_Capsule : Modificator
{
    //Капсула с машинным маслом
    [SerializeField] private float defaultSlope = 15f;
    [SerializeField] private Capsule preefab;

    public override void Use()
    {
        base.Use();

        if (nowCharge > 0)
            Instantiate(preefab, GameManager.instance.PlayerTransform.transform.position, Quaternion.identity).GetComponent<Capsule>().TakeParametrs(timeOfWork, Level, defaultSlope);
        else
            Debug.Log("Нет Капсул с машинным маслом!");
    }
}
