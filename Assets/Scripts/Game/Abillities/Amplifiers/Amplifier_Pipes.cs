public class Amplifier_Pipes : Ability
{
    //��������� �����

    public override void AddLevel()
    {
        level++;
        Use();
    }

    public override void Use()
    {
        EntityPlayer player = GameManager.instance.Player;

        if (player.IsMellee)
            player.Damage += player.Damage * 0.2f;
    }
}
