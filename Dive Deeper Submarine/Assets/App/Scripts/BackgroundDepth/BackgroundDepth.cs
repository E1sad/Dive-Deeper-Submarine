using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Background{
  public class BackgroundDepth : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _diveLimmitY;

    //[Header("Links")]

    //Internal varibales

    #region My Methods
    private void MoveBackground() {
      if (transform.position.y >= _diveLimmitY) return;
      transform.position += new Vector3(0f, _moveSpeed * Time.deltaTime * LocalTime.DeltaTime,0f);
    }
    #endregion

    #region Unity's Methods
    private void Update() {
      MoveBackground();
    }
    #endregion
  }
}
