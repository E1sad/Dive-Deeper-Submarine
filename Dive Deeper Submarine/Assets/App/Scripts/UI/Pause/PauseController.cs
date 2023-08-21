using SOG.UI.Menu;
using SOG.UI.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Pause{
  public class PauseController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private PauseView view;

    //Internal varibales

    #region My Methods
    public void ContinueButtonPressed(){ContinueEvent.Raise();}
    public void ToMenuButtonPressed(){view.gameObject.SetActive(false); ToMenuEvent.Raise();}
    public void OptionsButtonPressed(){
      view.gameObject.SetActive(false); OptionsButtonEvent.Raise(new OptionsButtonEventArgs(UIEnum.PAUSE));
    }
    public void RestartButtonPressed(){ view.gameObject.SetActive(false); RestartEvent.Raise();}
    private void onPauseEventHandler(){view.gameObject.SetActive(true);}
    private void onContinueEventHandler(){view.gameObject.SetActive(false);}
    private void onBackButtonPressedEventHadnler(BackButtonEventArgs eventArgs) {
      if (eventArgs.From == UIEnum.PAUSE) view.gameObject.SetActive(true);
    }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      PauseEvent.OnPauseEvent += onPauseEventHandler;
      ContinueEvent.OnContinueEvent += onContinueEventHandler;
      BackButtonEvent.OnBackButtonPressedEvent += onBackButtonPressedEventHadnler;
    }
    private void OnDisable(){
      PauseEvent.OnPauseEvent -= onPauseEventHandler;
      ContinueEvent.OnContinueEvent -= onContinueEventHandler;
      BackButtonEvent.OnBackButtonPressedEvent += onBackButtonPressedEventHadnler;
    }
    #endregion
  }
}
