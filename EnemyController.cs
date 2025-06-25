using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Patrol Bounds (World-space)")]
    public Transform leftPoint, rightPoint;
    public float speed = 2f;
    public float midPause = 0.7f, endPause = 1f;

    private float leftBoundX, rightBoundX;
    private bool movingRight = true, isAlive = true;
    private Animator anim;
    private SpriteRenderer sr;
    private static readonly int MovingHash = Animator.StringToHash("Moving");

    void Awake()
    {
        anim = GetComponent<Animator>();
        sr   = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        leftBoundX  = leftPoint.position.x;
        rightBoundX = rightPoint.position.x;
        leftPoint.gameObject.SetActive(false);
        rightPoint.gameObject.SetActive(false);
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (isAlive)
        {
            float endpoint = movingRight ? rightBoundX : leftBoundX;
            float midStop  = Random.Range(
                movingRight ? transform.position.x : leftBoundX,
                movingRight ? rightBoundX : transform.position.x
            );

            anim.SetBool(MovingHash, true);
            yield return MoveToX(midStop);
            anim.SetBool(MovingHash, false);
            yield return new WaitForSeconds(midPause);

            anim.SetBool(MovingHash, true);
            yield return MoveToX(endpoint);
            anim.SetBool(MovingHash, false);
            yield return new WaitForSeconds(endPause);

            movingRight = !movingRight;
        }
    }

    IEnumerator MoveToX(float xTarget)
    {
        while (isAlive && Mathf.Abs(transform.position.x - xTarget) > 0.05f)
        {
            float dir = movingRight ? 1f : -1f;
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(xTarget, transform.position.y),
                speed * Time.deltaTime
            );
            sr.flipX = movingRight;
            yield return null;
        }
    }
    
}
