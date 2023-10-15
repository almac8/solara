using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleActivator {
  private string activeTitle;
  private string inactiveTitle;

  public bool IsActive { get; private set; }

  public event Action Activated;

  public string Title {
    get {
      if(IsActive) {
        return activeTitle;
      } else {
        return inactiveTitle;
      }
    }
  }

  public ModuleActivator(bool initialState, string activeTitle, string inactiveTitle) {
    IsActive = initialState;
    this.activeTitle = activeTitle;
    this.inactiveTitle = inactiveTitle;
  }

  public void Toggle() {
    IsActive = !IsActive;

    if(IsActive) {
      Activated?.Invoke();
    }
  }
}