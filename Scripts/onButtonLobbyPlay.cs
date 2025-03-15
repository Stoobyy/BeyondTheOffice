using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OnButtonLobbyPlay : MonoBehaviour
{
    public string sceneName;  // Assign the target scene name in the Inspector
    public Image fadeImage;   // Drag the FadePanel's Image component here
    public float fadeDuration = 1f;
    public static OnButtonLobbyPlay Instance { get; private set; }

    void OnMouseDown()
    {
        PlayerPrefs.DeleteKey("CounterValue");
        PlayerPrefs.DeleteKey("passValue");
        PlayerPrefs.Save();
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        // Ensure the fade panel is active
        fadeImage.gameObject.SetActive(true);

        // Fade out (black screen appears)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);

        // Load the next scene
        SceneManager.LoadScene(sceneName);

        // Wait for one frame for the scene to load
        yield return null;
    }
}
