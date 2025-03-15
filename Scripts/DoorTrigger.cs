using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string doorName; // Set this in the Inspector for each door

    private void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collision detected with: " + collision.gameObject.name);

    if (collision.gameObject.CompareTag("Player")) // Check if it's the player
    {
        Debug.Log("Player collided with door: " + doorName);
        FindObjectOfType<OfficePuzzle>().PlayerEnteredDoor(doorName);
    }
}
}
