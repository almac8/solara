using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlueprintUI : MonoBehaviour {
  [SerializeField] private Blueprint blueprint;
  
  private Button floorsButton;
  private Button wallsButton;
  private Button unitsButton;

  private VisualElement floorsSection;
  private VisualElement wallsSection;
  private VisualElement unitsSection;

  private Button blankTileButton;
  private Button floorTileButton;
  private Button floorCornerTileButton;

  private Button wallTileButton;

  private Button cornerCameraTileButton;

  private Blueprint.TileCategory currentlySelectedCategory;
  private Blueprint.TileType currentlySelectedTile;

  private void OnEnable() {
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    floorsButton = rootVisualElement.Q<Button>("floors");
    wallsButton = rootVisualElement.Q<Button>("walls");
    unitsButton = rootVisualElement.Q<Button>("units");

    floorsButton.clicked += () => SetCategory(Blueprint.TileCategory.FLOORS);
    wallsButton.clicked += () => SetCategory(Blueprint.TileCategory.WALLS);
    unitsButton.clicked += () => SetCategory(Blueprint.TileCategory.UNITS);

    floorsSection = rootVisualElement.Q<VisualElement>("floor_tile_types");
    wallsSection = rootVisualElement.Q<VisualElement>("wall_tile_types");
    unitsSection = rootVisualElement.Q<VisualElement>("unit_tile_types");

    blankTileButton = floorsSection.Q<Button>("blank");
    floorTileButton = floorsSection.Q<Button>("floor");
    floorCornerTileButton = floorsSection.Q<Button>("floor_corner");

    blankTileButton.clicked += () => SetTileType(Blueprint.TileType.BLANK);
    floorTileButton.clicked += () => SetTileType(Blueprint.TileType.FLOOR);
    floorCornerTileButton.clicked += () => SetTileType(Blueprint.TileType.FLOOR_CORNER);

    wallTileButton = wallsSection.Q<Button>("wall");

    wallTileButton.clicked += () => SetTileType(Blueprint.TileType.WALL);

    cornerCameraTileButton = unitsSection.Q<Button>("corner_camera");

    cornerCameraTileButton.clicked += () => SetTileType(Blueprint.TileType.CAMERA_CORNER);

    SetCategory(Blueprint.TileCategory.FLOORS);
  }

  private void SetCategory(Blueprint.TileCategory newCategory) {
    currentlySelectedCategory = newCategory;
    blueprint.SetCurrentTileCategory(newCategory);

    switch(newCategory) {
      case Blueprint.TileCategory.FLOORS:
        floorsSection.style.display = DisplayStyle.Flex;
        wallsSection.style.display = DisplayStyle.None;
        unitsSection.style.display = DisplayStyle.None;
        break;

      case Blueprint.TileCategory.WALLS:
        floorsSection.style.display = DisplayStyle.None;
        wallsSection.style.display = DisplayStyle.Flex;
        unitsSection.style.display = DisplayStyle.None;
        break;

      case Blueprint.TileCategory.UNITS:
        floorsSection.style.display = DisplayStyle.None;
        wallsSection.style.display = DisplayStyle.None;
        unitsSection.style.display = DisplayStyle.Flex;
        break;
    }
  }

  private void SetTileType(Blueprint.TileType newTileType) {
    currentlySelectedTile = newTileType;
    blueprint.SetCurrentTileType(newTileType);
  }
}