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

    //Internal varibales
    private int _holeNumber;
    private float _waterLevel;
    private bool _interactable;
    private bool _interacted;
    private bool _isElectricPowerOn;

    #region My Methods
    private void WaterLevel() {
      if (_waterLevel >= 5) { Debug.Log("GameOver");  return; }
      _waterLevel += _holeNumber * _waterLevelIncreaseMultiplier * Time.deltaTime * LocalTime.DeltaTime;
      _waterGameObject.transform.localScale =
        new Vector3(_waterGameObject.transform.localScale.x, _waterLevel, _waterGameObject.transform.localScale.z);
    }
    private void PumpWater() {
      if (!_isElectricPowerOn) return;
      if (!_interactable || _waterLevel <= 0) return;
      if (_interacted) {_waterLevel -= _quantityOfWaterPumpedAtOnce*Time.deltaTime * LocalTime.DeltaTime; }
    }
    private void IncreaseHoleNumber() {_holeNumber++;}
    private void DecreaseHoleNumber() { _holeNumber--;}
    private void InteractionButtonPressedEventHandler() { _interacted = true; }
    private void InteractionButtonReleasedEventHandler() { _interacted = false; }
    private void ElectricPowerOutEventHandler() { _isElectricPowerOn = false; }
    private void ElectricPowerRestoredEventHandler() { _isElectricPowerOn = true; }
    #endregion

    #region Unity's Methods
    private void Start() {
      _holeNumber = 0; //Temporary until GameStateLogic implemented
      _waterLevel = 0f; //Temporary until GameStateLogic implemented
      _interactable = false; //Temporary until GameStateLogic implemented
      _interacted = false; //Temporary until GameStateLogic implemented
      _isElectricPowerOn = true; //Temporary until GameStateLogic implemented
    }
    private void Update() {
      WaterLevel(); PumpWater();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { _interactable = true;  }
    }
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { _interactable = false; }
    }
    private void OnEnable() {
      HoleManager.PlusHoleEvent.EventPlusHole += IncreaseHoleNumber;
      HoleManager.MinusHoleEvent.EventMinusHole += DecreaseHoleNumber;
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed += InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased += InteractionButtonReleasedEventHandler;
      ElectricManager.ElectricPowerOutEvent.EventElectricPowerOut += ElectricPowerOutEventHandler;
      ElectricManager.ElectricPowerRestoredEvent.EventElectricPowerRestored += ElectricPowerRestoredEventHandler;
    }
    private void OnDisable() {
      HoleManager.PlusHoleEvent.EventPlusHole -= IncreaseHoleNumber;
      HoleManager.MinusHoleEvent.EventMinusHole -= DecreaseHoleNumber;
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed -= InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased -= InteractionButtonReleasedEventHandler;
      ElectricManager.ElectricPowerOutEvent.EventElectricPowerOut -= ElectricPowerOutEventHandler;
      EngineManager.EngineRepairedEvent.EventEngineRepaired -= ElectricPowerRestoredEventHandler;
      ElectricManager.ElectricPowerRestoredEvent.EventElectricPowerRestored -= ElectricPowerRestoredEventHandler;
    }
    #endregion
  }
}
