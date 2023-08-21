using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Credits{
  public class CreditsView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private CreditsController controller;

    #region My Methods
    public void OnBackButtonPressed() { controller.OnBackButtonPressed(); }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
