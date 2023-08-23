using SOG.EngineManager;
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
    private bool _isEngineWorking;

    #region My Methods
    private void MoveBackground() {
      if (!_isEngineWorking) return;
      if (transform.position.y >= _diveLimmitY) return;
      transform.position += new Vector3(0f, _moveSpeed * Time.deltaTime * LocalTime.DeltaTime,0f);
    }
    private void EngineRepairedEventHandler() { _isEngineWorking = true; }
    private void EngineBrokeEventHandler() { _isEngineWorking = false; }
    #endregion

    #region Unity's Methods
    private void Start() {
      _isEngineWorking = true;
    }
    private void Update() {
      MoveBackground();
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
