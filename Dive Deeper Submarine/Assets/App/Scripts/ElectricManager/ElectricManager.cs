using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.ElectricManager{
  public class ElectricManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _maxTimeToBroke;
    [SerializeField] private float _minTimeToBroke;

    //[Header("Links")]

    //Internal varibales
    private bool _interacted;
    private bool _interactable;
    System.Random random = new System.Random();
    private bool _isElectricOn;
    private Coroutine _engineFailureRoutine;

    #region My Methods
    private void Repair() {
      if (_isElectricOn) return;
      if (!_interactable) return;
      if (_interacted) {
          ElectricPowerRestoredEvent.Raise(); ; _isElectricOn = true;
          StartElectricPowerFailureCoroutine();
          this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;}
    }
    private IEnumerator ElectricPowerFailure() {
      float elapsed = 0f;
      float failureTime = (float)(random.NextDouble() * (_maxTimeToBroke - _minTimeToBroke) + _minTimeToBroke);
      while (true) {
        elapsed += Time.deltaTime * LocalTime.DeltaTime;
        if (failureTime <= elapsed) {
          ElectricPowerOutEvent.Raise(); _isElectricOn = false;
          StopElectricPowerFailureCoroutine();
          this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
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
    #endregion

    #region Unity's Methods
    private void Start() {
      _interactable = false; //Temporary until GameStateLogic implemented
      _interacted = false; //Temporary until GameStateLogic implemented
      _isElectricOn = true;
      StartElectricPowerFailureCoroutine();
    }
    private void Update() {
      Repair();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { _interactable = true; }
    }
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { _interactable = false; }
    }
    private void OnEnable() {
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed += InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased += InteractionButtonReleasedEventHandler;
    }
    private void OnDisable() {
      Player.InteractionButtonPressedEvent.EventInteractionButtonPressed -= InteractionButtonPressedEventHandler;
      Player.InteractionButtonRleasedEvent.EventInteractionButtonRleased -= InteractionButtonReleasedEventHandler;
    }
    #endregion
  }
}
