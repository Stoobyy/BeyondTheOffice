using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import this to change scenes

public class LostWoodsPuzzle : MonoBehaviour
{
    public string[] correctSequence = { "Up", "Left", "Down", "Right" }; // Customize this sequence
    private List<string> playerPath = new List<string>();

    public void PlayerEnteredDoor(string door)
    {
        playerPath.Add(door);
        Debug.Log("Player entered: " + door);

        // Check if the player follows the correct order
        for (int i = 0; i < playerPath.Count; i++)
        {
            if (i >= correctSequence.Length || playerPath[i] != correctSequence[i])
            {
                Debug.Log("Wrong Path! Resetting...");
                ResetPlayer();
                return;
            }
        }

        // If they finish the sequence correctly, load the next scene
        if (playerPath.Count == correctSequence.Length)
        {
            Debug.Log("Puzzle Completed! Loading next level...");
            LoadNextLevel();
        }
    }

    private void ResetPlayer()
    {
        playerPath.Clear();
        Debug.Log("Player sent back to start!");
        // Optionally teleport player back to a starting position
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene("puzzle1"); // Change this to the next scene name
    }
}

