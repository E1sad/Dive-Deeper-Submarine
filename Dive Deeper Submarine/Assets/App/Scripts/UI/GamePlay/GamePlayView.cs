using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.GamePlay{
  public class GamePlayView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GamePlayController controller;
    [SerializeField] private TMPro.TMP_Text text;

    //Internal varibales

    #region My Methods
    public void SetScoreText(int integer) {
      text.text = Convert.ToString(integer);
    }
    #endregion

    #region Unity's Methods


    #endregion
  }
}
