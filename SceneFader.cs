
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

void Awake()
{
    DontDestroyOnLoad(gameObject);
}


    void Start()
    {
        StartCoroutine(FadeInStart());
    }

    public IEnumerator FadeInStart()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1); // Ensure full black at the start

        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // Ensure fully transparent
        fadeImage.gameObject.SetActive(false);   // Hide panel after fade-in
    }
    public IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1); // Ensure full black at the start

        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // Ensure fully transparent
        fadeImage.gameObject.SetActive(false);   // Hide panel after fade-in
    }


    public IEnumerator FadeOutAndTeleport(Transform player, Vector3 newPosition)
{
    if (!fadeImage.gameObject.activeSelf)
    {
        fadeImage.gameObject.SetActive(true); // Ensure it's active before starting
    }

    fadeImage.color = new Color(0, 0, 0, 0); // Start fully transparent

    for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
    {
        float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
        fadeImage.color = new Color(0, 0, 0, alpha);
        yield return null;
    }

    fadeImage.color = new Color(0, 0, 0, 1); // Fully black

    // Teleport player after full fade-out
    player.position = newPosition;

    yield return new WaitForSeconds(0.5f); // Optional delay before fading in

    StartCoroutine(FadeIn()); // Start fade-in after teleport
}

}
