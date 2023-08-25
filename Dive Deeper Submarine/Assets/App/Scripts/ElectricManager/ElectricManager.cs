using SOG.GameManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.ElectricManager{
  public class ElectricManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _maxTimeToBroke;
    [SerializeField] private float _minTimeToBroke;

    [Header("Links")]
    [SerializeField] private Sprite _ElectricOn;
    [SerializeField] private Sprite _ElectricOff;
    [SerializeField] private RectTransform _interactButton;
    [SerializeField] private UnityEngine.UI.Image _filler;

    //Internal varibales
    private bool _interacted;
    private bool _interactable;
    System.Random random = new System.Random();
    private bool _isElectricOn;
    private Coroutine _engineFailureRoutine;

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current, GameStateEnum previous) {
      switch (current) {
        case GameStateEnum.GAME_PLAY: GamePlayState(previous); break;
        case GameStateEnum.IDLE: IdleState(); break;
        case GameStateEnum.PAUSED: break;}
    }
    private void GamePlayState(GameStateEnum previous) {
      if (previous == GameStateEnum.IDLE) { 
        StartElectricPowerFailureCoroutine(); _interactButton.gameObject.SetActive(false); 
        _filler.gameObject.SetActive(false); _isElectricOn = true; _interactable = false; _interacted = false;}
    }
    private void IdleState() {
      StopElectricPowerFailureCoroutine(); _interactButton.gameObject.SetActive(false);
      _filler.gameObject.SetActive(false); _isElectricOn = true; _interactable = false; _interacted = false;
    }
    private void Repair() {
      if (_isElectricOn) return;
      if (!_interactable) return;
      _interactButton.gameObject.SetActive(true);
      if (_interacted) {
        _interactButton.gameObject.SetActive(false); ElectricPowerRestoredEvent.Raise(); ; _isElectricOn = true; 
        StartElectricPowerFailureCoroutine(); this.gameObject.GetComponent<SpriteRenderer>().sprite=_ElectricOn;} 
      else { _filler.gameObject.SetActive(false); }
    }
    private IEnumerator ElectricPowerFailure() {
      float elapsed = 0f;
      float failureTime = (float)(random.NextDouble() * (_maxTimeToBroke - _minTimeToBroke) + _minTimeToBroke);
      while (true) {
        elapsed += Time.deltaTime * LocalTime.DeltaTime;
        if (failureTime <= elapsed) {
          ElectricPowerOutEvent.Raise(); _isElectricOn = false;
          StopElectricPowerFailureCoroutine();
          this.gameObject.GetComponent<SpriteRenderer>().sprite = _ElectricOff;
        }
        yield return null;
      }
    }
    private void StartElectricPowerFailureCoroutine() {
      if (_engineFailureRoutine != null) StopCoroutine(_engineFailureRoutine);
      _engineFailureRoutine = StartCoroutine(ElectricPowerFailure());
    }
    private void StopElectricPowerFailureCoroutine() {
      if (_engineFailureRoutine != null) StopCoroutine(_engineFailureRoutine); _engineFailureRoutine = null;
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
        if (!_isElectricOn){ _interactButton.gameObject.SetActive(true); _filler.gameObject.SetActive(false);}}
    }
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { 
        _interactable = false; _interactButton.gameObject.SetActive(false); _filler.gameObject.SetActive(false);}
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
