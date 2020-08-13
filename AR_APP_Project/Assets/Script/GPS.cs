using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GPS : MonoBehaviour
{
    public static GPS Instance { set; get; }
    private const float gymLatitude = 56.8438f;
    private const float gymLongitude = 14.80258f;

    public float latitude;
    public float longitude;
    public Text coordinatesText;
    public Text notInPlaceText;
    private int nextUpdateTime = 1;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(StartLocationServices());
    }

    void Update()
    {
        if (Time.time > nextUpdateTime)
        {
            nextUpdateTime++;
            StartCoroutine(StartLocationServices());
        }
    }
    private IEnumerator StartLocationServices()
    {
        /* check if user has location service enabled */
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enable GPS");
            yield break;
        }
            

        /* Start service before querying location */
        Input.location.Start();

        /* Wait until service initializes */
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        /* Service didn't initialize in 20 seconds */
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }
        /* Connection has failed */
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        if (InPlace())
            notInPlaceText.gameObject.SetActive(false);
        else
            notInPlaceText.gameObject.SetActive(true);
        Debug.Log("Coordinates:");
        Debug.Log(latitude);
        Debug.Log(longitude);
        coordinatesText.text = "Latitude: " + latitude + "\nLongitude: " + longitude;
        yield break;
    }

    public bool InPlace()
    {
        return Math.Abs(gymLatitude - latitude) <= 0.0003 && Math.Abs(gymLongitude - longitude) <= 0.0003;
    }

}