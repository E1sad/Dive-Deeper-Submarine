namespace SOG.Player{
  public static class InteractionButtonPressedEvent{
    public static event System.Action EventInteractionButtonPressed;
    public static void Raise() { EventInteractionButtonPressed?.Invoke(); }
  }
  public static class InteractionButtonRleasedEvent {
    public static event System.Action EventInteractionButtonRleased;
    public static void Raise() { EventInteractionButtonRleased?.Invoke(); }
  }
}
