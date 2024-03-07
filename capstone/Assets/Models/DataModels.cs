using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class DataModels : MonoBehaviour
{
    public DataModels()
    {
        
    }

    public void ProcessScheduleTransactions()
    {
        StartCoroutine(Create_ScheduledTransactions());
    }

    private IEnumerator Create_ScheduledTransactions()
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", Context.DriversId);
        form.AddField("ArrivalDateTime", string.Empty);
        form.AddField("DepartureDateTime", string.Empty);
        form.AddField("FrontSeat1", 0);
        form.AddField("FrontSeat2", 0);
        form.AddField("van1stSeat1", 0);
        form.AddField("van1stSeat2", 0);
        form.AddField("van1stSeat3", 0);
        form.AddField("van1stSeat4", 0);
        form.AddField("van2ndSeat1", 0);
        form.AddField("van2ndSeat2", 0);
        form.AddField("van2ndSeat3", 0);
        form.AddField("van2ndSeat4", 0);
        form.AddField("van3rdSeat1", 0);
        form.AddField("van3rdSeat2", 0);
        form.AddField("van3rdSeat3", 0);
        form.AddField("van3rdSeat4", 0);
        form.AddField("van4thSeat1", 0);
        form.AddField("van4thSeat2", 0);
        form.AddField("van4thSeat3", 0);
        form.AddField("van4thSeat4", 0);
        form.AddField("ExtraSeat1", 0);
        form.AddField("ExtraSeat2", 0);
        form.AddField("ExtraSeat3", 0);
        form.AddField("ExtraSeat4", 0);
        form.AddField("Status", 1);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/create_scheduledtransactions.php", form);

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            try
            {
                string jsonResponse = request.downloadHandler.text;
            }
            catch (Exception ex)
            {
            }

        }
    }
}