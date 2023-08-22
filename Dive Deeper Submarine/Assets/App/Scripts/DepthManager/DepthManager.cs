using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.DepthManager{
  public class DepthManager : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _diveSpeed;

    //[Header("Links")]

    //Internal varibales
    private float _depth;
    private int _previousDepth = 0;


    #region My Methods
    private void Dive() {
      _depth += _diveSpeed * Time.deltaTime * LocalTime.DeltaTime;
      int depthInteger = (int)_depth; 
      if (_previousDepth < depthInteger) {DepthEvent.Raise(depthInteger); _previousDepth = depthInteger;}
    }
    #endregion

    #region Unity's Methods
    private void FixedUpdate() {
      Dive();
    }
    #endregion
  }
}
