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

  public WorldSettings Load(string fileToLoad) {
    if (File.Exists(fileToLoad)) {
      string json;

      using(FileStream stream = File.Open(fileToLoad, FileMode.Open)) {
        using(BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false)) {
          json = reader.ReadString();
        }
      }

      return JsonUtility.FromJson<WorldSettings>(json);
    } else {
      Debug.LogError("File does NOT Exist");
      return new WorldSettings();
    }
  }

  public void Save() {
    string json = JsonUtility.ToJson(this);

    using(FileStream stream = File.Open(filename + ".ws", FileMode.Create)) {
      using(BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false)) {
        writer.Write(json);
      }
    }
  }
}