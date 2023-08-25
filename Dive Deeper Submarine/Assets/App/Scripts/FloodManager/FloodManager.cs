using SOG.GameManger;
using SOG.UI.GameOver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.FloodManager{
  public class FloodManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _waterLevelIncreaseMultiplier;
    [SerializeField] private float _quantityOfWaterPumpedAtOnce;

    [Header("Links")]
    [SerializeField] private GameObject _waterGameObject;
    [SerializeField] private RectTransform _interactButton;
    [SerializeField] private UnityEngine.UI.Image _filler;

    //Internal varibales
    private int _holeNumber;
    private float _waterLevel;
    private bool _interactable;
    private bool _interacted;
    private bool _isElectricPowerOn;

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
        _holeNumber = 0; _waterLevel = 0f; _interactable = false; _interacted = false; _isElectricPowerOn = true;
        _interactButton.gameObject.SetActive(false); _filler.gameObject.SetActive(false);}
    }
    private void IdleState() {
      _holeNumber = 0; _waterLevel = 0f; _interactable = false; _interacted = false; _isElectricPowerOn = true;
      _interactButton.gameObject.SetActive(false); _filler.gameObject.SetActive(false);
    }
    private void WaterLevel() {
      if (_waterLevel >= 4) { GameOverEvent.Raise(); _waterLevel = 0;  return; }
      _waterLevel += _holeNumber * _waterLevelIncreaseMultiplier * Time.deltaTime * LocalTime.DeltaTime;
      _waterGameObject.transform.localScale =
        new Vector3(_waterGameObject.transform.localScale.x, _waterLevel, _waterGameObject.transform.localScale.z);
    }
    private void PumpWater() {
      if (!_isElectricPowerOn) return;
      if (!_interactable || _waterLevel <= 0) return;
      if (_interacted) {
        _waterLevel -= _quantityOfWaterPumpedAtOnce*Time.deltaTime * LocalTime.DeltaTime;
        _filler.gameObject.SetActive(true); } 
      else { _filler.gameObject.SetActive(false); }
    }
    private void IncreaseHoleNumber() {_holeNumber++;}
    private void DecreaseHoleNumber() { _holeNumber--;}
    private void InteractionButtonPressedEventHandler() { _interacted = true; }
    private void InteractionButtonReleasedEventHandler() { _interacted = false; }
    private void ElectricPowerOutEventHandler() { _isElectricPowerOn = false; }
    private void ElectricPowerRestoredEventHandler() { _isElectricPowerOn = true; }
    private void EventGameStateChangedHandler(OnGameStateChangeEventArg eventArg) {
      OnGameStateChanged(eventArg.Current, eventArg.Previous);
    }
    #endregion

    #region Unity's Methods
    private void Update() {
      WaterLevel(); PumpWater();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { 
        _interactable = true; _interactButton.gameObject.SetActive(true); _filler.gameObject.SetActive(false);}
    }
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { 
        _interactable = false; _interactButton.gameObject.SetActive(false); _filler.gameObject.SetActive(false);}
    }
    private void OnEnable() {
      HoleManager.PlusHoleEvent.EventPlusHole += IncreaseHoleNumber;
      HoleManager.MinusHoleEvent.EventMinusHole += DecreaseHoleNumber;
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed += InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased += InteractionButtonReleasedEventHandler;
      ElectricManager.ElectricPowerOutEvent.EventElectricPowerOut += ElectricPowerOutEventHandler;
      ElectricManager.ElectricPowerRestoredEvent.EventElectricPowerRestored += ElectricPowerRestoredEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged += EventGameStateChangedHandler;

    }
    private void OnDisable() {
      HoleManager.PlusHoleEvent.EventPlusHole -= IncreaseHoleNumber;
      HoleManager.MinusHoleEvent.EventMinusHole -= DecreaseHoleNumber;
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed -= InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased -= InteractionButtonReleasedEventHandler;
      ElectricManager.ElectricPowerOutEvent.EventElectricPowerOut -= ElectricPowerOutEventHandler;
      EngineManager.EngineRepairedEvent.EventEngineRepaired -= ElectricPowerRestoredEventHandler;
      ElectricManager.ElectricPowerRestoredEvent.EventElectricPowerRestored -= ElectricPowerRestoredEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged -= EventGameStateChangedHandler;
    }
    #endregion
  }
}
