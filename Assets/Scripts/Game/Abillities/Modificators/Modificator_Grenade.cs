using UnityEngine;

public class Modificator_Grenade : Modificator
{
    //Оглушаящая граната
    [SerializeField] private Grenade grenade;
    [SerializeField] private float speedGrenade = 35;

    public override void Use()
    {
        base.Use();
        Entity player = GameManager.instance.Player;

        if (nowCharge > 0)
            Instantiate(grenade, new Vector3(player.transform.position.x, player.transform.position.y + 2,
                                             player.transform.position.z), player.transform.rotation).GetComponent<Grenade>().TakeParametrs(damage, radius, timeOfWork, speedGrenade);
        else
            Debug.Log("Нет гранат!");
    }
}
