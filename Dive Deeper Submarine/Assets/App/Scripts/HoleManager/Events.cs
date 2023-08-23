using System;

namespace SOG.HoleManager{
  public static class PlusHoleEvent {
    public static event Action EventPlusHole; 
    public static void Raise() { EventPlusHole?.Invoke(); }
  }
  public static class MinusHoleEvent {
    public static event Action EventMinusHole;
    public static void Raise() { EventMinusHole?.Invoke(); }
  }
  public static class DestroyHoleEvent {
    public delegate void DestroyHoleDelegate(Hole hole);
    public static event DestroyHoleDelegate EventDestroyHole;
    public static void Raise(Hole hole) {EventDestroyHole?.Invoke(hole);}
  }
}
