using UnityEngine;

public class WorldSettings {
  public string[] mapSizeNames = new string[] {
    "Small",
    "Medium",
    "Large"
  };
  
  public int[] mapSizeValues = new int[] {
    128,
    256,
    512
  };

  public int worldSeed;
  public int worldSize;

  public GameObject tileObject;
  public float tileWidth;
  public float tileHeight;

  public float topographyScale;
  public float valleyCutoff;
  public float mountainCutoff;
}