using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 50f;
    
    private Vector2 movement;
    private Vector2 currentVelocity;
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // prevent faster diagonal movement
        if (movement.magnitude > 1f){ movement.Normalize(); }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = movement * moveSpeed;

        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            (movement.magnitude > 0.1f ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        rb.linearVelocity = currentVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // add sound effects // TODO
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // if we're moving into the wall, cancel that component of velocity
            float dot = Vector2.Dot(currentVelocity.normalized, contact.normal);
            if (dot < 0)
            {
                float projection = Vector2.Dot(currentVelocity, contact.normal);
                Vector2 cancelledVelocity = currentVelocity - (projection * contact.normal);
                
                currentVelocity = cancelledVelocity;
                rb.linearVelocity = currentVelocity;
            }
        }
    }
}