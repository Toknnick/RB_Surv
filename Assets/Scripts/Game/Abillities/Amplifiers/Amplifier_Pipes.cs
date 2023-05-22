public class Amplifier_Pipes : Ability
{
    //Титановые трубы

    public override void AddLevel()
    {
        level++;
        Use();
    }

    public override void Use()
    {
        Entity player = GameManager.instance.Player;

        if (player.IsMellee)
            player.DamagePerSec += player.DamagePerSec * 0.2f;
    }
}
