using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private Health _health;
    
    protected Health Health => _health;

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

    public bool TryAddHealth(float recoverHealth) => _health.TryTreated(recoverHealth);

    protected void CreateNewHealth()
    {
        _health = new Health(_maxHealth);
        _health.Died += Die;
    }

    protected abstract void Die();
}