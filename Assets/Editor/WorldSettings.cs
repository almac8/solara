using UnityEngine;
using System;
using System.IO;
using System.Text;

[Serializable]
public class WorldSettings {
  [NonSerialized]
  public string[] mapSizeNames = new string[] {
    "Small",
    "Medium",
    "Large"
  };
  
  [NonSerialized]
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

  public string filename;

  public void Save() {
    string json = JsonUtility.ToJson(this);

    using(FileStream stream = File.Open(filename + ".ws", FileMode.Create)) {
      using(BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false)) {
        writer.Write(json);
      }
    }
  }
}