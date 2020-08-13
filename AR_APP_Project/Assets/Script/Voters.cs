using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Voters : MonoBehaviour
{
    /* Like and Dislike Buttons*/
    public Button likeBtn;
    public Button disLikeBtn;

    /* Like and Dislike for AM and PM bookings*/
    public static int AMlikeCounter = 0;
    public static int AMdisLikeCounter = 0;
    public static int PMlikeCounter = 0;
    public static int PMdisLikeCounter = 0;

    /* AM and PM Toggle Buttons*/
    //public Toggle AM;
    //public Toggle PM;
    private PeriodButtons periodSelection;

    /* Boolean value for AM and PM Like and Dislike */
    private bool likedAM = false;
    private bool dislikedAM = false;
    private bool likedPM = false;
    private bool dislikedPM = false;

    /* Check the selected Button */
    private bool AMSelected;
    private bool PMSelected;
    private bool userHasSelectedOnce = false;
    private ClientAPIs clientAPIs;
    private GPS gPS;

    void Start()
    {
        clientAPIs = GameObject.FindGameObjectWithTag("Client").GetComponent<ClientAPIs>();
        gPS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GPS>();
        periodSelection = GameObject.FindGameObjectWithTag("Period Select").GetComponent<PeriodButtons>();
    }

    /* Add event listener for Booking Button */

    void OnEnable()
    {
        likeBtn.onClick.AddListener(() => ButtonCallBack(likeBtn));
        disLikeBtn.onClick.AddListener(() => ButtonCallBack2(disLikeBtn));

    }

    /* AM Button */

    public void ButtonCallBack(Button buttonPressed)
    {

        if (!gPS.InPlace())
            return;
        if (buttonPressed == likeBtn)
        {
            if (periodSelection.getIsAMSelected())
                LikeAM();
            else if (periodSelection.getIsPMSelected())
                LikePM();

        }
    }
    /* PM Button */

    public void ButtonCallBack2(Button buttonPressed)
    {
        if (!gPS.InPlace())
            return;
        if (buttonPressed == disLikeBtn)
        {
            if (periodSelection.getIsAMSelected())
                DislikeAM();
            else if (periodSelection.getIsPMSelected())
                DislikePM();
        }
    }
    /* Un-Register Button Events */
    void OnDisable()
    {
        likeBtn.onClick.RemoveAllListeners();
        disLikeBtn.onClick.RemoveAllListeners();
    }
    /*Check if AM is liked and update post vote*/
    void LikeAM()
    {
        if (likedAM == true)
            return;
        if (dislikedAM == true)
        {
            likedAM = true;
            dislikedAM = false;
            AMdisLikeCounter -= 1;
        }
        clientAPIs.Vote("Like", "AM");
        AMlikeCounter += 1;
        likedAM = true;
    }
    /*Check if AM is Disliked and update post vote*/

    void DislikeAM()
    {
        if (dislikedAM == true)
            return;
        if (likedAM == true)
        {
            likedAM = false;
            dislikedAM = true;
            AMlikeCounter -= 1;
        }

        clientAPIs.Vote("Dislike", "AM");
        AMdisLikeCounter += 1;
        dislikedAM = true;
    }
    /*Check if PM is liked and update post vote*/

    void LikePM()
    {
        if (likedPM == true)
            return;
        if (dislikedPM == true)
        {
            likedPM = true;
            dislikedPM = false;
            PMdisLikeCounter -= 1;
        }

        Debug.Log("Clicked: " + likeBtn.name + " Evening");
        clientAPIs.Vote("Like", "PM");
        PMlikeCounter += 1;
        likedPM = true;
    }
    /*Check if PM is Disliked and update post vote*/

    void DislikePM()
    {
        if (dislikedPM == true)
            return;
        if (likedPM == true)
        {
            likedPM = false;
            dislikedPM = true;
            PMlikeCounter -= 1;
        }

        Debug.Log("Clicked: " + disLikeBtn.name + " Evening");
        clientAPIs.Vote("Dislike", "PM");
        PMdisLikeCounter += 1;
        dislikedPM = true;
    }
    /*Update AM and PM selected button */

    void Update()
    {
        if (!userHasSelectedOnce)
        {
            if (periodSelection.getIsAMSelected())
            {
                AMSelected = true;
                userHasSelectedOnce = true;
                Debug.Log("Am is selected");

            }

            else if (periodSelection.getIsPMSelected())
            {
                PMSelected = true;
                userHasSelectedOnce = true;
                Debug.Log("Pm is selected");

            }
        }

        /*if (AM.isOn && PM.isOn)
        {
            if (AMSelected)
            {
                AMSelected = false;
                AM.isOn = false;
                PMSelected = true;
            }

            else if (PMSelected)
            {
                PMSelected = false;
                PM.isOn = false;
                AMSelected = true;
            }

        }*/
    }
}
