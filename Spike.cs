using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spike : MonoBehaviour
{
    [Tooltip("Player tag to detect")]
    public string playerTag = "Player";

    void Awake()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
                player.Die();
        }
    }
}
