using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlueprintUI : MonoBehaviour {
  [SerializeField] private Blueprint blueprint;
  
  private void OnEnable() {
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button floorsButton = rootVisualElement.Q<Button>("floors");
    Button wallsButton = rootVisualElement.Q<Button>("walls");
    Button unitsButton = rootVisualElement.Q<Button>("units");

    floorsButton.clicked += () => blueprint.SetCurrentTileType("floorsButton");
    wallsButton.clicked += () => blueprint.SetCurrentTileType("wallsButton");
    unitsButton.clicked += () => blueprint.SetCurrentTileType("unitsButton");
  }
}