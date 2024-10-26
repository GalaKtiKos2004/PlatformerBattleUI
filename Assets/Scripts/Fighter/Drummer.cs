using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColliderDetector))]
public class Drummer : Fighter
{
    [SerializeField] private HealthView _healthView;

    [SerializeField] private LayerMask _attackedLayer;
    [SerializeField] private Vector2 _colliderSize;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackColldown = 3f;

    private WaitForSeconds _wait;

    private ColliderDetector _detector;
    private Attacker _attacker;

    private bool _canAttack;

    protected HealthView HealthView => _healthView;

    protected override void Awake()
    {
        base.Awake();

        _wait = new WaitForSeconds(_attackColldown);
        _attacker = new Attacker();
        _detector = GetComponent<ColliderDetector>();
        _canAttack = true;
    }

    private void Start()
    {
        _healthView.Init(_health);
    }

    private void Update()
    {
        TryAttack();
    }

    protected override void TryAttack()
    {
        if (_canAttack == false)
        {
            return;
        }

        if (_attacker.TryAttack(_damage, _detector, transform, _attackedLayer, _colliderSize))
        {
            StartCoroutine(AttackColldown());
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    protected void InitHealthBar()
    {
        _healthView.Init(_health);
    }

    private IEnumerator AttackColldown()
    {
        _canAttack = false;
        yield return _wait;
        _canAttack = true;
    }
}