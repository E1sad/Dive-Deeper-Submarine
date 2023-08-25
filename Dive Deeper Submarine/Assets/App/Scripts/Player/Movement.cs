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
    private void PlayerMovement() {
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      _playerRb.velocity = new Vector2(horizontalInput * _speedMultiplier, _playerRb.velocity.y)
        *Time.deltaTime*LocalTime.DeltaTime;
      if (horizontalInput != 0) {
        _captainSpritesTransform.localScale = new Vector3(Mathf.Round(horizontalInput), 1f, 1f);
        _captainAnimator.SetBool("IsRunning", true);
      }else {_captainAnimator.SetBool("IsRunning", false);}
    }
    #endregion 

    #region Unity's Methods
    private void Update() {
      PlayerMovement();
    }
    #endregion
  }
}
