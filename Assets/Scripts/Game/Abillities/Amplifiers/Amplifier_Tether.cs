using System.Linq;

public class Amplifier_Tether : Ability
{
    //Стальной тросс

    public override void AddLevel()
    {
        level++;
        Use();
    }

    public override void Use()
    {
        PlayerAI playerAI = GameManager.instance.PlayerAI;

        foreach (Modificator ability in playerAI.Modificators.Cast<Modificator>())
            ability.ChangeRadius();
    }
}
