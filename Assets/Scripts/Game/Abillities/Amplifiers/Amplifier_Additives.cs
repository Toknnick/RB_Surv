public class Amplifier_Additives : Ability
{
    //������� ��� �������

    public override void AddLevel()
    {
        level++;
        Use();
    }

    public override void Use()
    {
        GameManager.instance.Player.Speed += GameManager.instance.Player.Speed * 0.2f;
    }
}
