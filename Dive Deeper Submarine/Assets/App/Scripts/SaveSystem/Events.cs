using System;

namespace SOG.SaveManager {
  public static class SendDataToObjects {
    public delegate void SendDataToObjectsDelegates(int bestScore, bool isMusicOn, bool isSoundOn);
    public static event SendDataToObjectsDelegates SendDataToObjectsEvent;
    public static void Raise(int bestScore, bool isMusicOn, bool isSoundOn) {
      SendDataToObjectsEvent?.Invoke(bestScore, isMusicOn, isSoundOn);
    }
  }
  public static class SaveBestScore {
    public static event Action<int> SaveBestScoreEvent;
    public static void Raise(int bestScore) { SaveBestScoreEvent?.Invoke(bestScore); }
  }
  public static class SaveMusicSettings {
    public static event Action<bool, bool> SaveMusicSettingsEvent;
    public static void Raise(bool isMusicOn, bool isSoundOn) {
      SaveMusicSettingsEvent?.Invoke(isMusicOn, isSoundOn);
    }
  }
}
