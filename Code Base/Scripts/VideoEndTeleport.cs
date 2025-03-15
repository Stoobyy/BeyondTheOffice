using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoEndTeleport : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad;
    public float delay = 3f; // Delay in seconds

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video finished. Waiting for delay...");
        StartCoroutine(DelayedSceneLoad());
    }

    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Loading scene after delay.");
        SceneManager.LoadScene(sceneToLoad);
    }
}
