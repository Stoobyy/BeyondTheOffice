using UnityEngine;
using System.Collections;

public class DoorTriggerOffice : MonoBehaviour
{
    public float teleportDistance = 2f;
    private SceneFader sceneFader;
    public int passCount = 0;
    public bool canUpdateSprite = true;
    public SpriteRenderer spriteRenderer; // Assign in Inspector
    public Sprite newSprite1; // Assign in Inspector
    public Sprite newSprite2;

    private void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>(); // Find SceneFader in the scene
    }
    void Awake() {
    DontDestroyOnLoad(gameObject);
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canUpdateSprite && passCount < 2)
            {
                StartCoroutine(UpdateSpriteWithDelay()); // Start coroutine for delayed sprite update
            }

            Vector3 newPosition = collision.transform.position + new Vector3(0, -teleportDistance, 0);
            StartCoroutine(sceneFader.FadeOutAndTeleport(collision.transform, newPosition));
        }
    }

    private IEnumerator UpdateSpriteWithDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before updating the sprite

        if (spriteRenderer != null)
        {
            if (passCount == 0 && newSprite1 != null)
            {
                spriteRenderer.sprite = newSprite1;
            }
            else if (passCount == 1 && newSprite2 != null)
            {
                spriteRenderer.sprite = newSprite2;
            }
        }

        passCount++; // Now increment passCount AFTER the sprite update

        if (passCount >= 2)
        {
            canUpdateSprite = false;
        }
    }
}
