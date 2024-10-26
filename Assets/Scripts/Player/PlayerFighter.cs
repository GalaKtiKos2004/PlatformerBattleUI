using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PositionStarter))]
public class PlayerFighter : Drummer
{
    private PlayerInput _input;
    private PositionStarter _positionStarter;

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<PlayerInput>();

        _positionStarter = GetComponent<PositionStarter>();
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
        InitHealthBar();
        _positionStarter.StartGame();
    }
}