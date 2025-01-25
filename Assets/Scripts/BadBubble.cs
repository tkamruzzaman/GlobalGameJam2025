using UnityEngine;

public class BadBubble : MonoBehaviour
{
    [Header("Growth")]
    [SerializeField] private float initialSize = 1f;       
    [SerializeField] [Range(2.0f, 5.0f)]private float maxSize = 3f;           
    [SerializeField] [Range(0.01f, 0.5f)]private float growthRate = 0.5f;     
    
    
    [Header("Warning")]
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] [Range(0.75f, 0.9f)]private float warningThreshold = 0.8f;
    
    private Vector3 currentScale;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isWarning;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentScale = Vector3.one * initialSize;
        transform.localScale = currentScale;
    }

    private void Update()
    {
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
        SpawnExplosionEffect();

        Destroy(gameObject);
    }

    private void SpawnExplosionEffect()
    {
        //TODO
    }
}
