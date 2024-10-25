using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColliderDetector))]
public class Fighter : MonoBehaviour
{
    [SerializeField] private LayerMask _attackedLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Vector2 _colliderSize;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _attackColldown = 3f;

    private PlayerInput _input;
    private ColliderDetector _detector;
    private Attacker _attacker;
    private Health _health;
    private WaitForSeconds _wait;
    private Vector3 _startPosition;
    private bool _canAttack = true;

    private void Awake()
    {
        TryGetComponent(out _input);
        _detector = GetComponent<ColliderDetector>();
        _attacker = new Attacker();
        _health = new Health(_maxHealth);
        _wait = new WaitForSeconds(_attackColldown);
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void Update()
    {
        bool isPlayerLayer = (_playerLayer.value & (1 << gameObject.layer)) != 0;
        bool isEnemyLayer = (_enemyLayer.value & (1 << gameObject.layer)) != 0;

        if (_canAttack && (isPlayerLayer && _input.IsAttack || isEnemyLayer))
        {
            Attack();
        }
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    public bool TryAddHealth(float recoverHealth)
    {
        if (_health.TryAddHealth(recoverHealth))
        {
            return true;
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void Attack()
    {
        if (_attacker.TryAttack(_damage, _detector, transform, _attackedLayer, _colliderSize))
        {
            StartCoroutine(AttackColldown());
        }
    }

    private void Die()
    {
        if ((_playerLayer.value & (1 << gameObject.layer)) != 0)
        {
            _health.Died -= Die;
            transform.position = _startPosition;
            _health = new Health(_maxHealth);
            _health.Died += Die;    
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AttackColldown()
    {
        _canAttack = false;
        yield return _wait;
        _canAttack = true;
    }
}