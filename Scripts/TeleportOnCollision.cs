using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeleportOnCollision : MonoBehaviour
{
    public string sceneToLoad;
    private SceneFader sceneFader;

    void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>(); // Find the SceneFader in the scene
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeAndSwitchScene(collision.gameObject));
        }
    }

    IEnumerator FadeAndSwitchScene(GameObject player)
{
    
    // Start fade out and wait for it to complete
    yield return StartCoroutine(sceneFader.FadeOutAndTeleport(player.transform, Vector3.zero));
    
    // Store player position data in a persistent manager if needed
    // PlayerPositionManager.Instance.SetTargetPosition(targetPosition);
    
    // Now load the scene
    SceneManager.LoadScene(sceneToLoad);
    
    // No need to call FadeIn here, as the new scene's Start() will handle it
}


}
