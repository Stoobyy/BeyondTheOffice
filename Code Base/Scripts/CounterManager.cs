using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public int counter = 0; // Make counter static
    public SpriteRenderer spriteRenderer1; // First sprite
    public SpriteRenderer spriteRenderer2; // Second sprite
    public Sprite newSprite1Puzzle1; // New sprite for first object
    public Sprite newSprite2Puzzle1; // New sprite for second object
     public Sprite newSprite1Puzzle2; // New sprite for first object
    public Sprite newSprite2Puzzle2; // New sprite for second object
    public Collider2D puzzle1Collider; // The collider to activate
    public Collider2D puzzle2Collider; // The collider to activate
    public DoorTriggerOffice doorTrigger; // Reference to DoorTriggerOffice
    private bool level1Unlocked = false; // Check if level 1 is unlocked
    public static CounterManager Instance { get; private set; }  

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            counter = PlayerPrefs.GetInt("CounterValue", counter);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void start()
    {
        counter = PlayerPrefs.GetInt("CounterValue", counter);
        Debug.Log("Counter Value: " + counter);
    }
    public void IncrementCounter()
    {
        counter++;
        PlayerPrefs.SetInt("CounterValue", counter); // Save counter value
        PlayerPrefs.Save(); // Ensure data is saved
    }


    void Update()
    {
        if (doorTrigger != null && doorTrigger.passCount == 2 && !level1Unlocked)
        {
            level1Unlocked = true; // Set level1Unlocked to true
            if (puzzle1Collider != null)
                puzzle1Collider.enabled = true; // Enable puzzle1Sprite (assuming it's a GameObject with a Collider)
            IncrementCounter(); // Increment counter
        }


        if (counter == 2)
        {
            ChangeSprites1();
            EnableCollider2();
        }

        if (counter == 3)
        {
            ChangeSprites2();
        }
    }


    void ChangeSprites1()
    {
        if (spriteRenderer1 != null && newSprite1Puzzle1 != null)
        {
            spriteRenderer1.sprite = newSprite1Puzzle1;
        }
        if (spriteRenderer2 != null && newSprite2Puzzle1 != null)
        {
            spriteRenderer2.sprite = newSprite2Puzzle1;
        }
    }

    void EnableCollider2()
    {
        if (puzzle2Collider != null)
        {
            puzzle2Collider.enabled = true;
        }
    }

    void ChangeSprites2()
    {
        if (spriteRenderer1 != null && newSprite1Puzzle2 != null)
        {
            spriteRenderer1.sprite = newSprite1Puzzle2;
        }
        if (spriteRenderer2 != null && newSprite2Puzzle2 != null)
        {
            spriteRenderer2.sprite = newSprite2Puzzle2;
        }
    }
}

