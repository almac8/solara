using UnityEngine;

public class ConstructionManager : MonoBehaviour {
  [SerializeField] private GameObject constructionReference;

  private bool constructionModeEnabled;
  private GameObject constructionGhost;
  private TerrainCollider terrainCollider;

  private void Start() {
    terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
  }

  private void Update() {
    if(constructionModeEnabled) {
      if(Input.GetButtonDown("Cancel")) {
        DisableConstructionMode();
      }

      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hitData;

      if(terrainCollider.Raycast(ray, out hitData, 1000)) {
        Vector3 constructionPosition = new Vector3(Mathf.Round(hitData.point.x), Mathf.Round(hitData.point.y), Mathf.Round(hitData.point.z));
        constructionGhost.transform.position = constructionPosition;

        if(Input.GetMouseButtonDown(0)) {
          Instantiate(constructionGhost, constructionPosition, constructionGhost.transform.rotation);
        }
      }
    }
  }

  public void EnableConstructionMode() {
    constructionModeEnabled = true;
    constructionGhost = Instantiate(constructionReference, Vector3.zero, constructionReference.transform.rotation);
  }

  public void DisableConstructionMode() {
    constructionModeEnabled = false;
    Destroy(constructionGhost);
  }
}