using UnityEngine;

namespace SOG.SaveManager {
  public class SaveManagerObject : MonoBehaviour {
    /*[Header("Variables")]*/

    //[Header("Links")]

    //Internal varibales
    private Data _data;
    private int _bestScore = 0;
    private bool _isMusicOn = true;
    private bool _isSoundOn = true;

    #region My Methods
    private void Save() {
      Data data = new Data( _bestScore, _isMusicOn, _isSoundOn);
      SaveSytem.SaveData(data, Application.persistentDataPath + "/alma.txt");
    }
    private void Load() {
      _data = SaveSytem.LoadData(Application.persistentDataPath + "/alma.txt");
      if (_data == null) { _data = new Data(0, true, true); }
      SendDataToObjects.Raise( _data.BestScore, _data.IsMusicOn, _data.IsSoundOn); 
      
    }
    private void saveBestScoreEventHandler(int bestScore) {_bestScore = bestScore; Save(); }
    private void saveMusicSettingsEventHandler(bool isMusicOn, bool isSoundOn) {
      _isMusicOn = isMusicOn; _isSoundOn = isSoundOn; Save();
    }
    #endregion

    #region Unity's Methods
    private void Start() { Load(); }
    private void OnEnable() {
      SaveBestScore.SaveBestScoreEvent += saveBestScoreEventHandler;
      SaveMusicSettings.SaveMusicSettingsEvent += saveMusicSettingsEventHandler;
    }
    private void OnDisable() {
      SaveBestScore.SaveBestScoreEvent -= saveBestScoreEventHandler;
      SaveMusicSettings.SaveMusicSettingsEvent -= saveMusicSettingsEventHandler;
    }
    #endregion
  }
}
