using SOG.UI.GameOver;
using SOG.UI.Menu;
using SOG.UI.Pause;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.GameManger {
  public class GameManager : MonoBehaviour {
    [Header("Variables")]
    [SerializeField] private GameStateEnum _starterState;

    [Header("Links")]
    [SerializeField] private AudioClip _idleAudioClip;
    [SerializeField] private AudioClip _gamePlayAudioClip;
    [SerializeField] private AudioClip _gameOverAudioClip;

    //Internal varibales
    private GameStateEnum _previousGameState, _currentGameState;

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current){
      OnGameStateChangedEvent.Raise(new OnGameStateChangeEventArg(current, _previousGameState));
      switch (current){
        case GameStateEnum.GAME_PLAY: LocalTime.DeltaTime = 1f;
          MusicAndSoundManager.Instance.PlayBackgroundMusic(_gamePlayAudioClip); break;
        case GameStateEnum.IDLE: LocalTime.DeltaTime = 0f;
          MusicAndSoundManager.Instance.PlayBackgroundMusic(_idleAudioClip); break;
        case GameStateEnum.PAUSED: LocalTime.DeltaTime = 0f; PauseEvent.Raise();
          MusicAndSoundManager.Instance.PlayBackgroundMusic(_idleAudioClip); break;}
    }
    private void GameStateChange() {
      if (_currentGameState == GameStateEnum.GAME_PLAY || _currentGameState == GameStateEnum.PAUSED) {
        if (Input.GetKeyDown(KeyCode.Escape) && _currentGameState == GameStateEnum.GAME_PLAY) {
          _currentGameState = GameStateEnum.PAUSED;
          OnGameStateChanged(GameStateEnum.PAUSED); _previousGameState = GameStateEnum.PAUSED;}
        else if (Input.GetKeyDown(KeyCode.Escape) && _currentGameState == GameStateEnum.PAUSED) {
          _currentGameState = GameStateEnum.GAME_PLAY; ContinueEvent.Raise();
          OnGameStateChanged(GameStateEnum.GAME_PLAY); _previousGameState = GameStateEnum.GAME_PLAY;}}
    }
    private void OnPlayButtonPressed() {
      _currentGameState = GameStateEnum.GAME_PLAY;
      OnGameStateChanged(GameStateEnum.GAME_PLAY); _previousGameState = GameStateEnum.GAME_PLAY;
    }
    private void OnPauseButtonPressed() {
      _currentGameState = GameStateEnum.PAUSED;
      OnGameStateChanged(GameStateEnum.PAUSED); _previousGameState = GameStateEnum.PAUSED;
    }
    private void OnToMenuButtonPressed() {
      _currentGameState = GameStateEnum.IDLE;
      OnGameStateChanged(GameStateEnum.IDLE); _previousGameState = GameStateEnum.IDLE;
    }
    private void OnContinueButtonPressed() {
      _currentGameState = GameStateEnum.GAME_PLAY;
      OnGameStateChanged(GameStateEnum.GAME_PLAY); _previousGameState = GameStateEnum.GAME_PLAY;
    }
    private void OnRestartButtonPressed() {
      _currentGameState = GameStateEnum.IDLE;
      OnGameStateChanged(GameStateEnum.IDLE); _previousGameState = GameStateEnum.IDLE;
      _currentGameState = GameStateEnum.GAME_PLAY;
      OnGameStateChanged(GameStateEnum.GAME_PLAY); _previousGameState = GameStateEnum.GAME_PLAY;
    }
    private void onGameOverEventHandler() { 
      LocalTime.DeltaTime = 0f; MusicAndSoundManager.Instance.PlaySoundEffects(_gameOverAudioClip);
      MusicAndSoundManager.Instance.StopAllSounds();
    }
    #endregion

    #region Unity's Methods
    private void Start() {
      _previousGameState = _starterState;
      _currentGameState = _starterState; OnGameStateChanged(_currentGameState);
    }
    private void Update() {
      GameStateChange();
    }
    private void OnEnable() {
      PlayButtonEvent.OnPlayButtonPressedEvent += OnPlayButtonPressed;
      UI.Pause.ToMenuEvent.OnToMenuEvent += OnToMenuButtonPressed;
      UI.Pause.ContinueEvent.OnContinueEvent += OnContinueButtonPressed;
      UI.Pause.RestartEvent.OnRestartEvent += OnRestartButtonPressed;
      GameOverEvent.OnGameOverEvent += onGameOverEventHandler;
    }
    private void OnDisable() {
      PlayButtonEvent.OnPlayButtonPressedEvent -= OnPlayButtonPressed;
      UI.Pause.ToMenuEvent.OnToMenuEvent -= OnToMenuButtonPressed;
      UI.Pause.ContinueEvent.OnContinueEvent -= OnContinueButtonPressed;
      UI.Pause.RestartEvent.OnRestartEvent -= OnRestartButtonPressed;
      GameOverEvent.OnGameOverEvent -= onGameOverEventHandler;

    }
    #endregion
  }
}