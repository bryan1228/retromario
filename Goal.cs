using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Goal : MonoBehaviour
{
    [Tooltip("Tag of the player object")]
    public string playerTag = "Player";

    void Awake()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        UIManager.Instance.ShowGoalScreen();
    }
}
