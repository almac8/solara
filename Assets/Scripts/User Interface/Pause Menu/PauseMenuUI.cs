using UnityEngine;

public class PauseMenuUI : MonoBehaviour {
  [SerializeField] private GameObject pauseMenu;

  private GameObject pauseMenuInstance;
  public bool IsActive { get; private set; }

  private void Awake() {
    Transform uiCanvasTransform = transform.GetChild(0);
    
    pauseMenuInstance = Instantiate(pauseMenu, uiCanvasTransform);
    pauseMenuInstance.SetActive(false);

    IsActive = false;
  }

  public void Toggle() {
    IsActive = !IsActive;

    pauseMenuInstance.SetActive(IsActive);
  }
}