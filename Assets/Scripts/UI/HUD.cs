using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
  [SerializeField] private GameObject moduleMatrixUI;

  private Unit selectedUnit;
  private string unitName;
  private ModuleMatrix moduleMatrix;
  private List<ModuleGauge> gauges;
  private List<ModuleActivator> activators;

  private VisualElement rootVisualElement;
  private List<ProgressBar> gaugeVisuals;
  private List<Button> activatorVisuals;
  private Button closeButton;

  private void OnEnable() {
    selectedUnit = SelectionManager.SelectedUnit;

    if(selectedUnit == null) {
      gameObject.SetActive(false);
    } else {
      BuildUnitUI();
    }
  }

  private void Update() {
    for(int i = 0; i < gauges.Count; i++) {
      gaugeVisuals[i].title = gauges[i].Title;
      gaugeVisuals[i].value = gauges[i].Value;
      gaugeVisuals[i].highValue = gauges[i].MaxValue;
    }

    for(int i = 0; i < activators.Count; i++) {
      activatorVisuals[i].text = activators[i].Title;
    }
  }

  private void BuildUnitUI() {
    rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
    
    CollectUnitData();
    SetupModuleGauges();
    SetupModuleActivators();
    SetupModuleMatrixUI();
    SetupCloseButton();
  }

  private void CollectUnitData() {
    unitName = selectedUnit.gameObject.name;
    moduleMatrix = selectedUnit.GetModuleMatrix();
    gauges = new List<ModuleGauge>();
    activators = new List<ModuleActivator>();

    if(moduleMatrix != null && moduleMatrix.modules.Count > 0) {
      gauges = moduleMatrix.GetGauges();
      activators = moduleMatrix.GetActivators();
    }
  }

  private void SetupModuleGauges() {
    gaugeVisuals = new List<ProgressBar>();

    foreach (ModuleGauge gauge in gauges) {
      ProgressBar gaugeVisual = new ProgressBar();

      gaugeVisual.title = gauge.Title;
      gaugeVisual.value = gauge.Value;
      gaugeVisual.highValue = gauge.MaxValue;

      gaugeVisuals.Add(gaugeVisual);

      VisualElement gaugeList = rootVisualElement.Q<VisualElement>("gauge_list");
      gaugeList.Add(gaugeVisual);
    }
  }

  private void SetupModuleActivators() {
    activatorVisuals = new List<Button>();

    foreach (ModuleActivator activator in activators) {
      Button activatorVisual = new Button();

      activatorVisual.text = activator.Title;
      activatorVisual.clicked += activator.Toggle;

      activatorVisuals.Add(activatorVisual);

      VisualElement activatorList = rootVisualElement.Q<VisualElement>("activator_list");
      activatorList.Add(activatorVisual);
    }
  }
  
  private void SetupModuleMatrixUI() {
    Button moduleMatrixButton = rootVisualElement.Q<Button>("module_matrix");

    moduleMatrixButton.clicked += () => {
      moduleMatrixUI.SetActive(true);
      gameObject.SetActive(false);
    };
  }

  private void SetupCloseButton() {
    closeButton = rootVisualElement.Q<Button>("close");

    closeButton.clicked += () => {
      SelectionManager.DeselectAll();
      gameObject.SetActive(false);
    };
  }
}