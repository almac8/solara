using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeDatabase : Module {
  public float localTopographyRadius;

  public void Start() {
    Title = "Knowledge Database";
    Description = "\"Universal Know-It-All - Bow Before Its Wisdom.\" The cosmic encyclopedia that even Solara consults. Don't blow it up, or we'll have to start over.";
  }

  public override void RunStep(float deltaTime) {}
}