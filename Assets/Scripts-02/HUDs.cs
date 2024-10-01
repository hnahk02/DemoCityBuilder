using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDs : MonoBehaviour
{
    public Button[] BtnArr;

    public Action<int> OnClickButton;

    private void Start()
    {
        int count = 0;

        for (int i = 1; i < BtnArr.Length; i++)
        {
            int id = count;
            BtnArr[i-1].onClick.AddListener(() => {
                OnClickButton?.Invoke(i);
                Debug.Log(i);
            });
       
        }
    }

}
