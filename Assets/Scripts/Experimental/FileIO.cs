using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour {
  private void Awake() {
    string path = Directory.GetCurrentDirectory();

    if(Directory.Exists("saves")) {
      string saveStatePath = path + "/saves/Solara's Veil.ss";
      SaveState saveState = new SaveState().Load(saveStatePath);

      Debug.Log(saveState.name);
    } else {
      string newDirectoryPath = path + "/saves/";
      Directory.CreateDirectory(newDirectoryPath);

      SaveState saveState = new SaveState();
      saveState.name = "Solara's Veil";
      saveState.Save(newDirectoryPath + saveState.name);
    }
  }
}