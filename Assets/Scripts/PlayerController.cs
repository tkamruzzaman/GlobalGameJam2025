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
    public AudioSource audioSource;

    [Header("Physics Layers")]
    [SerializeField] private LayerMask badBubbleLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.bodyType = RigidbodyType2D.Dynamic;

        playerCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // prevent faster diagonal movement
        if (movement.magnitude > 1f) { movement.Normalize(); }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (1 << other.gameObject.layer == wallLayer)
        {
            // add sound effects // TODO
            //print("WALL");
        }
        if (1 << other.gameObject.layer == badBubbleLayer)
        {
            BadBubble badBubble = other.gameObject.GetComponent<BadBubble>();
            if (badBubble != null)
            {
                print("BAD");
                GameManager.Instance.UpdateScore();
                badBubble.ConsumeByPlayer();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (ContactPoint2D contact in other.contacts)
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