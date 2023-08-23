using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.EngineManager{
  public class EngineManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _maxTimeToBroke;
    [SerializeField] private float _minTimeToBroke;
    [SerializeField] private float _timeToRepair;

    //[Header("Links")]

    //Internal varibales
    private bool _interacted;
    private bool _interactable;
    private float _elapsed;
    System.Random random = new System.Random();
    private bool _isEngineWorking;
    private Coroutine _engineFailureRoutine;

    #region My Methods
    private void Repair() {
      if (_isEngineWorking) return; 
      if (!_interactable) return;
      if (_interacted) {
        _elapsed += Time.deltaTime * LocalTime.DeltaTime;
        if (_timeToRepair <= _elapsed) { EngineRepairedEvent.Raise(); ; _isEngineWorking = true;
          StartEngineFailureCoroutine();
          this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;}
      } else { _elapsed = 0f; }
    }
    private IEnumerator EngineFailure() {
      float elapsed = 0f;
      float failureTime = (float)(random.NextDouble() * (_maxTimeToBroke - _minTimeToBroke) + _minTimeToBroke);
      while (true) {
        elapsed += Time.deltaTime * LocalTime.DeltaTime;
        if (failureTime <= elapsed) {EngineBrokeEvent.Raise(); _isEngineWorking = false; 
          StopEngineFailureCoroutine();
          this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;}
        yield return null;}
    }
    private void StartEngineFailureCoroutine() {
      if (_engineFailureRoutine != null) StopCoroutine(_engineFailureRoutine);
      _engineFailureRoutine = StartCoroutine(EngineFailure());
    }
    private void StopEngineFailureCoroutine() {
      if (_engineFailureRoutine != null) StopCoroutine(_engineFailureRoutine); _engineFailureRoutine = null;
    }
      private void InteractionButtonPressedEventHandler() { _interacted = true; }
    private void InteractionButtonReleasedEventHandler() { _interacted = false; }
    #endregion

    #region Unity's Methods
    private void Start() {
      _elapsed = 0f; //Temporary until GameStateLogic implemented
      _interactable = false; //Temporary until GameStateLogic implemented
      _interacted = false; //Temporary until GameStateLogic implemented
      _isEngineWorking = true;
      StartEngineFailureCoroutine();
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
