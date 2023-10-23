using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : Module {
  private const int TARGETING_INDEX = 0;
  private const int KNOWLEDGE_DATABASE_INDEX = 1;

  [SerializeField] private float movementSpeed;

  private Vector3 homeDockPosition = Vector3.zero;
  private Vector3 movement = Vector3.zero;

  private void Awake() {
    Title = "Hover Movement";
    Description = "Creates Movement";
    homeDockPosition = transform.position;

    ModuleRequirement targetingRequirement = new ModuleRequirement();
    targetingRequirement.SetRequiredModule<Targeting>("Targeting");

    ModuleRequirement knowledgeDatabaseRequirement = new ModuleRequirement();
    knowledgeDatabaseRequirement.SetRequiredModule<KnowledgeDatabase>("Knowledge Database");

    Requirements.Add(targetingRequirement);
    Requirements.Add(knowledgeDatabaseRequirement);
  }

  public override void RunStep(float deltaTime) {
    transform.Translate(Vector3.Normalize(movement) * movementSpeed * deltaTime);
    movement = Vector3.zero;
  }

  public void ApplyMovement(Vector3 newMovement) {
    Targeting targeting = Requirements[TARGETING_INDEX].AssociatedModule as Targeting;
    KnowledgeDatabase knowledgeDatabase = Requirements[KNOWLEDGE_DATABASE_INDEX].AssociatedModule as KnowledgeDatabase;

    if(targeting != null && knowledgeDatabase != null) {
      float distanceToTarget = Vector3.Distance(homeDockPosition, targeting.Target.transform.position);
      if(distanceToTarget < knowledgeDatabase.localTopographyRadius) movement += newMovement;
    }
  }
}