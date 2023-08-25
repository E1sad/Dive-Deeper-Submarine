using SOG.GameManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.HoleManager {
  public class HoleManager : MonoBehaviour{
    [Header("Hole Generetaion")]
    [SerializeField] private float _maxTimeToNewHole;
    [SerializeField] private float _minTimeToNewHole;
    [SerializeField] private int _difficultyMultiplier;
    [SerializeField] private float _maxXToNewHole;
    [SerializeField] private float _minXToNewHole;
    [SerializeField] private float _maxYToNewHole;
    [SerializeField] private float _minYToNewHole;
    [Header("Hole Instantiation")]
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject _hole;
    [SerializeField] private int _numberOfHoles;
    
    //[Header("Links")]

    //Internal varibales
    private int _depth;
    private System.Random _random = new System.Random();
    private List<GameObject> _allHoles;
    private List<GameObject> _usedHoles;
    private List<GameObject> _usableHoles;
    private Coroutine _generateHoleRoutine;

    #region My Methods
    private void OnGameStateChanged(GameStateEnum current, GameStateEnum previous) {
      switch (current) {
        case GameStateEnum.GAME_PLAY: GamePlayState(previous); break;
        case GameStateEnum.IDLE: IdleState(); break;
        case GameStateEnum.PAUSED: break;}
    }
    private void GamePlayState(GameStateEnum previous) {
      if (previous == GameStateEnum.IDLE) StartGenerateHole();
    }
    private void IdleState() {
      StopGenerateHole();
      int count = _usedHoles.Count;
      for (int i = 0; i < count; i++) {RemoveHole(_usedHoles[0].GetComponent<Hole>());}
    }
    private void InstantiateHoles() {
      for (int i = 0; i < _numberOfHoles; i++) {
        GameObject hole = Instantiate(_hole, parent);
        Hole holeScript = hole.GetComponent<Hole>();
        holeScript.Interactable = false; holeScript.Elapsed = 0f; holeScript.Interacted = false;
        holeScript.Instantiate();
        hole.SetActive(false);_allHoles.Add(hole);_usableHoles.Add(hole);}
    }
    private GameObject GetHole() {
      if (_usableHoles.Count <= 0) return null;
      GameObject hole = _usableHoles[0]; _usableHoles.RemoveAt(0);_usedHoles.Add(hole);return hole;
    }
    private void RemoveHole(Hole hole) {
      hole.Interactable = false; hole.Elapsed = 0f; hole.Interacted = false;
      hole.gameObject.SetActive(false) ; _usableHoles.Add(hole.gameObject); 
      _usedHoles.RemoveAll(Hole => Hole == hole.gameObject); MinusHoleEvent.Raise();
    }
    private Vector3 getPositionForHole() {
      float x = 0f; int k = 0;
      float y = (float)(_random.NextDouble() * (_maxYToNewHole - _minYToNewHole) + _minYToNewHole);
      bool isPerfectPosition =false , isColliding = false;
      while (k < 100) {
        x = (float)(_random.NextDouble() * (_maxXToNewHole - _minXToNewHole) + _minXToNewHole);
        if (x < 1.5 && x > -1.5) continue;
        for (int i = 0; i < _usedHoles.Count; i++) {
          if (_usedHoles[i].transform.position.x - x < 2 && _usedHoles[i].transform.position.x - x > -2) {
            isColliding = true; break;}}
        if (!isColliding) { isPerfectPosition = true; break;} else { isColliding = false; k++; }}
      if (isPerfectPosition) return new Vector3(x, y, 0f);
      else {return new Vector3(9, y, 0f);}
    }
    private IEnumerator GenerateHole() {
      float elapsed = 0f;
      float time = (float)(_random.NextDouble()*(_maxTimeToNewHole - _minTimeToNewHole) +_minTimeToNewHole);
      while (true) {
        if(_usableHoles.Count == 0) { yield return null; }
        if (time <= elapsed) {
          Vector3 positionOfHole = getPositionForHole(); GameObject hole = GetHole();
          if (hole != null) {
            hole.transform.position = positionOfHole; hole.SetActive(true);
            hole.GetComponent<Hole>().PlacedInWorld(); PlusHoleEvent.Raise();}
          time = (float)(_random.NextDouble() * (_maxTimeToNewHole - _minTimeToNewHole) + _minTimeToNewHole);
          elapsed = 0f;}
        elapsed += Time.deltaTime * LocalTime.DeltaTime;
        yield return null;}
    }
    private void StartGenerateHole() {
      if (_generateHoleRoutine != null) StopCoroutine(_generateHoleRoutine);
      _generateHoleRoutine = StartCoroutine(GenerateHole());
    }
    private void StopGenerateHole() {
      if (_generateHoleRoutine != null) StopCoroutine(_generateHoleRoutine); _generateHoleRoutine = null;
    }
    private void DepthEventHandler(int depth) { 
      _depth = depth;
      if ((_maxTimeToNewHole - _depth / _difficultyMultiplier) >= _minTimeToNewHole)
        _maxTimeToNewHole -= _depth / _difficultyMultiplier;
    }
    private void DestroyHoleEventHandler(Hole hole) {RemoveHole(hole);}
    private void EventGameStateChangedHandler(OnGameStateChangeEventArg eventArg) {
      OnGameStateChanged(eventArg.Current, eventArg.Previous);
    }
    #endregion

    #region Unity's Methods
    private void Awake() {
      _allHoles = new List<GameObject>();
      _usedHoles = new List<GameObject>();
      _usableHoles = new List<GameObject>();
    }
    private void Start() {

      InstantiateHoles();
    }
    private void OnEnable() {
      DepthManager.DepthEvent.EventDepth += DepthEventHandler;
      DestroyHoleEvent.EventDestroyHole += DestroyHoleEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged += EventGameStateChangedHandler;  
    }
    private void OnDisable() {
      DepthManager.DepthEvent.EventDepth -= DepthEventHandler;
      DestroyHoleEvent.EventDestroyHole -= DestroyHoleEventHandler;
      OnGameStateChangedEvent.EventGameStateChanged -= EventGameStateChangedHandler;
    }
    #endregion
  }
}
