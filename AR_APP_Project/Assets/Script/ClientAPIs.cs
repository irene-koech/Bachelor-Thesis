using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ClientAPIs : MonoBehaviour
{
 /* Url get data, post bookings and post votes */
    public string url;
    public string urlV;
    public string urlB;

 /* Display database */
    public Text AM;
    public Text PM;
    public Text AMlike;
    public Text PMlike;
    public Text AMDislike;
    public Text PMDislike;

/* Identify users devices */
    private string deviceID;

/* Bookings */
    public void Book(string period)
    {
        var book = new Booking()
        {
            period = period,
            userID = deviceID
        };
        StartCoroutine(Post(urlB, book));
        Debug.Log("Users booking id" + book.userID + period.ToString());
    }
    public void Vote(string votes, string period)
    {
        var vote = new Voting()
        {
            period = period,
            vote = votes,
            userID = deviceID
        };
        StartCoroutine(PostVote(urlV, vote));
        Debug.Log("Votings" + vote.userID + vote.ToString());
    }


    void Start()
    {
        deviceID = "jksjjkdfjjknfkndv";
        //deviceID = SystemInfo.deviceUniqueIdentifier;
        StartCoroutine(Get(url));

    }

    public IEnumerator Get(string url)
    {
        /*
         * Make a web request by defining a new UnityWebRequest
         */
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {

                if (www.isDone)
                {
                    /*
                     * feed the data as www.downloadHandler.data from our api. Return our parsed data as string(results)
                     */

                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log("Blablabla");
                    Debug.Log(result);
                    Debug.Log("URL: " + url);
                    DataList dataList = JsonUtility.FromJson<DataList>(result);
                    AM.text = "AM Bookings " + dataList.amCounter.ToString();
                    PM.text = "PM Bookings " + dataList.pmCounter.ToString();

                    AMlike.text = "AM Likes " + dataList.AMlikeCounter.ToString();
                    PMlike.text = "PM Likes " + dataList.PMlikeCounter.ToString();

                    AMDislike.text = "AM Dislikes " + dataList.AMdisLikeCounter.ToString();
                    PMDislike.text = "PM Dislikes " + dataList.PMdisLikeCounter.ToString();
                    Debug.Log("jbsajkhlcdsld");
                    

                }
                else
                {
                    /* handle the problem*/
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    public IEnumerator Post(string url, Booking book)
    {
        var jsonData = JsonUtility.ToJson(book);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();
            
        }
        StartCoroutine(Get(this.url));
    }

    public IEnumerator PostVote(string url, Voting vote)

    {
        var jsonData = JsonUtility.ToJson(vote);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();
            
        }
        StartCoroutine(Get(this.url));
    }
}
