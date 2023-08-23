using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player{
  public class Interactions : MonoBehaviour{
    //[Header("Variables")]

    //[Header("Links")]

    //Internal varibales

    #region My Methods
    private void Interaction() {
      if (Input.GetKeyDown(KeyCode.E)) { InteractionButtonPressedEvent.Raise(); }
      if (Input.GetKeyUp(KeyCode.E)) { InteractionButtonRleasedEvent.Raise(); }
    }
    #endregion

    #region Unity's Methods
    private void Update() {
      Interaction();
    }
    #endregion
  }
}
