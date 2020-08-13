using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    /*AM and PM Toggle Buttons */

    //public Toggle AM;
    //public Toggle PM;
    private PeriodButtons periodSelection;

    public GameObject calendarGameObject;
    private Calender calender;
    private ClientAPIs clientAPIs;


   void Start()
    {
    //    calender = calendarGameObject.GetComponent<Calender>();
        clientAPIs = GameObject.FindGameObjectWithTag("Client").GetComponent<ClientAPIs>();
        periodSelection = GameObject.FindGameObjectWithTag("Period Select").GetComponent<PeriodButtons>();
    }
    /*Check if AM or PM is selected, AM <12 and PM is after 12*/

    void ActiveBtnToggle()
    {
        if (periodSelection.getIsAMSelected() && System.DateTime.Now.Hour < 12)

        {
            Debug.Log("AM");
            clientAPIs.Book("AM");
            //Calender.amCounter += 1;
        }
        else


        if (periodSelection.getIsPMSelected() && System.DateTime.Now.Hour >= 12)
        {
            Debug.Log("PM");
            clientAPIs.Book("PM");
            //Calender.pmCounter += 1;
        }
    }
    /*Submit the selected booking button */

    public void OnSubmit()
    {
        Debug.Log("Selecting button");
        ActiveBtnToggle();
    }

    void Deactivate()
    {

    }
}
