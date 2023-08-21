using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.GameOver{
  public class GameOverView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GameOverController controller;

    //Internal varibales

    #region My Methods
    public void OnToMenuButtonPressed() { controller.ToMenuButtonPressed(); }
    public void OnRestartButtonPressed() { controller.RestartButtonPressed(); }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
