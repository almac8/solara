using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ground : MonoBehaviour {
  [SerializeField] private TMP_Text selectedUnitText;

  private void OnMouseDown() {
    selectedUnitText.text = "";
  }
}