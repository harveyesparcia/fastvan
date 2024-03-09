using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class DataModels : MonoBehaviour
{
    public Action<bool> OnAddSchedule;
    public Action<int> OnCountSchedule;
    public Action OnUpdateSchedule;
    public Action<QueuesModel> OnDriverUpdateSchedule;
    public Action<List<ScheduledTransaction>> OnDriverGetSchedule;
    private int currentQueue;

    public int CurrentQueue
    {
        get { return currentQueue; }
        set { currentQueue = value; }
    }

    private List<QueuesModel> queue = new List<QueuesModel>();

    public List<QueuesModel> Queue
    {
        get { return queue; }
        set { queue = value; }
    }

    private int currentQueueId;

    public int CurrentQueueId
    {
        get { return currentQueueId; }
        set { currentQueueId = value; }
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

    public void GetDriverSchedule(string driversId, string QueuesId)
    {
        StartCoroutine(Get_DriverSchedule(driversId, int.Parse(QueuesId)));
    }

    public void CreateQueues(string count)
    {
        StartCoroutine(Create_Queues(int.Parse(count)));
    }

    public void GetQueues()
    {
        StartCoroutine(Get_Queues());
    }

    public void GetQueues(string driversId)
    {
        StartCoroutine(Get_Queues(driversId));
    }

    public void GetCountQueues()
    {
        StartCoroutine(Get_CountQueues());
    }

    public void UpdateQueues(string driversId, Dictionary<string, int> parameters)
    {
        StartCoroutine(Update_Schedule(driversId, parameters));
    }

    public void ProcessScheduleTransactions(string queuesId)
    {
        StartCoroutine(Create_ScheduledTransactions(queuesId));
    }

    private IEnumerator Get_CountQueues()
    {
        WWWForm form = new WWWForm();

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/queuelist.php", form);

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
                        Queue = response.data;
                        OnCountSchedule.Invoke(currentQueue);
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
    }

    private IEnumerator Get_Queues()
    {
        WWWForm form = new WWWForm();

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/queuelist.php", form);

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
                        Queue = response.data;

                        OnUpdateSchedule?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
    }

    private IEnumerator Get_DriverSchedule(string driversId, int queueId)
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", driversId);
        form.AddField("QueueId", queueId);
        CurrentQueueId = queueId;

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/select_scheduledtransactions.php", form);

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
                var response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                if (response != null)
                {
                    if (response.Status.Contains("success"))
                    {
                        if (response.Data.Any())
                        {
                            var data = response.Data;
                            OnDriverGetSchedule?.Invoke(data);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
    }

    private IEnumerator Get_Queues(string driversId)
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", Context.DriversId);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/select_queues2.php", form);

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
                        // OnDriverUpdateSchedule?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
    }

    private IEnumerator Update_Schedule(string driversId, Dictionary<string, int> parameters)
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", driversId);
        form.AddField("QueuesId", currentQueueId);

        foreach (var param in parameters)
        {
            form.AddField(param.Key, param.Value);
        }

        using (UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/update_scheduledtransactions.php", form))
        {
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
                    var response = JsonConvert.DeserializeObject<ApiUpdateResponse>(jsonResponse);

                    if (response != null)
                    {
                        if (response.Status.Contains("success"))
                        {
                            if (response.Data != null)
                            {
                                var data = response.Data;
                                OnDriverGetSchedule?.Invoke(new List<ScheduledTransaction> { data });
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception: " + ex.Message);
                }
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

    private IEnumerator Create_ScheduledTransactions(string queuesId)
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", Context.DriversId);
        form.AddField("QueuesId", int.Parse(queuesId));
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
            }

        }
    }
}