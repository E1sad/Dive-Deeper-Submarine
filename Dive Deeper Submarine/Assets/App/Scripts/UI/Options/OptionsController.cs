using SOG.UI.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Options{
  public class OptionsController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private OptionsView view;

    //Internal Variables 
    private UIEnum from;

    #region My Methods
    public void OnBackButtonPressed() { 
      view.gameObject.SetActive(false); BackButtonEvent.Raise(new BackButtonEventArgs(from)); 
    }
    private void onOptionsButtonPressedEventHandler(OptionsButtonEventArgs eventArgs){
      from = eventArgs.From; view.gameObject.SetActive(true);
    }
    #endregion

    #region Unity's Methods
    private void Start(){from = UIEnum.MENU;}
    private void OnEnable(){
      OptionsButtonEvent.OnOptionsButtonPressedEvent += onOptionsButtonPressedEventHandler;
    }
    private void OnDisable(){
      OptionsButtonEvent.OnOptionsButtonPressedEvent -= onOptionsButtonPressedEventHandler;
    }
    #endregion
  }
}
