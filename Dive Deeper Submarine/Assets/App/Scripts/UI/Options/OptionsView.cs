using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Options{
  public class OptionsView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private OptionsController controller;

    #region My Methods
    public void OnBackButtonPressed(){ controller.OnBackButtonPressed();}
    #endregion

    #region Unity's Methods

    #endregion
  }
}
