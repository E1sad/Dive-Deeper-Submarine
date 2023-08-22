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

    //Internal varibales

    #region My Methods
    private void PlayerMovement() {
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      _playerRb.velocity = new Vector2(horizontalInput * _speedMultiplier, _playerRb.velocity.y)
        *Time.deltaTime*LocalTime.DeltaTime;
    }
    #endregion 

    #region Unity's Methods
    private void Update() {
      PlayerMovement();
    }
    #endregion
  }
}
