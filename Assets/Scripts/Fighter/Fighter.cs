using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    protected Health _health;

    protected virtual void Awake()
    {

        CreateNewHealth();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public bool TryAddHealth(float recoverHealth)
    {
        if (_health.TryTreated(recoverHealth))
        {
            return true;
        }

        return false;
    }

    protected void CreateNewHealth()
    {
        _health = new Health(_maxHealth);
        _health.Died += Die;
    }

    protected abstract void TryAttack();

    protected abstract void Die();
}