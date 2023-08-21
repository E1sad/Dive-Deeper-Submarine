using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Menu{
  public class MenuUIView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private MenuUIController controller;

    #region My Methods
    public void OnPlayButtonPressed(){controller.PlayButtonPressed();}
    public void OnOptionsButtonPressed(){controller.OptionsButtonPressed();}
    public void OnCreditsButtonPressed(){controller.CreditsButtonPressed();}
    public void OnExitButtonPressed() { controller.ExitButtonPressed(); }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
