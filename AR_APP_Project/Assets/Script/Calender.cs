using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Calender : MonoBehaviour
{
    public Text clockText;
    public Text dayText;

    // Update is called once per frame
    void Update()
    {        
        DateTime time = System.DateTime.Now;
        clockText.text = time.Hour + ":" + time.Minute;
        dayText.text = "" + time.Day + "/" + time.Month + "/" + time.Year;
    }
  
}
