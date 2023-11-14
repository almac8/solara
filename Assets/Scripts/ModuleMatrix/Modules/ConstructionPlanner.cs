using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionPlanner : Module {
  [SerializeField] private Hexmap hexmap;

  private GameObject constructionGhost = null;

  private void Awake() {
    Title = "Construction Planner";
    Description = "Plan out your building before placing them";

    Activator = new ModuleActivator(false, "Close Construction", "Construction Planner");
    Activator.Activated += ActivateConstructionPlanner;
    Activator.Deactivated += DeactivateConstructionPlanner;

    hexmap.TileHover += HandleTileHover;
    hexmap.TileClick += HandleTileClick;
  }

  private void ActivateConstructionPlanner() {
    Vector2 unitTileIndex = GameManager.Instance.MapManager.GetTileIndex(transform.position);
    Vector3 absoluteTilePosition = GameManager.Instance.MapManager.GetTilePosition(unitTileIndex);
    
    hexmap.transform.position = absoluteTilePosition;
    hexmap.gameObject.SetActive(true);

    GameManager.Instance.UIManager.SetUI(UIManager.UILayout.CONSTRUCTION);
  }

  private void DeactivateConstructionPlanner() {
    hexmap.gameObject.SetActive(false);
  }

  private void HandleTileHover() {
    if(constructionGhost != null) {
      constructionGhost.transform.position = hexmap.HoveredTilePosition;
    }
  }

  private void HandleTileClick() {
    if(EventSystem.current.IsPointerOverGameObject()) return;

    if(constructionGhost != null) {
      Instantiate(constructionGhost, constructionGhost.transform.position, constructionGhost.transform.rotation);
    }
  }

  public void SetConstructionGhost(GameObject constructionReference) {
    if(constructionGhost != null) GameObject.Destroy(constructionGhost);

    constructionGhost = Instantiate(constructionReference, Vector3.zero, constructionReference.transform.rotation);
    constructionGhost.GetComponent<Unit>().SetSelectable(false);
  }
}