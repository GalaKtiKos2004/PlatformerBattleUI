using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerFighter : Drummer
{
    [SerializeField] private HealthView _healthView;

    private PlayerInput _input;

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<PlayerInput>();
        _healthView.Init(_health);
    }

    private void Update()
    {
        if (_input.IsAttack)
        {
            TryAttack();
        }
    }

    protected override void Die()
    {
        CreateNewHealth();
        _healthView.Init(_health);
    }
}