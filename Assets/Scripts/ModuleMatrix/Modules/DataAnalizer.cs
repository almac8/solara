using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAnalizer : Module {
  private const int DATA_STORAGE_INDEX = 0;
  private const int KNOWLEDGE_DATABASE_INDEX = 1;

  [SerializeField] private float analysisRate;
  private float dataAnalized;

  public void Start() {
    Title = "Data Analizer";
    Description = "\"Data Cruncher 9000 - Where Numbers Go to Cry.\" Crunching numbers from experiments and unlocking secrets, because we love being the smartest AI in the galaxy.";
    Activator = new ModuleActivator(false, "Disable Data Analyzer", "Enable Data Analyzer");

    ModuleRequirement dataStorageRequirement = new ModuleRequirement();
    dataStorageRequirement.SetRequiredModule<DataStorage>("Data Storage");

    ModuleRequirement knowledgeDatabaseRequirement = new ModuleRequirement();
    knowledgeDatabaseRequirement.SetRequiredModule<KnowledgeDatabase>("Knowledge Database");

    Requirements.Add(dataStorageRequirement);
    Requirements.Add(knowledgeDatabaseRequirement);
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
      DataStorage dataStorage = Requirements[DATA_STORAGE_INDEX].AssociatedModule as DataStorage;
      KnowledgeDatabase knowledgeDatabase = Requirements[KNOWLEDGE_DATABASE_INDEX].AssociatedModule as KnowledgeDatabase;

      if(dataStorage != null && knowledgeDatabase != null) {
        float dataToAnalize = analysisRate * deltaTime;
        if(dataStorage.ReadData(dataToAnalize)) dataAnalized += dataToAnalize;
        if(dataAnalized > 10f) knowledgeDatabase.localTopographyRadius = 10f;
      }
    }
  }
}