using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Pause{
  public class PauseView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private PauseController controller;

    //Internal varibales

    #region My Methods
    public void OnContinueButtonPressed(){controller.ContinueButtonPressed();}
    public void OnToMenuButtonPressed(){controller.ToMenuButtonPressed();}
    public void OnOptionsButtonPressed(){controller.OptionsButtonPressed();}
    public void OnRestartButtonPressed(){controller.RestartButtonPressed();}
    #endregion

    #region Unity's Methods

    #endregion
  }
}
