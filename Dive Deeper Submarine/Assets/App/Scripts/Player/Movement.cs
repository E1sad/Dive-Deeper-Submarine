using SOG.GameManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player{
  public class Movement : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _gravityMultiplier;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private Transform _captainSpritesTransform;
    [SerializeField] private Animator _captainAnimator;

    //Internal varibales

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current, GameStateEnum previous) {
      switch (current) {
        case GameStateEnum.GAME_PLAY: GamePlayState(previous); break;
        case GameStateEnum.IDLE: IdleState(); break;
        case GameStateEnum.PAUSED: break;}
    }
    private void GamePlayState(GameStateEnum previous) {
      if (previous == GameStateEnum.IDLE) {}
    }
    private void IdleState() {
      transform.position = new Vector3(0f,0f,0f);
    }
    private void PlayerMovement() {
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      _playerRb.velocity = new Vector2(horizontalInput * _speedMultiplier, _playerRb.velocity.y)
        *Time.deltaTime*LocalTime.DeltaTime;
      if (horizontalInput != 0) {
        _captainSpritesTransform.localScale = new Vector3(Mathf.Round(horizontalInput), 1f, 1f);
        _captainAnimator.SetBool("IsRunning", true);
      }else {_captainAnimator.SetBool("IsRunning", false);}
    }
    private void EventGameStateChangedHandler(OnGameStateChangeEventArg eventArg) {
      OnGameStateChanged(eventArg.Current, eventArg.Previous);
    }
    #endregion 

    #region Unity's Methods
    private void Update() {
      PlayerMovement();
    }
    private void OnEnable() {
      OnGameStateChangedEvent.EventGameStateChanged += EventGameStateChangedHandler;
    }
    private void OnDisable() {
      OnGameStateChangedEvent.EventGameStateChanged -= EventGameStateChangedHandler;
    }
    #endregion
  }
}
