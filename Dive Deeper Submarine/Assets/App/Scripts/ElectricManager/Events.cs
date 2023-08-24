namespace SOG.ElectricManager{
  public static class ElectricPowerOutEvent {
    public static event System.Action EventElectricPowerOut;
    public static void Raise() { EventElectricPowerOut?.Invoke(); }
  }
  public static class ElectricPowerRestoredEvent {
    public static event System.Action EventElectricPowerRestored;
    public static void Raise() { EventElectricPowerRestored?.Invoke(); }
  }
}
