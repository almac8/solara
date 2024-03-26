using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {
  public enum UIState {
    MAIN_MENU,
    START,
    LOAD,
    SETTINGS
  };

  [SerializeField] private List<GameObject> uiPanels;

  private GameObject[] uiPanelInstances;

  public UIState ActiveState { get; private set; }

  private void Awake() {
    uiPanelInstances = new GameObject[uiPanels.Count];
    Transform uiCanvasTransform = transform.GetChild(0);
    
    for(int i = 0; i < uiPanels.Count; i++) {
      uiPanelInstances[i] = Instantiate(uiPanels[i], uiCanvasTransform);
      uiPanelInstances[i].SetActive(false);
    }

    SetState(UIState.MAIN_MENU);
  }
  public void SetState(UIState newUIState) {
    ActiveState = newUIState;

    for(int i = 0; i < uiPanelInstances.Length; i++) {
      uiPanelInstances[i].SetActive(false);
    }

    uiPanelInstances[(int)(newUIState)].SetActive(true);
  }
}