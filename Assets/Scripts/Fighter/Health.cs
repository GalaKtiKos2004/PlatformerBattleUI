using System;

public class Health
{
    private float _maxHealth;

    public event Action Died;

    public Health(float maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public float CurrentHealth { get; private set; }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public bool TryAddHealth(float recoverHealth)
    {
        if (CurrentHealth + recoverHealth > _maxHealth)
        {
            return false;
        }
        else
        {
            CurrentHealth += recoverHealth;
            return true;
        }
    }
}
