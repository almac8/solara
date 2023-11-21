using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

public class WorldGenerator : EditorWindow {
  private const int MIN_MAP_SIZE = 2;
  private const int MAX_MAP_SIZE = 64;

  string[] mapSizeNames = new string[] {
    "One",
    "Two",
  };
  
  int[] mapSizeValues = new int[] {
    2,
    4
  };

  private static string previousScene;
  private static WorldGenerator window;
  private static UnityEngine.SceneManagement.Scene scene;
  
  private GameObject tileObject;
  private int mapSize;
  private float tileWidth;
  private float tileHeight;
  
  [MenuItem("Window/World Generator")]
  private static void ShowWindow() {
    window = GetWindow<WorldGenerator>();
    window.titleContent = new GUIContent("World Generator");
    window.Show();

    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		string currentScene = EditorSceneManager.GetActiveScene().path;
    previousScene = currentScene;
		
		scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
		SceneView.lastActiveSceneView.FrameSelected();
  }
  
  private void OnGUI() {
    EditorGUILayout.BeginVertical();
    tileObject = (GameObject)EditorGUILayout.ObjectField(tileObject, typeof(Object), true);
    mapSize = EditorGUILayout.IntPopup("Map Size: ", mapSize, mapSizeNames, mapSizeValues);
    tileWidth = EditorGUILayout.FloatField("Tile Width: ", tileWidth);
    tileHeight = EditorGUILayout.FloatField("Tile Height: ", tileHeight);
    EditorGUILayout.EndVertical();

    if(GUILayout.Button("Generate")) GenerateMap();

    if(GUILayout.Button("Close")) {
      EditorSceneManager.OpenScene(previousScene);
      window.Close();
    }
  }

  private void GenerateMap() {
    if(tileObject == null) {
      Debug.LogError("Tile has not been assigned");
      return;
    }
    
    instances = (GameObject) PrefabUtility.InstantiatePrefab(tileObject, scene);
  }
}