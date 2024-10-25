using UnityEngine;

[RequireComponent(typeof(Fighter))]
public class Collector : MonoBehaviour
{
    private Fighter _fighter;
    private Wallet _wallet;

    private void Awake()
    {
        _fighter = GetComponent<Fighter>();
        _wallet = new Wallet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (collectable is Coin)
            {
                _wallet.AddCoin();
            }

            if (collectable is MedecineChest medecineChest)
            {
                if (_fighter.TryAddHealth(medecineChest.RecoverHealth) == false)
                {
                    return;
                }
            }

            collectable.Collect();
        }
    }
}
