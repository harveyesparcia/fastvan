using Gravitons.UI.Modal;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DataModels : MonoBehaviour
{
    public Action<bool> OnAddSchedule;
    public Action<int> OnAddQueue;
    public Action<int> OnCountSchedule;
    public Action<bool> OnUpdateSchedule;
    public Action<QueuesModel> OnDriverUpdateSchedule;
    public Action<List<ScheduledTransaction>> OnDriverGetSchedule;
    public Action<bool> OnCheckExist;
    public Action<bool, UserModel> OnRegisterChanged;
    public Action<bool, List<UserModel>> OnListOfDriversChanged;
    private int currentQueue;

    public int CurrentQueue
    {
        get { return currentQueue; }
        set { currentQueue = value; }
    }

    private string driversId;

    public string DriversId
    {
        get { return driversId; }
        set { driversId = value; }
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

    public void RegisterDriver(string firstname, string lastname, string date, string address, string contactnumber, string email, string platenumber, string driverlicenseNumber)
    {
        StartCoroutine(Registation(firstname, lastname, date, address, contactnumber, email, platenumber, driverlicenseNumber));
    }

    public void GetDriverSchedule(string driversId, string QueuesId)
    {
        StartCoroutine(Get_DriverSchedule(driversId, int.Parse(QueuesId)));
    }

    public void CreateQueues(string count)
    {
        StartCoroutine(Create_Queues(int.Parse(count)));
    }

    public void GetListOfDrivers()
    {
        StartCoroutine(Get_List_Of_Drivers());
    }

    public void GetQueues(bool onupdate)
    {
        StartCoroutine(Get_Queues(onupdate));
    }

    public void GetQueues(string driversId)
    {
        StartCoroutine(Get_Queues(driversId));
    }

    public void CheckIfQueuesExist(string driversId)
    {
        StartCoroutine(Check_If_Queues_Exist(driversId));
    }


    public void GetCountQueues()
    {
        StartCoroutine(Get_CountQueues());
    }

    public void UpdateQueues(string driversId, Dictionary<string, int> parameters)
    {
        StartCoroutine(Update_Schedule(driversId, parameters));
    }

    public void CreatePassengerTransactions(string driversId, Dictionary<string, int> parameters)
    {
    }

    public void ProcessScheduleTransactions(int queuesId)
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

    private IEnumerator Get_Queues(bool update)
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

                        OnUpdateSchedule?.Invoke(update);
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
        form.AddField("DriversId", driversId.Trim());
        form.AddField("QueueId", queueId.ToString().Trim());
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

    private IEnumerator Check_If_Queues_Exist(string driversId)
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", Context.DriversId);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/CheckIfQueuesExist.php", form);

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
                var response = JsonConvert.DeserializeObject<CheckExistResponse>(jsonResponse);

                if (response != null)
                {
                    if (response.status.Contains("success"))
                    {
                        OnCheckExist.Invoke(response.exists);
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
            form.AddField($"{param.Key}Name", Context.firstname);
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

                                UpdateCompleted(response.Data, driversId, currentQueue);

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

    private void UpdateCompleted(ScheduledTransaction scheduledTransaction, string driversId, int currentQueue)
    {

        if (scheduledTransaction.FrontSeat1 == 1 && scheduledTransaction.FrontSeat2 == 1 &&
            scheduledTransaction.FirstSeat1 == 1 && scheduledTransaction.FirstSeat2 == 1 && scheduledTransaction.FirstSeat3 == 1 && scheduledTransaction.FirstSeat4 == 1 &&
            scheduledTransaction.SecondSeat1 == 1 && scheduledTransaction.SecondSeat2 == 1 && scheduledTransaction.SecondSeat3 == 1 && scheduledTransaction.SecondSeat4 == 1 &&
            scheduledTransaction.ThirdSeat1 == 1 && scheduledTransaction.ThirdSeat2 == 1 && scheduledTransaction.ThirdSeat3 == 1 && scheduledTransaction.ThirdSeat4 == 1 &&
            scheduledTransaction.FourthSeat1 == 1 && scheduledTransaction.FourthSeat2 == 1 && scheduledTransaction.FourthSeat3 == 1 && scheduledTransaction.FourthSeat4 == 1)
        {
            StartCoroutine(Update_Completed(driversId, currentQueue));
        }
    }

    private IEnumerator Update_Completed(string driversId, int currentQueue)
    {

        WWWForm form = new WWWForm();
        form.AddField("DriversId", driversId);
        form.AddField("Status", 0);

        using (UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/update_queues.php", form))
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
                    StartCoroutine(Update_ScheduleCompleted(driversId));
                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception: " + ex.Message);
                }
            }
        }
    }

    private IEnumerator Update_ScheduleCompleted(string driversId)
    {

        WWWForm form = new WWWForm();
        form.AddField("DriversId", driversId);
        form.AddField("Status", 0);

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

        }
        else
        {
            try
            {
                string jsonResponse = request.downloadHandler.text;
                StartCoroutine(Create_ScheduledTransactions(count));
            }
            catch (Exception ex)
            {
            }

        }
    }

    private IEnumerator Create_ScheduledTransactions(int queuesId)
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", Context.DriversId);
        form.AddField("QueuesId", queuesId);
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

    private IEnumerator Create_PassengerTransaction(int count)
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

        }
        else
        {
            try
            {
                string jsonResponse = request.downloadHandler.text;
                StartCoroutine(Create_ScheduledTransactions(count));
            }
            catch (Exception ex)
            {
            }

        }
    }

    IEnumerator Registation(string firstname, string lastname, string date, string address, string contactnumber, string email, string platenumber, string driverlicenseNumber)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", string.Empty);
        form.AddField("password", string.Empty);
        form.AddField("firstname", firstname);
        form.AddField("lastname", lastname);
        form.AddField("typeofaccount", 2);
        form.AddField("BirthDate", date);
        form.AddField("Address", address);
        form.AddField("IsResign", 0);
        form.AddField("IsBlocked", 0);
        form.AddField("ContactNumber", contactnumber);
        form.AddField("Email", email);
        form.AddField("isDriver", 1);
        form.AddField("PlateNumber", platenumber);
        form.AddField("DriversLicenseNumber", driverlicenseNumber);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/register.php", form);

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
                var response = JsonConvert.DeserializeObject<UserModel>(jsonResponse);
                Debug.Log("Response: " + jsonResponse);
                if (response.Status.Contains("success"))
                {
                    OnRegisterChanged.Invoke(true, response);

                }
                else
                {
                    OnRegisterChanged.Invoke(false, null);

                }
            }
            catch (Exception ex)
            {
                OnRegisterChanged.Invoke(false, null);
            }

        }
    }


    IEnumerator Get_List_Of_Drivers()
    {
        WWWForm form = new WWWForm();
        //form.AddField("DriversId", string.Empty);


        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/list_ofdrivers.php", form);

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
                var response = JsonConvert.DeserializeObject<DriverResponse>(jsonResponse);
                Debug.Log("Response: " + jsonResponse);
                if (response.status.Contains("success"))
                {
                    OnListOfDriversChanged.Invoke(true, response.data);

                }
                else
                {
                    OnListOfDriversChanged.Invoke(false, null);

                }
            }
            catch (Exception ex)
            {
                OnListOfDriversChanged.Invoke(false, null);
            }

        }
    }
}