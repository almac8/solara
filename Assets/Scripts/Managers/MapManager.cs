using UnityEngine;

public class MapManager : MonoBehaviour {
  private float tileWidth = 1.73f;
  private float tileHeight = 1.44f;

  public Vector2 GetTileIndex(Vector3 absolutePosition) {
    Vector2 tileIndex = Vector2.zero;

    tileIndex.x = Mathf.FloorToInt(absolutePosition.x / tileWidth);
    tileIndex.y = Mathf.FloorToInt(absolutePosition.z / tileHeight);

    return tileIndex;
  }

  public Vector3 GetTilePosition(Vector2 tileIndex) {
    Vector3 tilePosition = Vector3.zero;

    tilePosition.x = tileIndex.x * tileWidth + tileWidth / 2;
    tilePosition.z = tileIndex.y * tileHeight + tileHeight / 2;

    return tilePosition;
  }
}