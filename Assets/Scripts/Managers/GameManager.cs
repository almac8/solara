using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionManager))]
[RequireComponent(typeof(UnitManager))]
[RequireComponent(typeof(MapManager))]
[RequireComponent(typeof(UIManager))]

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }
  
  public SelectionManager SelectionManager { get; private set; }
  public UnitManager UnitManager { get; private set; }
  public MapManager MapManager { get; private set; }
  public UIManager UIManager { get; private set; }

  private void Awake() {
    if(Instance != null && Instance != this) {
      Destroy(this);
      return;
    }

    Instance = this;
    
    SelectionManager = GetComponent<SelectionManager>();
    UnitManager = GetComponent<UnitManager>();
    MapManager = GetComponent<MapManager>();
    UIManager = GetComponent<UIManager>();
  }
}