using SOG.SaveManager;
using SOG.UI.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Options{
  public class OptionsController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private OptionsView view;
    [SerializeField] private Sprite _toggleOn;
    [SerializeField] private Sprite _toggleOff;
    [SerializeField] private UnityEngine.UI.Image _muiscToggleBox;
    [SerializeField] private UnityEngine.UI.Image _soundToggleBox;

    //Internal Variables 
    private UIEnum from;
    private bool _isMusicOn;
    private bool _isSoundOn;

    #region My Methods
    public void OnBackButtonPressed() { 
      view.gameObject.SetActive(false); BackButtonEvent.Raise(new BackButtonEventArgs(from)); 
    }
    private void onOptionsButtonPressedEventHandler(OptionsButtonEventArgs eventArgs){
      from = eventArgs.From; view.gameObject.SetActive(true);
    }
    public void MusicToggle() {
      _isMusicOn = !_isMusicOn;
      if (_isMusicOn) {
        MusicAndSoundManager.Instance.UnMuteMusic(); _muiscToggleBox.sprite = _toggleOn;
      } else {
        MusicAndSoundManager.Instance.MuteMusic(); _muiscToggleBox.sprite = _toggleOff;}
    }
    public void SoundToggle() {
      _isSoundOn = !_isSoundOn;
      if (_isSoundOn) {
        MusicAndSoundManager.Instance.UnMuteSounds(); _soundToggleBox.sprite = _toggleOn;}
      else {
        MusicAndSoundManager.Instance.MuteSounds(); _soundToggleBox.sprite = _toggleOff;}
    }
    private void SendDataToObjectsEventHandler(int bestScore, bool isMusicon, bool isSoundOn) {
      _isMusicOn = !isMusicon; _isSoundOn = !isSoundOn;
      if (_isMusicOn) {
        MusicAndSoundManager.Instance.UnMuteMusic(); _muiscToggleBox.sprite = _toggleOn;
      } else { MusicAndSoundManager.Instance.MuteMusic(); _muiscToggleBox.sprite = _toggleOff; }
      if (_isSoundOn) {
        MusicAndSoundManager.Instance.UnMuteSounds(); _soundToggleBox.sprite = _toggleOn;
      } else { MusicAndSoundManager.Instance.MuteSounds(); _soundToggleBox.sprite = _toggleOff; }
    }
    #endregion

    #region Unity's Methods
    private void Start(){from = UIEnum.MENU;}
    private void OnEnable(){
      OptionsButtonEvent.OnOptionsButtonPressedEvent += onOptionsButtonPressedEventHandler;
      SendDataToObjects.SendDataToObjectsEvent += SendDataToObjectsEventHandler;
    }
    private void OnDisable(){
      OptionsButtonEvent.OnOptionsButtonPressedEvent -= onOptionsButtonPressedEventHandler;
      SendDataToObjects.SendDataToObjectsEvent -= SendDataToObjectsEventHandler;
    }
    #endregion
  }
}
