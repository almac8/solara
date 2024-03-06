using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
  public List<Camera> CameraList { get; private set; }
  
  [SerializeField] private Camera blueprintCamera;

  private bool isBlueprintActive;
  private int currentCameraIndex;

  private void Start() {
    CameraList = new List<Camera>();
    isBlueprintActive = false;
    currentCameraIndex = -1;
  }

  private void Update() {
    if(CameraList.Count < 1 && !isBlueprintActive) {
      ActivateBlueprint();
    } else {
      if(Input.GetKeyDown(KeyCode.M)) {
        ToggleBlueprint();
      }
      
      if(!isBlueprintActive) {
        if(Input.GetKeyDown(KeyCode.O)) {
          PreviousCamera();
        }
        
        if(Input.GetKeyDown(KeyCode.P)) {
          NextCamera();
        }
      }
    }
  }

  private void ToggleBlueprint() {
    isBlueprintActive = !isBlueprintActive;

    if(isBlueprintActive) {
      ActivateBlueprint();
    } else {
      DeactivateBlueprint();
    }
  }

  private void ActivateBlueprint() {
    isBlueprintActive = true;
    DeactivateCamera();
    blueprintCamera.gameObject.SetActive(isBlueprintActive);
  }

  private void DeactivateBlueprint() {
    if(CameraList.Count > 0) {
      isBlueprintActive = false;
      blueprintCamera.gameObject.SetActive(isBlueprintActive);
      ActivateCamera();
    } else {
      Debug.Log("Could not Deactivate Blueprint: No active Cameras");
    }
  }

  private void NextCamera() {
    if(CameraList.Count > 1) {
      DeactivateCamera();

      currentCameraIndex++;
      if(currentCameraIndex > CameraList.Count - 1) {
        currentCameraIndex = 0;
      }

      ActivateCamera();
    }
  }

  private void PreviousCamera() {
    if(CameraList.Count > 1) {
      DeactivateCamera();
      
      currentCameraIndex--;
      if(currentCameraIndex < 0) {
        currentCameraIndex = CameraList.Count - 1;
      }

      ActivateCamera();
    }
  }

  private void ActivateCamera() {
    if(currentCameraIndex < 0 && CameraList.Count > 0) {
      currentCameraIndex = 0;
    }

    CameraList[currentCameraIndex].gameObject.SetActive(true);
  }

  private void DeactivateCamera() {
    if(currentCameraIndex >= 0) {
      CameraList[currentCameraIndex].gameObject.SetActive(false);
    }
  }

  public void RegisterCamera(Camera newCamera) {
    CameraList.Add(newCamera);
  }

  public void UnregisterCamera(Camera oldCamera) {
    CameraList.Remove(oldCamera);
  }
}