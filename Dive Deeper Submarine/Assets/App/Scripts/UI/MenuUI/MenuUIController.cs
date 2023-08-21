using SOG.UI.Credits;
using SOG.UI.Options;
using SOG.UI.Pause;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Menu{
  public class MenuUIController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private MenuUIView view;

    #region My Methods
    public void PlayButtonPressed(){view.gameObject.SetActive(false); PlayButtonEvent.Raise();}
    public void OptionsButtonPressed(){ 
      view.gameObject.SetActive(false); OptionsButtonEvent.Raise(new OptionsButtonEventArgs(UIEnum.MENU)); }
    public void CreditsButtonPressed(){ 
      view.gameObject.SetActive(false); CreditsButtonEvent.Raise(new CreditsButtonEventArgs(UIEnum.MENU)); }
    public void ExitButtonPressed(){Application.Quit();}
    private void onBackButtonPressedEventHadnler(BackButtonEventArgs eventArgs){
      if (eventArgs.From == UIEnum.MENU) view.gameObject.SetActive(true);
    }
    private void onToMenuEventHandler(){view.gameObject.SetActive(true);}
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      BackButtonEvent.OnBackButtonPressedEvent += onBackButtonPressedEventHadnler;
      ToMenuEvent.OnToMenuEvent += onToMenuEventHandler;
    }
    private void OnDisable(){
      BackButtonEvent.OnBackButtonPressedEvent -= onBackButtonPressedEventHadnler;
      ToMenuEvent.OnToMenuEvent -= onToMenuEventHandler;
    }
    #endregion
  }
}
