using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ModuleMatrixUI : MonoBehaviour {
  private List<Module> modulesList;
  private VisualElement root;
  private VisualElement moduleMatrixUI;
  private ListView modulesListView;
  private VisualElement moduleView;
  private Label moduleTitle;
  private Label muduleDescription;
  private Button closeButton;

  private void Start() {
    modulesList = new List<Module>();
    root = GetComponent<UIDocument>().rootVisualElement;
    moduleMatrixUI = root.Q<VisualElement>("module_matrix_ui");
    modulesListView = root.Q<ListView>("modules_list");
    moduleView = root.Q<VisualElement>("module");
    moduleTitle = root.Q<Label>("module_title");
    muduleDescription = root.Q<Label>("mudule_description");
    closeButton = root.Q<Button>("close");
    closeButton.clicked += Hide;
  }

  public void Show() {
    Redraw();
    moduleMatrixUI.visible = true;
  }

  private void Hide() {
    moduleMatrixUI.visible = false;
  }

  private void Redraw() {
    Unit selectedUnit = SelectionManager.SelectedUnit;
    
    if(selectedUnit != null) {
      modulesList = selectedUnit.transform.Find("Emergency Module Matrix").GetComponent<ModuleMatrix>().modules;

      modulesListView.itemsSource = modulesList;

      modulesListView.makeItem = () => new Label();
      modulesListView.bindItem = (item, index) => (item as Label).text = modulesList[index].Title;

      modulesListView.selectionChanged += objects => {
        Module selectedModule = modulesListView.selectedItem as Module;
        moduleTitle.text = selectedModule.Title;
      };
    }
  }
}