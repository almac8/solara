using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
  private VisualElement root;

  private VisualElement unitHUD;
  private Label unitLabel;
  private Label resourceLabel;
  private ProgressBar powerProgressBar;
  private Button deploySolarPanelButton;
  private Button deployDroneButton;
  private Button topographyScanButton;
  private Button closeButton;

  private void Start() {
    root = GetComponent<UIDocument>().rootVisualElement;

    unitHUD = root.Q<VisualElement>("unit_hud");
    unitLabel = root.Q<Label>("unit_label");
    resourceLabel = root.Q<Label>("resource_label");
    powerProgressBar = root.Q<ProgressBar>("power_progress_bar");

    deploySolarPanelButton = root.Q<Button>("deploy_solar_panel");
    deploySolarPanelButton.clicked += DeploySolarPanel;

    deployDroneButton = root.Q<Button>("deploy_drone");
    deployDroneButton.clicked += DeployDrone;

    topographyScanButton = root.Q<Button>("topography_scan");
    topographyScanButton.clicked += TopographyScan;

    closeButton = root.Q<Button>("close");
    closeButton.clicked += Close;
  }

  private void Update() {
    if(SelectionManager.SelectedUnit == null) {
      unitHUD.visible = false;
    } else {
      unitLabel.text = SelectionManager.SelectedUnit.gameObject.name;
      unitHUD.visible = true;

      PowerStorage powerStorage = SelectionManager.SelectedUnit.transform.Find("Emergency Module Matrix").gameObject.GetComponent<PowerStorage>();

      powerProgressBar.highValue = powerStorage.chargeCapacity;
      powerProgressBar.value = powerStorage.charge;
      powerProgressBar.title = "Power: " + powerStorage.GetStatusString();

      if(Input.GetButtonDown("Cancel")) {
        SelectionManager.DeselectAll();
      }
    }

    if(SelectionManager.SelectedResource == null) {
      resourceLabel.text = "";
      resourceLabel.visible = false;
    } else {
      resourceLabel.text = SelectionManager.SelectedResource.gameObject.name;
      resourceLabel.visible = true;
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
    Debug.Log("Topography Scan");
  }

  private void Close() {
    SelectionManager.DeselectAll();
  }
}