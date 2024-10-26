using System;

public class Health
{
    private float _maxHealth;

    public event Action Died;
    public event Action<float, float> Changed;

    public Health(float maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentHealth = maxHealth;

        Changed?.Invoke(CurrentHealth, _maxHealth);
    }

    public float CurrentHealth { get; private set; }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        Changed?.Invoke(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public bool TryTreated(float recoverHealth)
    {
        if (CurrentHealth + recoverHealth > _maxHealth)
        {
            return false;
        }
        else
        {
            CurrentHealth += recoverHealth;
            Changed?.Invoke(CurrentHealth, _maxHealth);
            return true;
        }
    }
}
