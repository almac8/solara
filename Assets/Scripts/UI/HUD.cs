using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
  private Unit selectedUnit;
  private string unitName;
  private ModuleMatrix moduleMatrix;
  private List<ModuleGauge> gauges;

  private VisualElement rootVisualElement;
  private List<ProgressBar> gaugeVisuals;

  private void Awake() {
    gauges = new List<ModuleGauge>();
    gaugeVisuals = new List<ProgressBar>();
    rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
  }

  private void OnEnable() {
    Unit selectedUnit = SelectionManager.SelectedUnit;

    if(selectedUnit != null) {
      unitName = selectedUnit.gameObject.name;

      moduleMatrix = selectedUnit.GetModuleMatrix();
      if(moduleMatrix == null) {
        Debug.Log("No Module Matrix on this Unit");
      } else if(moduleMatrix.modules.Count > 0) {
        gauges = moduleMatrix.GetGauges();
        
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
    }
  }

  private void Update() {
    for(int i = 0; i < gauges.Count; i++) {
      gaugeVisuals[i].title = gauges[i].Title;
      gaugeVisuals[i].value = gauges[i].Value;
      gaugeVisuals[i].highValue = gauges[i].MaxValue;
    }
  }
}



/* 
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
  [SerializeField] private Drone drone;
  [SerializeField] private ModuleMatrixUI moduleMatrixUI;
  [SerializeField] private ConstructionManager constructionManager;
  

  private VisualElement unitHUD;
  private ProgressBar dataProgressBar;
  private Button deploySolarPanelButton;
  private Button deployDroneButton;
  private Button topographyScanButton;
  private Button analizeDataButton;
  private Button moduleMatrixButton;
  private Button constructionModeButton;
  private Button closeButton;

  private VisualElement resourceHUD;
  private Label resourceLabel;
  private Button collectSampleButton;

  private void Start() {
    unitHUD = root.Q<VisualElement>("unit_hud");

    dataProgressBar = root.Q<ProgressBar>("data_progress_bar");

    deploySolarPanelButton = root.Q<Button>("deploy_solar_panel");
    deploySolarPanelButton.clicked += DeploySolarPanel;

    deployDroneButton = root.Q<Button>("deploy_drone");
    deployDroneButton.clicked += DeployDrone;

    topographyScanButton = root.Q<Button>("topography_scan");
    topographyScanButton.clicked += TopographyScan;

    analizeDataButton = root.Q<Button>("analize_data");
    analizeDataButton.clicked += AnalizeData;

    moduleMatrixButton = root.Q<Button>("module_matrix");
    moduleMatrixButton.clicked += ShowModuleMatrix;

    constructionModeButton = root.Q<Button>("construction_mode");
    constructionModeButton.clicked += EnableConstructionMode;

    closeButton = root.Q<Button>("close");
    closeButton.clicked += Close;
    
    resourceHUD = root.Q<VisualElement>("resource_hud");
    resourceLabel = root.Q<Label>("resource_label");
    
    collectSampleButton = root.Q<Button>("collect_sample");
    collectSampleButton.clicked += CollectSample;
  }

  private void Update() {
    if(SelectionManager.SelectedUnit == null) {
      unitHUD.visible = false;
    } else {
      DataStorage dataStorage = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<DataStorage>();
      dataProgressBar.highValue = dataStorage.storageCapacity;
      dataProgressBar.value = dataStorage.storageUsed;
      dataProgressBar.title = "Data: " + dataStorage.GetStatusString();
    }

    if(SelectionManager.SelectedResource == null) {
      resourceHUD.visible = false;
    } else {
      resourceLabel.text = SelectionManager.SelectedResource.gameObject.name;
      resourceHUD.visible = true;
    }

    if(Input.GetButtonDown("Cancel")) {
      SelectionManager.DeselectAll();
    }
  }

  private void DeploySolarPanel() {
    SolarPanel solarPanel = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<SolarPanel>();
    solarPanel.isDeployed = true;
  }

  private void DeployDrone() {
    DroneDock droneDock = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<DroneDock>();
    droneDock.droneIsDeployed = true;
  }

  private void TopographyScan() {
    TopographyScanner topographyScanner = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<TopographyScanner>();
    topographyScanner.isScanning = true;
  }

  private void AnalizeData() {
    DataAnalizer dataAnalizer = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<DataAnalizer>();
    dataAnalizer.isAnalizing = true;
  }

  private void Close() {
    SelectionManager.DeselectAll();
  }

  private void CollectSample() {
    DroneDock droneDock = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<DroneDock>();

    if(droneDock.droneIsDeployed) {
      drone.Collect(SelectionManager.SelectedResource.gameObject);
    } else {
      Debug.Log("Drone is not Deployed");
    }
  }

  private void ShowModuleMatrix() {
    moduleMatrixUI.Show();
  }

  private void EnableConstructionMode() {
    constructionManager.EnableConstructionMode();
    Close();
  }
}
 */