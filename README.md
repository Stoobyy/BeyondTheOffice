# Beyond The Office

## Description
This project consists of multiple Unity C# scripts that handle various gameplay mechanics, such as scene transitions, teleportation, puzzle-solving, and player movement.

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/Stoobyy/BeyondTheOffice/
   ```
2. Open the project in Unity.
3. Attach the scripts to the appropriate GameObjects.

## Scripts Documentation

### 1. SceneFader.cs
Handles scene fading transitions and teleportation.
- **FadeInStart()**: Fades in the scene at the start.
- **FadeIn()**: Performs a manual fade-in.
- **FadeOutAndTeleport(Transform player, Vector3 newPosition)**: Fades out, teleports the player, and fades back in.

### 2. TeleportOnCollision.cs
Teleports the player to a new scene on collision.
- **OnCollisionEnter2D()**: Detects player collision and triggers scene transition.
- **FadeAndSwitchScene()**: Calls `SceneFader` to fade out and load a new scene.

### 3. VideoEndTeleport.cs
Loads a new scene after a video ends.
- **OnVideoEnd()**: Triggers when the video finishes playing.
- **DelayedSceneLoad()**: Waits for a delay before switching scenes.

### 4. OfficePuzzle.cs
Handles an office-themed puzzle with sequence-based door navigation.
- **PlayerEnteredDoor(string doorName)**: Checks if the player follows the correct sequence.
- **ResetPlayer()**: Resets the player's position if they take the wrong path.
- **TeleportWithDelay()**: Teleports the player after completing the puzzle.
- Various effects (e.g., camera shake, flickering lights) enhance immersion.

### 5. DoorTriggerOffice.cs
Manages door interactions and teleportation.
- **OnCollisionEnter2D()**: Triggers teleportation when the player collides with the door.
- **UpdateSpriteWithDelay()**: Updates the door's sprite based on the number of passes.

### 6. OnButtonLobbyPlay.cs
Handles scene transition when a button is clicked.
- **OnMouseDown()**: Deletes player progress and starts a fade-out transition.
- **FadeAndLoadScene()**: Loads the target scene after a fade-out effect.

### 7. CounterManager.cs
Manages puzzle counters and updates sprites based on progress.
- **IncrementCounter()**: Increases the counter and saves progress.
- **ChangeSprites1() / ChangeSprites2()**: Changes object sprites based on counter value.
- **EnableCollider2()**: Enables interaction with the next puzzle.

### 8. DoorTrigger.cs
Handles door collision events in a puzzle environment.
- **OnCollisionEnter2D()**: Calls `OfficePuzzle.PlayerEnteredDoor()` when the player collides with a door.

### 9. DoorCollision.cs (LostWoodsPuzzle)
Manages a directional sequence puzzle.
- **PlayerEnteredDoor(string door)**: Tracks the player's path and checks for correctness.
- **ResetPlayer()**: Resets progress if the player takes a wrong turn.
- **LoadNextLevel()**: Loads the next scene upon completion.

### 10. PlayerControl.cs
Controls player movement.
- **Update()**: Reads input for movement.
- **FixedUpdate()**: Moves the player and updates animation parameters.

## Usage
1. Attach scripts to the relevant GameObjects.
2. Configure the Inspector settings (e.g., assign `sceneToLoad`, `fadeImage`, `correctSequence`).
3. Run the Unity project and interact with objects to trigger the scripts.

## Contributors
- **Amrith Saras** - Scripting
- **Diya Jothish** - UI Design
- **Abhay T P** - Testing and Debugging, Assets
- **Abraham Thomas** - Puzzle Design

## Contact
For any issues or contributions, please open a GitHub issue or contact the developers.

