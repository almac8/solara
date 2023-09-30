using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
  [SerializeField] private Drone drone;
  [SerializeField] private ModuleMatrixUI moduleMatrixUI;
  [SerializeField] private ConstructionManager constructionManager;
  
  private VisualElement root;

  private VisualElement unitHUD;
  private Label unitLabel;
  private ProgressBar powerProgressBar;
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
    root = GetComponent<UIDocument>().rootVisualElement;

    unitHUD = root.Q<VisualElement>("unit_hud");
    unitLabel = root.Q<Label>("unit_label");

    powerProgressBar = root.Q<ProgressBar>("power_progress_bar");
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
      unitLabel.text = SelectionManager.SelectedUnit.gameObject.name;

      PowerStorage powerStorage = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<PowerStorage>();

      powerProgressBar.highValue = powerStorage.chargeCapacity;
      powerProgressBar.value = powerStorage.charge;
      powerProgressBar.title = "Power: " + powerStorage.GetStatusString();

      DataStorage dataStorage = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<DataStorage>();
      dataProgressBar.highValue = dataStorage.storageCapacity;
      dataProgressBar.value = dataStorage.storageUsed;
      dataProgressBar.title = "Data: " + dataStorage.GetStatusString();
      
      unitHUD.visible = true;
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