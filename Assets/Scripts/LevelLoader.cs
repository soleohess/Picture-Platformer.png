using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab {
	public Color32 color;
	public GameObject prefab;
}

public class LevelLoader : MonoBehaviour {

	public string levelFileName;

	//public Texture2D levelMap;

	public ColorToPrefab[] colorToPrefab;


	// Use this for initialization
	void Awake () {
		LoadMap();
		GameObject mainCamera = GameObject.FindWithTag("MainCamera");
		//mainCamera.Start();
	}

	void Start()
	{
		
	}

	void EmptyMap() {
		// Find all of our children and...eliminate them.

		while(transform.childCount > 0) {
			Transform c = transform.GetChild(0);
			c.SetParent(null); // become Batman
			Destroy(c.gameObject); // become The Joker
		}
	}

	void LoadAllLevelNames() {
		// Read the list of files from StreamingAssets/Levels/*.png
		// The player will progress through the levels alphabetically
	}

	void LoadMap() {
		EmptyMap();

		// Read the image data from the file in StreamingAssets
		string filePath = Application.dataPath + "/StreamingAssets/" + levelFileName;
		byte[] bytes = System.IO.File.ReadAllBytes(filePath);
		Texture2D levelMap = new Texture2D(2, 2);
		levelMap.LoadImage(bytes);


		// Get the raw pixels from the level image map
		Color32[] allPixels = levelMap.GetPixels32();
		int width = levelMap.width;
		int height = levelMap.height;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				SpawnTileAt( allPixels[(y * width) + x], x, y );
			}
		}
	}

	void SpawnTileAt( Color32 c, int x, int y ) {

		// If this is a transparent pixel, then it's meant to just be empty.
		if(c.a <= 0) 
		{
			return;
		}

		// Find the right color in our map

		// NOTE: This isn't optimized. You should have a dictionary lookup for max speed
		foreach(ColorToPrefab ctp in colorToPrefab) {
			
			if( c.Equals(ctp.color) ) 
			{
				// Spawn the prefab at the right location
				GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity );
				go.transform.SetParent(this.transform);
				// maybe do more stuff to the gameObject here?
				return;
			}
		}

		// If we got to this point, it means we did not find a matching color in our array.

		Debug.LogError("No color to prefab found for: " + c.ToString() );

	}
	
}
/*
 * here's what Chat GPT says

   csharp
   using UnityEngine;
   using UnityEngine.SceneManagement;
   
   public class LevelManager : MonoBehaviour
   {
   // Array to store the names of your level scenes
   public string[] levelScenes;
   
   // Index to keep track of the current level
   private int currentLevelIndex = 0;
   
   // Function to load the next level
   public void LoadNextLevel()
   {
   // Check if there are more levels to load
   if (currentLevelIndex < levelScenes.Length - 1)
   {
   currentLevelIndex++;
   SceneManager.LoadScene(levelScenes[currentLevelIndex]);
   }
   else
   {
   Debug.Log("No more levels to load."); // You can handle this case as needed
   }
   }
   
   // Function to load a specific level by index
   public void LoadLevel(int index)
   {
   if (index >= 0 && index < levelScenes.Length)
   {
   currentLevelIndex = index;
   SceneManager.LoadScene(levelScenes[currentLevelIndex]);
   }
   else
   {
   Debug.LogError("Invalid level index.");
   }
   }
   
   // Function to reload the current level
   public void ReloadCurrentLevel()
   {
   SceneManager.LoadScene(levelScenes[currentLevelIndex]);
   }
   }
   Here's how you can use this script:
   
   Attach this script to a GameObject in your scene.
   Populate the levelScenes array in the inspector with the names of your level scenes. Make sure these scenes are added to your build settings.
   Call the LoadNextLevel() function when you want to load the next level, such as when the player completes the current level.
   If you want to load a specific level by index, you can call LoadLevel(index).
   If you need to reload the current level, you can call ReloadCurrentLevel().
   This script assumes that each level is represented by a separate scene in your Unity project, and you have already set up the scenes accordingly.
   
   using System;
   using UnityEngine;
   using System.Collections;
   
   [System.Serializable]
   public class ColorToPrefab {
   public Color32 color;
   public GameObject prefab;
   }
   
   public class LevelLoader : MonoBehaviour {
   
   public string levelFileName;
   
   //public Texture2D levelMap;
   
   public ColorToPrefab[] colorToPrefab;
   
   
   // Use this for initialization
   void Awake () {
   LoadMap();
   GameObject mainCamera = GameObject.FindWithTag("MainCamera");
   //mainCamera.Start();
   }
   
   void Start()
   {
   
   }
   
   void EmptyMap() {
   // Find all of our children and...eliminate them.
   
   while(transform.childCount > 0) {
   Transform c = transform.GetChild(0);
   c.SetParent(null); // become Batman
   Destroy(c.gameObject); // become The Joker
   }
   }
   
   void LoadAllLevelNames() {
   // Read the list of files from StreamingAssets/Levels/*.png
   // The player will progress through the levels alphabetically
   }
   
   void LoadMap() {
   EmptyMap();
   
   // Read the image data from the file in StreamingAssets
   string filePath = Application.dataPath + "/StreamingAssets/" + levelFileName;
   byte[] bytes = System.IO.File.ReadAllBytes(filePath);
   Texture2D levelMap = new Texture2D(2, 2);
   levelMap.LoadImage(bytes);
   
   
   // Get the raw pixels from the level image map
   Color32[] allPixels = levelMap.GetPixels32();
   int width = levelMap.width;
   int height = levelMap.height;
   
   for (int x = 0; x < width; x++)
   {
   for (int y = 0; y < height; y++)
   {
   SpawnTileAt( allPixels[(y * width) + x], x, y );
   }
   }
   }
   
   void SpawnTileAt( Color32 c, int x, int y ) {
   
   // If this is a transparent pixel, then it's meant to just be empty.
   if(c.a <= 0) 
   {
   return;
   }
   
   // Find the right color in our map
   
   // NOTE: This isn't optimized. You should have a dictionary lookup for max speed
   foreach(ColorToPrefab ctp in colorToPrefab) {
   
   if( c.Equals(ctp.color) ) 
   {
   // Spawn the prefab at the right location
   GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity );
   go.transform.SetParent(this.transform);
   // maybe do more stuff to the gameObject here?
   return;
   }
   }
   
   // If we got to this point, it means we did not find a matching color in our array.
   
   Debug.LogError("No color to prefab found for: " + c.ToString() );
   
   }
   
   }
   ChatGPT
   It looks like you have a solid foundation for loading levels from PNG images using colors to instantiate prefabs in Unity.
   To implement loading the next level after completing the current one, you can integrate the LevelLoader script with the LevelManager script I provided earlier.
   Here's how you can modify your LevelLoader script to accommodate this:
   
   csharp
   Copy code
   using UnityEngine;
   
   public class LevelLoader : MonoBehaviour
   {
   public string[] levelFileNames; // Array to store the file names of your levels
   private int currentLevelIndex = 0; // Index to keep track of the current level
   
   public ColorToPrefab[] colorToPrefab;
   
   void Awake()
   {
   LoadMap();
   }
   
   void LoadMap()
   {
   // Your existing code for loading the level from a PNG image goes here
   }
   
   void SpawnTileAt(Color32 c, int x, int y)
   {
   // Your existing code for spawning tiles from colors goes here
   }
   
   // Function to load the next level
   public void LoadNextLevel()
   {
   if (currentLevelIndex < levelFileNames.Length - 1)
   {
   currentLevelIndex++;
   LoadMap(); // Load the next level
   }
   else
   {
   Debug.Log("No more levels to load."); // You can handle this case as needed
   }
   }
   }
   Here's how you can use this modified script:
   
   Attach the LevelLoader script to a GameObject in your scene.
   Populate the levelFileNames array with the file names of your level PNG images.
   Integrate the LevelLoader script with the LevelManager script by calling LoadNextLevel() from the LevelManager when the player completes the current level.
   With this setup, when the player completes a level, you can call LoadNextLevel() from your LevelManager, which will trigger the LevelLoader to load the next level.
   Make sure you have proper scene management and prefab setup for each level.
 */
