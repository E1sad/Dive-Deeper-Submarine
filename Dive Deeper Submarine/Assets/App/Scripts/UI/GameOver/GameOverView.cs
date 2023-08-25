using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.GameOver{
  public class GameOverView : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GameOverController controller;
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text bestScoreText;
    //Internal varibales

    #region My Methods
    public void OnToMenuButtonPressed() { controller.ToMenuButtonPressed(); }
    public void OnRestartButtonPressed() { controller.RestartButtonPressed(); }
    public void SetScoresText(int score, int bestScore) {
      scoreText.text = System.Convert.ToString(score);
      bestScoreText.text = System.Convert.ToString(bestScore);
    }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
