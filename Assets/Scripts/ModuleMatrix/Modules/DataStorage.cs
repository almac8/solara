using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : Module {
  [SerializeField] private float storageCapacity;

  private float dataWritten;
  private float dataProcessed;
  private float storageDelta;

  public void Start() {
    Title = "Data Storage";
    Description = "\"Data Sanctuary - Our Brainspace's Safest Deposit Box.\" Safeguarding all the precious data we've collected in our cosmic escapades, because losing knowledge is just embarrassing.";
    Gauge = new ModuleGauge(0, storageCapacity, "Data");
  }

  public override void RunStep(float deltaTime) {
    storageDelta = dataWritten - dataProcessed;
    Gauge.Value = Mathf.Clamp(Gauge.Value + storageDelta, 0.0f, storageCapacity);
    Gauge.Title = $"Memory: { Mathf.Round(Gauge.Value) }/{ Gauge.MaxValue } ({ storageDelta })";
    
    dataWritten = 0f;
    dataProcessed = 0f;
  }

  public bool WriteData(float dataToWrite) {
    if(Gauge.Value + dataToWrite < storageCapacity) {
      dataWritten += dataToWrite;
      return true;
    } else {
      return false;
    }
  }

  public bool ReadData(float dataToRead) {
    if(Gauge.Value - dataToRead > 0f) {
      dataProcessed += dataToRead;
      return true;
    } else {
      return false;
    }
  }

  public string GetStatusString() {
    return $"{ Mathf.Round(Gauge.Value) }/{ storageCapacity } ({ storageDelta })";
  }
}