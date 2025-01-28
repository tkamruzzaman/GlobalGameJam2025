using UnityEngine;

public class BadBubble : MonoBehaviour
{
    [Header("Growth")]
    [SerializeField] private float initialSize = 1f;
    [SerializeField][Range(2.0f, 5.0f)] private float maxSize = 3f;
    [SerializeField][Range(0.01f, 0.5f)] private float growthRate = 0.5f;


    [Header("Warning")]
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField][Range(0.75f, 0.9f)] private float warningThreshold = 0.8f;

    private Vector3 currentScale;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isWarning;
    private bool isExploded;
    private bool isConsumed;
    public AudioClip consumedSound;
    private PlayerController player;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentScale = Vector3.one * initialSize;
        transform.localScale = currentScale;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isExploded || isConsumed) { return; }

        float newSize = currentScale.x + (growthRate * Time.deltaTime);
        currentScale = Vector3.one * newSize;
        transform.localScale = currentScale;

        if (newSize >= maxSize * warningThreshold && !isWarning)
        {
            isWarning = true;
        }

        if (isWarning)
        {
            float warningIntensity = Mathf.InverseLerp(maxSize * warningThreshold, maxSize, newSize);
            spriteRenderer.color = Color.Lerp(originalColor, warningColor, warningIntensity);
        }

        if (newSize >= maxSize)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isExploded = true;
        SpawnExplosionEffect();
        Dead();
        Destroy(gameObject);
        GameManager.Instance.ShowGameOver(false);
    }

    private void SpawnExplosionEffect()
    {
        //TODO
    }

    public void ConsumeByPlayer()
    {
        isConsumed = true;
        ConsumeEffect();
        Dead();
        Destroy(gameObject);
        player.PlaySound(consumedSound);
    }

    private void ConsumeEffect()
    {
        //TODO
    }

    private void Dead()
    {
        GameManager.Instance.RemoveFromBubbleList(this);
    }
}
