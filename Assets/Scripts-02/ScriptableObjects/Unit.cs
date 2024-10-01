using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Rendering;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Dictionary<Unit, int> UnitLevelDict = new Dictionary<Unit, int>();


   public enum CostType{
        One,
        Two,
        Three,
        Four,
        Five
   }

   public CostType costType;

   public void SetCostType(int id){
        if(id == 1) costType = CostType.One;
        if(id == 2) costType = CostType.Two;
        if(id == 3) costType = CostType.Three;
   }

   


}
