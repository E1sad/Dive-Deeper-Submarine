using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Options{
  public class OptionsView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private OptionsController controller;

    #region My Methods
    public void OnBackButtonPressed(){ controller.OnBackButtonPressed();}
    public void MusicToggleButtonpressed() {controller.MusicToggle();}
    public void SoundToggleButtonpressed() { controller.SoundToggle(); }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
