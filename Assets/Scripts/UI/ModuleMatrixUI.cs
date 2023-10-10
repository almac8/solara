using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ModuleMatrixUI : MonoBehaviour {
  private void OnEnable() {
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
    ListView modulesListView = rootVisualElement.Q<ListView>("modules_list");

    List<Module> modulesList = SelectionManager.SelectedUnit.GetModuleMatrix().modules;
    modulesListView.itemsSource = modulesList;

    modulesListView.makeItem = () => new Label();
    modulesListView.bindItem = (item, index) => (item as Label).text = modulesList[index].Title;
    modulesListView.selectionChanged += objects => {
      Module selectedModule = modulesListView.selectedItem as Module;

      Label moduleTitle = rootVisualElement.Q<Label>("module_title");
      moduleTitle.text = selectedModule.Title;

      Label moduleDescription = rootVisualElement.Q<Label>("module_description");
      moduleDescription.text = selectedModule.Description;
    };

    Button closeButton = rootVisualElement.Q<Button>("close");
    closeButton.clicked += () => gameObject.SetActive(false);
  }
}