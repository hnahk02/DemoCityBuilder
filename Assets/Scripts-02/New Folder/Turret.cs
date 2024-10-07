using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
   public static event EventHandler OnClickTurret;

   public bool hasWeapon;

    void OnMouseDown(){
        OnClickTurret?.Invoke(this, EventArgs.Empty);
        Debug.Log(gameObject.name);
    }
}
