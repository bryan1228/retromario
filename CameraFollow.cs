using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [Tooltip("The target (player) to follow")]
    public Transform target;

    [Tooltip("How quickly the camera moves to follow")]
    public float smoothing = 5f;

    [Tooltip("Lowest Y the camera is allowed to go")]
    public float minY = -20f;

    void LateUpdate() {
        if (target == null) return;
        
        float targetY = Mathf.Max(target.position.y, minY);
        Vector3 targetPos = new Vector3(
            target.position.x,
            targetY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothing * Time.deltaTime
        );
    }
}
