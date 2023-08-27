namespace SOG.SaveManager {
  [System.Serializable]
  public class Data {
    public int BestScore;
    public bool IsMusicOn;
    public bool IsSoundOn;

    public Data(int bestScore, bool isMusicOn, bool isSoundOn) {
      BestScore = bestScore; IsMusicOn = isMusicOn; IsSoundOn = isSoundOn; 
    }
  }
}
