using SOG.EngineManager;
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
    private bool _isEngineWorking;

    #region My Methods
    private void Dive() {
      if (!_isEngineWorking) return;
      _depth += _diveSpeed * Time.deltaTime * LocalTime.DeltaTime;
      int depthInteger = (int)_depth; 
      if (_previousDepth < depthInteger) {DepthEvent.Raise(depthInteger); _previousDepth = depthInteger;}
    }
    private void EngineRepairedEventHandler() { _isEngineWorking = true; }
    private void EngineBrokeEventHandler() { _isEngineWorking = false; }
    #endregion

    #region Unity's Methods
    private void Start() {
      _isEngineWorking = true;
    }
    private void FixedUpdate() {
      Dive();
    }
    private void OnEnable() {
      EngineRepairedEvent.EventEngineRepaired += EngineRepairedEventHandler;
      EngineBrokeEvent.EventEngineBroke += EngineBrokeEventHandler;
    }
    private void OnDisable() {
      EngineRepairedEvent.EventEngineRepaired -= EngineRepairedEventHandler;
      EngineBrokeEvent.EventEngineBroke -= EngineBrokeEventHandler;
    }
    #endregion
  }
}
