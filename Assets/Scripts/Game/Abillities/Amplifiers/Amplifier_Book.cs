public class Amplifier_Book : Ability
{
    //Сервисная книжка

    public override void AddLevel()
    {
        level++;
        Use();
    }

    public override void Use()
    {
        Entity player = GameManager.instance.Player;

        player.MaxHealth += player.MaxHealth * 0.25f;
    }
}
