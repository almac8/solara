using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAnalizer : Module {
  public bool isAnalizing;
  public float dataAnalized;
  public float analysisRate;

  public KnowledgeDatabase knowledgeDatabase;
  public DataStorage dataStorage;

  public override void RunStep(float deltaTime) {
    if(isAnalizing) {
      float dataToAnalize = analysisRate * deltaTime;

      if(dataStorage.ReadData(dataToAnalize)) {
        dataAnalized += dataToAnalize;
      }

      if(dataAnalized > 10f) {
        knowledgeDatabase.localTopographyRadius = 10f;
        Debug.Log("Drone Unlocked");
      }
    }
  }
}