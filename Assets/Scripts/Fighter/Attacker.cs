using UnityEngine;

public class Attacker
{
    public bool TryAttack(float damage, ColliderDetector detector, Transform position, LayerMask attackedLayer, Vector2 colliderSize)
    {
        if (detector.IsGrounded(position, attackedLayer, colliderSize, out Collider2D attackedCollider))
        {
            if (attackedCollider.TryGetComponent(out Fighter attacked))
            {
                attacked.TakeDamage(damage);
                return true;
            }
        }

        return false;
    }
}
