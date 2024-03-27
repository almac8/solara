using UnityEngine;
using System;
using System.IO;
using System.Text;

[Serializable]
public class SaveState {
  public string name;

  public void Save(string path) {
    string json = JsonUtility.ToJson(this);

    using(FileStream stream = File.Open(path + ".ss", FileMode.Create)) {
      using(BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false)) {
        writer.Write(json);
      }
    }
  }

  public SaveState Load(string path) {
    if (File.Exists(path)) {
      string json;

      using(FileStream stream = File.Open(path, FileMode.Open)) {
        using(BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false)) {
          json = reader.ReadString();
        }
      }

      return JsonUtility.FromJson<SaveState>(json);
    } else {
      Debug.LogError("File does NOT Exist");
      return new SaveState();
    }
  }
}