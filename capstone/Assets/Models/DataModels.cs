using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class DataModels : MonoBehaviour
{
    public Action<bool> OnAddSchedule;
    private int currentQueue;

    public int CurrentQueue
    {
        get { return currentQueue; }
        set { currentQueue = value; }
    }

    private static DataModels _instance;

    public static DataModels Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataModels>();
            }
            return _instance;
        }
    }

    public DataModels()
    {

    }

    void Start()
    {

    }

    void Awake()
    {
        // Initialization code here
    }


    public void CreateQueues(string count)
    {
        StartCoroutine(Create_Queues(int.Parse(count)));
    }

    public void GetQueues()
    {
        StartCoroutine(Get_Queues());
    }

    public void UpdateQueues()
    {
        StartCoroutine(Update_Queues());
    }

    public void ProcessScheduleTransactions()
    {
        StartCoroutine(Create_ScheduledTransactions());
    }

    public void ProcessQueues()
    {
        StartCoroutine(Create_ScheduledTransactions());
    }

    private IEnumerator Get_Queues()
    {
        WWWForm form = new WWWForm();

        using UnityWebRequest request = UnityWebRequest.Get("http://www.aasimudin.cctc-ccs.net/Api/select_queues.php");

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
                var response = JsonConvert.DeserializeObject<ResponseQueue>(jsonResponse);

                if (response != null)
                {
                    if (response.status.Contains("success"))
                    {
                        currentQueue = response.data.Count;
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
    }

    private IEnumerator Update_Queues()
    {
        WWWForm form = new WWWForm();

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

    private IEnumerator Create_Queues(int count)
    {
        WWWForm form = new WWWForm();
        form.AddField("VanPlateNumber", string.Empty);
        form.AddField("DepartureDateTime", string.Empty);
        form.AddField("ArrivalDateTime", string.Empty);
        form.AddField("DriversId", Context.DriversId);
        form.AddField("Id", count);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/create_queues.php", form);

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
            OnAddSchedule?.Invoke(false);
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
            OnAddSchedule?.Invoke(false);
        }
        else
        {
            try
            {
                string jsonResponse = request.downloadHandler.text;

                OnAddSchedule?.Invoke(true);
            }
            catch (Exception ex)
            {
                OnAddSchedule?.Invoke(false);
            }

        }
    }
}