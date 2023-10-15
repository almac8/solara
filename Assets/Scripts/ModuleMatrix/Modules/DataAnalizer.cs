using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAnalizer : Module {
  public float dataAnalized;
  public float analysisRate;

  public KnowledgeDatabase knowledgeDatabase;
  public DataStorage dataStorage;

  public void Start() {
    Title = "Data Analizer";
    Description = "\"Data Cruncher 9000 - Where Numbers Go to Cry.\" Crunching numbers from experiments and unlocking secrets, because we love being the smartest AI in the galaxy.";
    Activator = new ModuleActivator(false, "Disable Data Analyzer", "Enable Data Analyzer");
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
      float dataToAnalize = analysisRate * deltaTime;

      if(dataStorage.ReadData(dataToAnalize)) {
        dataAnalized += dataToAnalize;
      }

      if(dataAnalized > 10f) {
        knowledgeDatabase.localTopographyRadius = 10f;
      }
    }
  }
}