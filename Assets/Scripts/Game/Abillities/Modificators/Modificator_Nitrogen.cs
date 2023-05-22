using UnityEngine;

public class Modificator_Nitrogen : Modificator
{
    //NitroGen
    [SerializeField] private Nitrogen preefab;

    public override void Use()
    {
        base.Use();
        enabled = true;

        if (nowCharge > 0)
            Instantiate(preefab, GameManager.instance.PlayerTransform.transform.position, Quaternion.identity).GetComponent<Nitrogen>().TakeParametrs(radius, timeOfWork);
        else
            Debug.Log("Нет Nitrogen!");
    }
}
