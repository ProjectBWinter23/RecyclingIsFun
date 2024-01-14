using UnityEngine;
using DG.Tweening;

public class BouncingSpriteFallAnimation : MonoBehaviour
{
    public Transform[] spriteTransforms;
    public Transform[] originalSpriteTransforms;
    public float fallDuration = 2f;
    public float bounceHeight = 1f;
    public float startY = 5f;
    public float endY = -5f;
    public float minDelay = 0.1f;
    public float maxDelay = 1.0f;


    private void Awake()
    {
        originalSpriteTransforms = new Transform[spriteTransforms.Length];
        for (int i = 0; i < spriteTransforms.Length; i++)
        {
            originalSpriteTransforms[i] = spriteTransforms[i];
        }
    }
    void OnEnable()
    {
        // Reset sprites to their start position
        
        ResetSpritePositions();

        // Animate each sprite falling from the top with a random delay and bounce
        foreach (Transform spriteTransform in spriteTransforms)
        {
            AnimateSpriteFall(spriteTransform);
        }
    }

    void ResetSpritePositions()
    {
        for (int i = 0; i < spriteTransforms.Length; i++)
        {
            spriteTransforms[i].position = originalSpriteTransforms[i].position;
        }
    }

    void AnimateSpriteFall(Transform spriteTransform)
    {
        // Generate a random delay for each sprite
        float randomDelay = Random.Range(minDelay, maxDelay);

        // Use DOTween to animate the sprite falling to the bottom with a bounce and random delay
        spriteTransform.DOMoveY(endY, fallDuration)
            .SetEase(Ease.OutBounce)  // Adjust the easing function if needed
            .SetDelay(randomDelay)
            .OnComplete(() => RestartAnimation(spriteTransform)); // Restart animation when it's complete
    }

    void RestartAnimation(Transform spriteTransform)
    {
        // Reset the position of the sprite
        spriteTransform.position = new Vector3(spriteTransform.position.x, startY, spriteTransform.position.z);

        // Restart the fall animation
        AnimateSpriteFall(spriteTransform);
    }

    private void OnDisable()
    {
        ResetSpritePositions();
    }
}
