using SOG.UI.GameOver;
using SOG.UI.Menu;
using SOG.UI.Pause;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.GamePlay{
  public class GamePlayController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GamePlayView view;

    //Internal varibales

    #region My Methods
    private void onGamePlayEventHandler(){
      view.gameObject.SetActive(true);
    }
    private void onPauseEventHandler(){
      view.gameObject.SetActive(false);
    }
    private void DepthEventHandler(int depth) {
      view.SetScoreText(depth);
    }
    #endregion

    #region Unity's Methods
    private void OnEnable()
    {
      ContinueEvent.OnContinueEvent += onGamePlayEventHandler;
      GamePlayEvent.OnGamePlayEvent += onGamePlayEventHandler;
      PauseEvent.OnPauseEvent += onPauseEventHandler;
      PlayButtonEvent.OnPlayButtonPressedEvent += onGamePlayEventHandler;
      GameOverEvent.OnGameOverEvent += onPauseEventHandler;
      RestartEvent.OnRestartEvent += onGamePlayEventHandler;
      DepthManager.DepthEvent.EventDepth += DepthEventHandler;
    }
    private void OnDisable()
    {
      ContinueEvent.OnContinueEvent -= onGamePlayEventHandler;
      GamePlayEvent.OnGamePlayEvent -= onGamePlayEventHandler;
      PauseEvent.OnPauseEvent -= onPauseEventHandler;
      PlayButtonEvent.OnPlayButtonPressedEvent -= onGamePlayEventHandler;
      GameOverEvent.OnGameOverEvent -= onPauseEventHandler;
      RestartEvent.OnRestartEvent -= onGamePlayEventHandler;
      DepthManager.DepthEvent.EventDepth -= DepthEventHandler;
    }
    #endregion
  }
}
