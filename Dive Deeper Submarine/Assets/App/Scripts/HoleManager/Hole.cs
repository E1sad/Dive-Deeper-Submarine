using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.HoleManager{
  public class Hole : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _repairTime;

    //[Header("Links")]

    //Internal varibales
    private bool _interacted;
    [HideInInspector] public bool Interactable { get { return _interactable; } set { _interactable = value; } }
    private bool _interactable;
    [HideInInspector] public bool Interacted { get { return _interacted; } set { _interacted = value; } }
    private float _elapsed;
    [HideInInspector] public float Elapsed { get { return _elapsed; } set { _elapsed = value; } }

    #region My Methods
    private void Repair() {
      if (!_interactable) return;
      if (_interacted) {
        _elapsed += Time.deltaTime * LocalTime.DeltaTime;
        if (_repairTime <= _elapsed) { DestroyHoleEvent.Raise(this); }
      } else { _elapsed = 0f; }
    }
    private void InteractionButtonPressedEventHandler() {_interacted = true;}
    private void InteractionButtonReleasedEventHandler() { _interacted = false; }
    #endregion

    #region Unity's Methods
    private void Update() {
      Repair();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) { _interactable = true;}
    }
    private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player")) {_interactable = false;}
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
