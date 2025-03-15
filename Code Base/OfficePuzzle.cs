using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal; // For 2D Global Light
using UnityEngine.SceneManagement; // Required for scene management

public class OfficePuzzle : MonoBehaviour
{
    public string[] correctSequence = { "Up", "Left", "Down", "Right" };
    private List<string> playerPath = new List<string>();

    public Transform exitPoint;
    public Transform resetPoint;
    public GameObject player;
    public Camera mainCamera;
    public Light2D globalLight; // 2D Global Light
    public Text dialogueText; // UI Text for dialogue
    public Image fadeImage; // UI Image for fade effect

    private bool isGlitching = false;
    private Color originalCameraColor;
    private Color originalLightColor;
    private float originalLightIntensity;
    private bool firstDoorTouched = false; // Track if first correct door was entered
    public SpriteRenderer spriteRenderer;
     public string sceneToLoad;

    private void Start()
    {
        // Store the original values
        if (mainCamera != null)
            originalCameraColor = mainCamera.backgroundColor;

        if (globalLight != null)
        {
            originalLightColor = globalLight.color;
            originalLightIntensity = globalLight.intensity;
        }

        if (dialogueText != null)
            dialogueText.text = ""; // Hide text at start

        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0); // Fully transparent at start
    }

    public void PlayerEnteredDoor(string doorName)
    {
        Debug.Log("PlayerEnteredDoor called with: " + doorName);
        playerPath.Add(doorName);

        for (int i = 0; i < playerPath.Count; i++)
        {
            if (i >= correctSequence.Length || playerPath[i] != correctSequence[i])
            {
                Debug.LogError($"Wrong Path! Resetting...");
                ResetPlayer();
                return;
            }
        }

        // First door touched? Enable effects from now on.
        if (!firstDoorTouched)
        {
            firstDoorTouched = true;
        }

        // Update dialogue dynamically
        if (dialogueText != null)
            dialogueText.text = $"You entered: {doorName}... Something feels off.";

        // Apply effects **each time** the player enters a correct door
        StartCoroutine(CameraShake(0.15f, 0.1f));
        StartCoroutine(FlickerLights(0.2f));
        StartCoroutine(ChangeCameraColor(0.2f));
        StartCoroutine(FadeScreen(0.2f));
        StartCoroutine(PushPlayerForward(0.3f));

        if (playerPath.Count == correctSequence.Length)
        {
            Debug.Log("Puzzle Completed! Triggering final effects...");
            StartCoroutine(CameraGlitch(0.5f));
            StartCoroutine(DisplayFinalDialogue());
            StartCoroutine(TeleportWithDelay(0.5f));
        }
    }

    private void ResetPlayer()
    {
        if (player == null || resetPoint == null) return;
        player.transform.position = resetPoint.position;
        playerPath.Clear();
        ResetEffects();

        if (dialogueText != null)
            dialogueText.text = "You feel like you took a wrong turn...";
    }

    private IEnumerator TeleportWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TeleportPlayer();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(sceneToLoad);
    }

    void ShowSprite()
    {
        spriteRenderer.enabled = true;
    }

    private void TeleportPlayer()
    {
        if (player == null || exitPoint == null) return;
        player.transform.position = exitPoint.position;
        ShowSprite();
        ResetEffects();
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        if (mainCamera == null) yield break;
        Vector3 originalPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            mainCamera.transform.position = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
    }

    private IEnumerator CameraGlitch(float duration)
    {
        if (isGlitching || mainCamera == null) yield break;
        isGlitching = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            mainCamera.backgroundColor = Random.ColorHSV();
            mainCamera.fieldOfView = Random.Range(55f, 70f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.backgroundColor = originalCameraColor;
        mainCamera.fieldOfView = 60f;
        isGlitching = false;
    }

    private IEnumerator ChangeCameraColor(float duration)
    {
        if (mainCamera == null || !firstDoorTouched) yield break;
        Color originalColor = mainCamera.backgroundColor;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            mainCamera.backgroundColor = Random.ColorHSV();
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.backgroundColor = originalColor;
    }

    private IEnumerator FlickerLights(float duration)
    {
        if (globalLight == null || !firstDoorTouched) yield break;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            globalLight.color = Color.red;
            globalLight.intensity = Random.Range(0.3f, 1f);
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        globalLight.color = originalLightColor;
        globalLight.intensity = originalLightIntensity;
    }

    private IEnumerator FadeScreen(float duration)
    {
        if (fadeImage == null) yield break;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.PingPong(Time.time * 2, 0.4f);
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
    }

    private IEnumerator PushPlayerForward(float moveTime)
    {
        if (player == null) yield break;
        Vector3 startPosition = player.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 0.5f, 0); // Move player slightly forward

        float elapsed = 0f;
        while (elapsed < moveTime)
        {
            player.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = endPosition;
    }

    private void ResetEffects()
    {
        if (mainCamera != null)
            mainCamera.backgroundColor = originalCameraColor;

        if (globalLight != null)
        {
            globalLight.color = originalLightColor;
            globalLight.intensity = originalLightIntensity;
        }

        if (dialogueText != null)
            dialogueText.text = "";

        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0);
    }

    private IEnumerator DisplayFinalDialogue()
    {
        if (dialogueText == null) yield break;
        dialogueText.text = "Something shifts... You've broken the loop.";
        yield return new WaitForSeconds(2f);
        dialogueText.text = "";
    }
}
