using SOG.GameManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.EngineManager{
  public class EngineManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _maxTimeToBroke;
    [SerializeField] private float _minTimeToBroke;
    [SerializeField] private float _timeToRepair;

    [Header("Links")]
    [SerializeField] private RectTransform _interactButton;
    [SerializeField] private UnityEngine.UI.Image _filler;
    [SerializeField] private GameObject _bubble;
    [SerializeField] private ParticleSystem[] _bubbles;

    //Internal varibales
    private bool _interacted;
    private bool _interactable;
    private float _elapsed;
    System.Random random = new System.Random();
    private bool _isEngineWorking;
    private Coroutine _engineFailureRoutine;

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current, GameStateEnum previous) {
      switch (current) {
        case GameStateEnum.GAME_PLAY: GamePlayState(previous); break;
        case GameStateEnum.IDLE: IdleState(); break;
        case GameStateEnum.PAUSED: break;
      }
    }
    private void GamePlayState(GameStateEnum previous) {
      if (previous == GameStateEnum.IDLE) { 
        StartEngineFailureCoroutine(); _elapsed = 0f; _interactable = false;_interacted = false;
        _isEngineWorking = true; _filler.fillAmount = 0f; _interactButton.gameObject.SetActive(false);
        StartEngineFailureCoroutine();} 
    }
    private void IdleState() {
      StopEngineFailureCoroutine(); _elapsed = 0f; _interactable = false; _interacted = false;
      _isEngineWorking = true; _filler.fillAmount = 0f; _interactButton.gameObject.SetActive(false);
    }
    private void Repair() {
      if (_isEngineWorking) return; 
      if (!_interactable) return;
      _interactButton.gameObject.SetActive(true);
      if (_interacted) {
        _elapsed += Time.deltaTime * LocalTime.DeltaTime; _filler.fillAmount = _elapsed / _timeToRepair;
        if (_timeToRepair <= _elapsed) { EngineRepairedEvent.Raise(); ; _isEngineWorking = true;
          StartEngineFailureCoroutine(); _interactButton.gameObject.SetActive(false); StartBubbles();}
      } else { _elapsed = 0f; _filler.fillAmount = 0f; }
    }
    private IEnumerator EngineFailure() {
      float elapsed = 0f;
      float failureTime = (float)(random.NextDouble() * (_maxTimeToBroke - _minTimeToBroke) + _minTimeToBroke);
      while (true) {
        elapsed += Time.deltaTime * LocalTime.DeltaTime;
        if (failureTime <= elapsed) {EngineBrokeEvent.Raise(); _isEngineWorking = false; 
          StopEngineFailureCoroutine(); StopBubbles();}
        yield return null;}
    }
    private void StartEngineFailureCoroutine() {
      if (_engineFailureRoutine != null) StopCoroutine(_engineFailureRoutine);
      _engineFailureRoutine = StartCoroutine(EngineFailure());
    }
    private void StopEngineFailureCoroutine() {
      if (_engineFailureRoutine != null) StopCoroutine(_engineFailureRoutine); _engineFailureRoutine = null;
    }
    private void StopBubbles() {
      for (int i = 0; i < _bubbles.Length; i++) {_bubbles[i].Stop();}
    }
    private void StartBubbles() {
      for (int i = 0; i < _bubbles.Length; i++) { _bubbles[i].Play(); }
    }
    private void InteractionButtonPressedEventHandler() { _interacted = true; }
    private void InteractionButtonReleasedEventHandler() { _interacted = false; }
    private void EventGameStateChangedHandler(OnGameStateChangeEventArg eventArg) {
      OnGameStateChanged(eventArg.Current, eventArg.Previous);
    }
    #endregion

    #region Unity's Methods
    private void Update() {
      Repair();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { 
        _interactable = true; 
        if(!_isEngineWorking) _interactButton.gameObject.SetActive(true);}
    }
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { 
        _interactable = false; _interactButton.gameObject.SetActive(false);}
    }
    private void OnEnable() {
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed += InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased += InteractionButtonReleasedEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged += EventGameStateChangedHandler;
    }
    private void OnDisable() {
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed -= InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased -= InteractionButtonReleasedEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged -= EventGameStateChangedHandler;
    }
    #endregion
  }
}
