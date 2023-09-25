using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : Module {
  public float storageUsed;
  public float storageCapacity;

  private float dataWritten;
  private float dataProcessed;
  private float storageDelta;

  public override void RunStep(float deltaTime) {
    storageDelta = dataWritten - dataProcessed;
    storageUsed = Mathf.Clamp(storageUsed + storageDelta, 0.0f, storageCapacity);
    
    dataWritten = 0f;
    dataProcessed = 0f;
  }

  public bool WriteData(float dataToWrite) {
    if(storageUsed + dataToWrite < storageCapacity) {
      dataWritten += dataToWrite;
      return true;
    } else {
      return false;
    }
  }

  public bool ReadData(float dataToRead) {
    if(storageUsed - dataToRead > 0f) {
      dataProcessed += dataToRead;
      return true;
    } else {
      return false;
    }
  }

  public string GetStatusString() {
    return $"{ Mathf.Round(storageUsed) }/{ storageCapacity } ({ storageDelta })";
  }
}