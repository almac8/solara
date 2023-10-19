using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ModuleMatrixUI : MonoBehaviour {
  private ModuleMatrix moduleMatrix;
  private ListView modulesListView;
  private VisualElement rootVisualElement;

  private void OnEnable() {
    rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
    moduleMatrix = SelectionManager.SelectedUnit.GetModuleMatrix();

    PopulateListView();

    DropdownField modulesDropdown = rootVisualElement.Q<DropdownField>("modules_dropdown");
    string[] modulesDropdownOptions = ModuleMatrix.ModuleType.GetNames(typeof(ModuleMatrix.ModuleType));
    modulesDropdown.choices = new List<string>(modulesDropdownOptions);

    Button addModuleButton = rootVisualElement.Q<Button>("add_module");
    addModuleButton.clicked += () => {
      if(modulesDropdown.index != -1) {
        moduleMatrix.AddModule((ModuleMatrix.ModuleType)modulesDropdown.index);
        PopulateListView();
      }
    };

    Button closeButton = rootVisualElement.Q<Button>("close");
    closeButton.clicked += () => gameObject.SetActive(false);
  }

  private void RemoveModule(Module moduleToRemove) {
    moduleMatrix.RemoveModule(moduleToRemove);
    modulesListView.ClearSelection();
    PopulateListView();
    SetModuleDetails(null);
  }

  private void PopulateListView() {
    modulesListView = rootVisualElement.Q<ListView>("modules_list");
    List<Module> modulesList = moduleMatrix.modules;
    modulesListView.itemsSource = modulesList;

    modulesListView.makeItem = () => new Label();
    modulesListView.bindItem = (item, index) => (item as Label).text = modulesList[index].Title;
    modulesListView.selectionChanged += objects => {
      Module selectedModule = modulesListView.selectedItem as Module;
      SetModuleDetails(selectedModule);
    };
  }

  private void SetModuleDetails(Module selectedModule) {
    Label moduleTitle = rootVisualElement.Q<Label>("module_title");
    Label moduleDescription = rootVisualElement.Q<Label>("module_description");
    Button removeModuleButton = rootVisualElement.Q<Button>("remove");

    if(selectedModule == null) {
      moduleTitle.text = "None";
      moduleDescription.text = "No Module Selected";
      removeModuleButton.clicked += () => Debug.Log("No Module Selected");
    } else {
      moduleTitle.text = selectedModule.Title;
      moduleDescription.text = selectedModule.Description;
      removeModuleButton.clicked += () => RemoveModule(selectedModule);
    }
  }
}