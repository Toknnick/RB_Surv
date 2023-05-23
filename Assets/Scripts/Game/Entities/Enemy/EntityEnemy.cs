using System;

public class EntityEnemy : Entity
{
    public event Action OnDie;

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
            OnDie?.Invoke();
    }

    public void DealDamage(EntityPlayer player, float amount)
    {
        player.TakeDamage(this, amount);
    }
}
