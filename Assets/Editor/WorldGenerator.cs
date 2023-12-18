using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WorldGenerator : EditorWindow {
  private enum UISection {
    LOAD,
    WORLD,
    TILE,
    SCENE,
    TOPOGRAPHY,
    SAVE
  }

  private static WorldGenerator window;
  private static WorldGeneratorSceneManager worldGeneratorSceneManager;

  public static WorldSettings WorldSettings { get; private set; }
  public static MapGenerator MapGenerator { get; private set; }

  private static Dictionary<UISection, bool> foldouts;
  private static bool hasGenerated;
  private static int loadFileIndex;
  
  [MenuItem("Window/World Generator")]
  private static void ShowWindow() {
    worldGeneratorSceneManager = new WorldGeneratorSceneManager();
    worldGeneratorSceneManager.OpenScene();

    WorldSettings = new WorldSettings();

    foldouts = new Dictionary<UISection, bool>();
    foldouts.Add(UISection.LOAD, false);
    foldouts.Add(UISection.WORLD, false);
    foldouts.Add(UISection.TILE, false);
    foldouts.Add(UISection.SCENE, false);
    foldouts.Add(UISection.TOPOGRAPHY, false);
    foldouts.Add(UISection.SAVE, false);

    hasGenerated = false;
    loadFileIndex = 0;

    window = GetWindow<WorldGenerator>();
    window.titleContent = new GUIContent("World Generator");
    window.Show();
  }
  
  private void OnGUI() {
    EditorGUILayout.BeginVertical();
    
    ShowLoadFoldout();
    ShowWorldFoldout();
    if(WorldSettings.worldSeed != 0 && WorldSettings.worldSize != 0) ShowTileFoldout();
    if(WorldSettings.tileObject != null && WorldSettings.tileWidth != 0 && WorldSettings.tileHeight != 0) ShowTopographyFoldout();
    if(hasGenerated) {
      ShowSceneFoldout();
      ShowSaveFoldout();
    }
    ShowCloseButton();

    EditorGUILayout.EndVertical();
  }

  private void ShowLoadFoldout() {
    IEnumerable<string> filenamesEnumerable = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.ws");

    if(filenamesEnumerable.Count() > 0) {
      foldouts[UISection.LOAD] = EditorGUILayout.BeginFoldoutHeaderGroup(foldouts[UISection.LOAD], "Load");

      if(foldouts[UISection.LOAD]) {
        string[] filenames = filenamesEnumerable.Cast<string>().ToArray();
        for(int i = 0; i < filenames.Length; i++) {
          int slashIndex = filenames[i].LastIndexOf('/') + 1;
          int substringLength = filenames[i].Length - slashIndex;
          filenames[i] = filenames[i].Substring(slashIndex, substringLength);
        }

        loadFileIndex = EditorGUILayout.Popup(loadFileIndex, filenames);
        if(GUILayout.Button("Load")) WorldSettings = WorldSettings.Load(filenames[loadFileIndex]);
      }

      EditorGUILayout.EndFoldoutHeaderGroup();
    }
  }

  private void ShowWorldFoldout() {
    foldouts[UISection.WORLD] = EditorGUILayout.BeginFoldoutHeaderGroup(foldouts[UISection.WORLD], "World");

    if(foldouts[UISection.WORLD]) {
      WorldSettings.worldSeed = EditorGUILayout.IntField("World Seed: ", WorldSettings.worldSeed);
      WorldSettings.worldSize = EditorGUILayout.IntPopup("World Size: ", WorldSettings.worldSize, WorldSettings.mapSizeNames, WorldSettings.mapSizeValues);
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private void ShowTileFoldout() {
    foldouts[UISection.TILE] = EditorGUILayout.BeginFoldoutHeaderGroup(foldouts[UISection.TILE], "Tile");
    
    if(foldouts[UISection.TILE]) {
      WorldSettings.tileObject = (GameObject)EditorGUILayout.ObjectField(WorldSettings.tileObject, typeof(Object), true);
      WorldSettings.tileWidth = EditorGUILayout.FloatField("Tile Width: ", WorldSettings.tileWidth);
      WorldSettings.tileHeight = EditorGUILayout.FloatField("Tile Height: ", WorldSettings.tileHeight);
    }
    
    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private void ShowTopographyFoldout() {
    foldouts[UISection.TOPOGRAPHY] = EditorGUILayout.BeginFoldoutHeaderGroup(foldouts[UISection.TOPOGRAPHY], "Topography");
    
    if(foldouts[UISection.TOPOGRAPHY]) {
      WorldSettings.topographyScale = EditorGUILayout.FloatField("Topography Scale: ", WorldSettings.topographyScale);
      WorldSettings.valleyCutoff = EditorGUILayout.FloatField("Topography Cutoff: ", WorldSettings.valleyCutoff);
      WorldSettings.mountainCutoff = EditorGUILayout.FloatField("Topography Cutoff: ", WorldSettings.mountainCutoff);
      if(GUILayout.Button("Generate Topography")) GenerateWorld();
    }
    
    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private void ShowSceneFoldout() {
    foldouts[UISection.SCENE] = EditorGUILayout.BeginFoldoutHeaderGroup(foldouts[UISection.SCENE], "Scene");
    
    if(foldouts[UISection.SCENE]) {
      if(GUILayout.Button("Toggle Tiles")) worldGeneratorSceneManager.ToggleTiles();
      if(GUILayout.Button("Toggle Terrain")) worldGeneratorSceneManager.ToggleTerrain();
    }
    
    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private void ShowSaveFoldout() {
    foldouts[UISection.SAVE] = EditorGUILayout.BeginFoldoutHeaderGroup(foldouts[UISection.SAVE], "Save");

    if(foldouts[UISection.SAVE]) {
      WorldSettings.filename = EditorGUILayout.TextField("File Name: ", WorldSettings.filename);
      if(GUILayout.Button("Save")) WorldSettings.Save();
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private void ShowCloseButton() {
    if(GUILayout.Button("Close")) {
      worldGeneratorSceneManager.CloseScene();
      window.Close();
    }
  }

  private void GenerateWorld() {
    MapGenerator = new MapGenerator(WorldSettings.worldSize, WorldSettings.worldSeed);
    MapGenerator.GenerateTopography(WorldSettings.topographyScale, WorldSettings.valleyCutoff, WorldSettings.mountainCutoff);
    hasGenerated = true;
    worldGeneratorSceneManager.RefreshScene();
  }
}