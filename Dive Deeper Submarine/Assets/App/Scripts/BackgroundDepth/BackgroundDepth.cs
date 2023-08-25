using SOG.EngineManager;
using SOG.GameManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Background{
  public class BackgroundDepth : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _diveLimmitY;

    //[Header("Links")]

    //Internal varibales
    private bool _isEngineWorking;

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current, GameStateEnum previous) {
      switch (current) {
        case GameStateEnum.GAME_PLAY: GamePlayState(previous); break;
        case GameStateEnum.IDLE: IdleState(); break;
        case GameStateEnum.PAUSED: break;}
    }
    private void GamePlayState(GameStateEnum previous) {
      if (previous == GameStateEnum.IDLE) { 
        _isEngineWorking = true; transform.position = new Vector3(0f, 0f, 0f); }
    }
    private void IdleState() {
      _isEngineWorking = true; transform.position = new Vector3(0f, 0f, 0f);
    }
    private void MoveBackground() {
      if (!_isEngineWorking) return;
      if (transform.position.y >= _diveLimmitY) return;
      transform.position += new Vector3(0f, _moveSpeed * Time.deltaTime * LocalTime.DeltaTime,0f);
    }
    private void EngineRepairedEventHandler() { _isEngineWorking = true; }
    private void EngineBrokeEventHandler() { _isEngineWorking = false; }
    private void EventGameStateChangedHandler(OnGameStateChangeEventArg eventArg) {
      OnGameStateChanged(eventArg.Current, eventArg.Previous);
    }
    #endregion

    #region Unity's Methods
    private void Update() {
      MoveBackground();
    }
    private void OnEnable() {
      EngineRepairedEvent.EventEngineRepaired += EngineRepairedEventHandler;
      EngineBrokeEvent.EventEngineBroke += EngineBrokeEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged += EventGameStateChangedHandler;
    }
    private void OnDisable() {
      EngineRepairedEvent.EventEngineRepaired -= EngineRepairedEventHandler;
      EngineBrokeEvent.EventEngineBroke -= EngineBrokeEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged -= EventGameStateChangedHandler;
    }
    #endregion
  }
}
