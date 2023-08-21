using SOG.UI.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Credits{
  public class CreditsController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private CreditsView view;

    //Internal varibales
    private UIEnum from;

    #region My Methods
    public void OnBackButtonPressed(){
      view.gameObject.SetActive(false); BackButtonEvent.Raise(new BackButtonEventArgs(from));
    }
    private void onCreditsButtonPressedEventHandler(CreditsButtonEventArgs eventArgs){
      from = eventArgs.From; view.gameObject.SetActive(true);
    }
    #endregion

    #region Unity's Methods
    private void Start(){from = UIEnum.MENU;}
    private void OnEnable(){
      CreditsButtonEvent.OnCreditsButtonPressedEvent += onCreditsButtonPressedEventHandler;
    }
    private void OnDisable(){
      CreditsButtonEvent.OnCreditsButtonPressedEvent -= onCreditsButtonPressedEventHandler;
    }
    #endregion
  }
}
