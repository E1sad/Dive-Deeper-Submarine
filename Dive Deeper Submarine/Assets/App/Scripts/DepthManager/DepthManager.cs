using SOG.EngineManager;
using SOG.GameManger;
using SOG.SaveManager;
using SOG.UI.GameOver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.DepthManager{
  public class DepthManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _diveSpeed;

    //[Header("Links")]

    //Internal varibales
    private float _depth;
    private int _bestDepth;
    private int _previousDepth = 0;
    private bool _isEngineWorking;

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current, GameStateEnum previous) {
      switch (current) {
        case GameStateEnum.GAME_PLAY: GamePlayState(previous); break;
        case GameStateEnum.IDLE: IdleState(); break;
        case GameStateEnum.PAUSED: break;}
    }
    private void GamePlayState(GameStateEnum previous) {
      if (previous == GameStateEnum.IDLE) { _depth = 0f; DepthEvent.Raise((int)_depth); _isEngineWorking = true; }
    }
    private void IdleState() {
      _depth = 0f; DepthEvent.Raise((int)_depth); _previousDepth = 0; _isEngineWorking = true;
    }
    private void Dive() {
      if (!_isEngineWorking) return;
      _depth += _diveSpeed * Time.deltaTime * LocalTime.DeltaTime;
      int depthInteger = (int)_depth; 
      if (_previousDepth < depthInteger) {DepthEvent.Raise(depthInteger); _previousDepth = depthInteger;}
    }
    private void EngineRepairedEventHandler() { _isEngineWorking = true; }
    private void EngineBrokeEventHandler() { _isEngineWorking = false; }
    private void onGameOverEventHandler() {
      if(_bestDepth <= _previousDepth) { 
        _bestDepth = _previousDepth; SaveBestScore.Raise(_bestDepth); }
      GameOverDepthScoreEvent.Raise(_previousDepth, _bestDepth);
    }
    private void EventGameStateChangedHandler(OnGameStateChangeEventArg eventArg) {
      OnGameStateChanged(eventArg.Current, eventArg.Previous);
    }
    private void SendDataToObjectsEventHandler(int bestScore, bool isMusicon, bool isSoundOn) {
      _bestDepth = bestScore; Debug.Log(_bestDepth);
    }
    #endregion

    #region Unity's Methods
    private void FixedUpdate() {
      Dive();
    }
    private void OnEnable() {
      EngineRepairedEvent.EventEngineRepaired += EngineRepairedEventHandler;
      EngineBrokeEvent.EventEngineBroke += EngineBrokeEventHandler;
      GameOverEvent.OnGameOverEvent += onGameOverEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged += EventGameStateChangedHandler;
      SendDataToObjects.SendDataToObjectsEvent += SendDataToObjectsEventHandler;
    }
    private void OnDisable() {
      EngineRepairedEvent.EventEngineRepaired -= EngineRepairedEventHandler;
      EngineBrokeEvent.EventEngineBroke -= EngineBrokeEventHandler;
      GameOverEvent.OnGameOverEvent -= onGameOverEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged -= EventGameStateChangedHandler;
      SendDataToObjects.SendDataToObjectsEvent -= SendDataToObjectsEventHandler;
    }
    #endregion
  }
}
