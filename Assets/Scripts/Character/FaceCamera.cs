using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private bool autoSwapSprites;
    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private float spriteSwapInterval = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Sprite primarySprite;
    private float swapTimer;
    private bool showingAlternateSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            primarySprite = spriteRenderer.sprite;
        }
    }

    private void Update()
    {
        if (Camera.main != null)
        {
            transform.forward = transform.position - Camera.main.transform.position;
        }

        UpdateSpriteSwap();
    }

    public void SetAlternateSpriteVisible(bool visible)
    {
        if (spriteRenderer == null || primarySprite == null || alternateSprite == null)
        {
            return;
        }

        showingAlternateSprite = visible;
        spriteRenderer.sprite = visible ? alternateSprite : primarySprite;
        swapTimer = 0f;
    }

    private void UpdateSpriteSwap()
    {
        if (!autoSwapSprites || spriteRenderer == null || primarySprite == null || alternateSprite == null)
        {
            return;
        }

        if (spriteSwapInterval <= 0f)
        {
            spriteRenderer.sprite = alternateSprite;
            return;
        }

        swapTimer += Time.deltaTime;

        if (swapTimer < spriteSwapInterval)
        {
            return;
        }

        swapTimer = 0f;
        showingAlternateSprite = !showingAlternateSprite;
        spriteRenderer.sprite = showingAlternateSprite ? alternateSprite : primarySprite;
    }
}
