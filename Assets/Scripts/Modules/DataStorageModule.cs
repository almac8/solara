using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorageModule {
  private float storageUsed;
  private float storageCapacity;

  private float dataWritten;
  private float dataProcessed;
  private float storageDelta;

  public DataStorageModule(float storageCapacity) {
    this.storageCapacity = storageCapacity;
  }

  public bool WriteData(float dataToWrite) {
    if(storageUsed + dataToWrite < storageCapacity) {
      dataWritten += dataToWrite;
      return true;
    } else {
      return false;
    }
  }

  public void Update() {
    storageDelta = dataWritten - dataProcessed;
    storageUsed = Mathf.Clamp(storageUsed + storageDelta, 0.0f, storageCapacity);
    
    dataWritten = 0f;
    dataProcessed = 0f;
  }

  public string GetStatusString() {
    return $"{ storageUsed }/{ storageCapacity } ({ storageDelta })";
  }
}