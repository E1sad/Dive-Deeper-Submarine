using SOG.UI.Pause;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.GameOver{
  public class GameOverController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GameOverView view;

    //Internal varibales

    #region My Methods
    public void ToMenuButtonPressed() { view.gameObject.SetActive(false); ToMenuEvent.Raise(); }
    public void RestartButtonPressed() { view.gameObject.SetActive(false); RestartEvent.Raise(); }
    private void onGameOverEventHandler(){view.gameObject.SetActive(true);}

    #endregion

    #region Unity's Methods
    private void OnEnable(){
      GameOverEvent.OnGameOverEvent += onGameOverEventHandler;
    }
    private void OnDisable(){
      GameOverEvent.OnGameOverEvent -= onGameOverEventHandler;
    }
    #endregion
  }
}
