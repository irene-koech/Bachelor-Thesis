using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeriodButtons : MonoBehaviour
{
    public Button AM;
    public Button PM;

    private bool isAMSelected = false;
    private bool isPMSelected = false;

    void OnEnable()
    {
        AM.onClick.AddListener(() => ButtonCallBack("AM"));
        PM.onClick.AddListener(() => ButtonCallBack("PM"));
    }

    public void ButtonCallBack(string period)
    {
        if (period.Equals("AM"))
        {
            isAMSelected = true;
            isPMSelected = false;
        }
        else if (period.Equals("PM"))
        {
            isAMSelected = false;
            isPMSelected = true;
        }
        Debug.Log("I'm the best Mwahahaha");
    }

    public bool getIsAMSelected()
    {
        return isAMSelected;
    }

    public bool getIsPMSelected()
    {
        return isPMSelected;
    }
}
