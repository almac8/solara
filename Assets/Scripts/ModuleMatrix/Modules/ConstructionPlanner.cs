using UnityEngine;

public class ConstructionPlanner : Module {
  private TerrainCollider terrainCollider;
  private GameObject blueprint = null;

  private void Awake() {
    Title = "Construction Planner";
    Description = "Plan out your building before placing them";

    Activator = new ModuleActivator(false, "Close Construction", "Construction Planner");
    Activator.Activated += ActivateConstructionPlanner;
    Activator.Deactivated += DeactivateConstructionPlanner;

    terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
  }

  private void ActivateConstructionPlanner() {
    GameManager.Instance.UIManager.SetUI(UIManager.UILayout.CONSTRUCTION);
  }

  private void DeactivateConstructionPlanner() {
    GameManager.Instance.UIManager.SetUI(UIManager.UILayout.HUD);
    if(blueprint != null) {
      GameObject.Destroy(blueprint);
    }
  }

  private void Update() {
    if(Activator.IsActive && blueprint != null) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hitData;

      if(terrainCollider.Raycast(ray, out hitData, 1000)) {
        Vector2 tileIndices = GameManager.Instance.MapManager.GetTileIndex(hitData.point);
        Vector3 constructionPosition = GameManager.Instance.MapManager.GetTilePosition(tileIndices);

        if(Vector3.Distance(constructionPosition, transform.position) < 5) {
          blueprint.transform.position = constructionPosition;

          if(Input.GetMouseButtonDown(0)) {
            Instantiate(blueprint, constructionPosition, blueprint.transform.rotation);
          }
        }
      }
    }
  }

  public void SetBlueprint(GameObject constructionReference) {
    if(blueprint != null) GameObject.Destroy(blueprint);

    blueprint = Instantiate(constructionReference, Vector3.zero, constructionReference.transform.rotation);
    blueprint.GetComponent<Unit>().isSelectable = false;
  }
}