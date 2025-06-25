using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [Tooltip("1 = bronze, 2 = silver, 3 = gold")]
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
