using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG
{
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
    private void InstantiateHoles() {
      for (int i = 0; i < _numberOfHoles; i++) {
        GameObject hole = Instantiate(_hole, parent);
        hole.SetActive(false);_allHoles.Add(hole);_usableHoles.Add(hole);}
    }
    private GameObject GetHole() {
      if (_usableHoles.Count <= 0) return null;
      GameObject hole = _usableHoles[0]; _usableHoles.RemoveAt(0);_usedHoles.Add(hole);return hole;
    }
    private void RemoveHole(GameObject hole) {
      hole.SetActive(false) ; _usableHoles.Add(hole); 
      _usedHoles.RemoveAll(Hole => Hole == hole);
    }
    private Vector3 getPositionForHole() {
      float x = 0f; int k = 0;
      float y = (float)(_random.NextDouble() * (_maxYToNewHole - _minYToNewHole) + _minYToNewHole);
      bool isPerfectPosition =false , isColliding = false;
      while (k < 100) {
        x = (float)(_random.NextDouble() * (_maxXToNewHole - _minXToNewHole) + _minXToNewHole);
        for (int i = 0; i < _usedHoles.Count; i++) {
          Debug.Log($"{i}: {x}");
          if (_usedHoles[i].transform.position.x - x < 2 && _usedHoles[i].transform.position.x - x > -2) {
            isColliding = true; break;}}
        if (!isColliding) { isPerfectPosition = true; break;} else { isColliding = false; k++; }}
      Debug.Log("--------------------------------");
      if (isPerfectPosition) return new Vector3(x, y, 0f);
      else {return new Vector3(9, y, 0f);}
    }
    private IEnumerator GenerateHole() {
      float elapsed = 0f;
      float time = (float)(_random.NextDouble()*(_maxTimeToNewHole - _minTimeToNewHole) +_minTimeToNewHole);
      while (true) {
        if (time <= elapsed) {
          Vector3 positionOfHole = getPositionForHole(); GameObject hole = GetHole();
          if (hole != null) {
            hole.transform.position = positionOfHole; hole.SetActive(true);}
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
    #endregion

    #region Unity's Methods
    private void Start() {
      _allHoles = new List<GameObject>();
      _usedHoles = new List<GameObject>();
      _usableHoles = new List<GameObject>();
      InstantiateHoles();
      StartGenerateHole();
    }
    private void OnEnable() {
      DepthManager.DepthEvent.EventDepth += DepthEventHandler;
    }
    private void OnDisable() {
      DepthManager.DepthEvent.EventDepth -= DepthEventHandler;
    }
    #endregion
  }
}