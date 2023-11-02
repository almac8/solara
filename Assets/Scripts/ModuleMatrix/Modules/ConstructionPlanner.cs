using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionPlanner : Module {
  [SerializeField] private Hexmap hexmap;
  [SerializeField] private GameObject constructionUiPanel;

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
    hexmap.gameObject.SetActive(true);
    constructionUiPanel.SetActive(true);
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