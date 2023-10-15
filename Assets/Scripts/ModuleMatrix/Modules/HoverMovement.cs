using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : Module {
  [SerializeField] private float movementSpeed;

  private Vector3 homeDockPosition = Vector3.zero;
  private Vector3 movement = Vector3.zero;
  private Targeting targeting = null;
  private KnowledgeDatabase knowledgeDatabase = null;

  private void Awake() {
    homeDockPosition = transform.position;
    targeting = GetComponent<Targeting>();
    knowledgeDatabase = transform.parent.gameObject.GetComponent<KnowledgeDatabase>();
  }

  public override void RunStep(float deltaTime) {
    transform.Translate(Vector3.Normalize(movement) * movementSpeed * deltaTime);
    movement = Vector3.zero;
  }

  public void ApplyMovement(Vector3 newMovement) {
    float distanceToTarget = Vector3.Distance(homeDockPosition, targeting.Target.transform.position);
    if(distanceToTarget < knowledgeDatabase.localTopographyRadius) movement += newMovement;
  }
}