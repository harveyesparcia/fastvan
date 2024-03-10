using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassengerView : MonoBehaviour
{
    [SerializeField] private GameObject ModalAddSchedule;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject ModalMEssage;
    [SerializeField] private TMP_Text count;
    [SerializeField] private GameObject ModalMessage2;
    [SerializeField] private TMP_Text message2;
    [SerializeField] private GameObject scheduleGameObject;
    [SerializeField] private TMP_Text name;
    [SerializeField] private GameObject seat;
    [SerializeField] private TMP_Text message3;
    [SerializeField] private GameObject bookedobjt;


    [SerializeField] private Button driver;
    [SerializeField] private Button driverarea2;
    [SerializeField] private Button driverarea3;
    [SerializeField] private Button firstseat1;
    [SerializeField] private Button firstseat2;
    [SerializeField] private Button firstseat3;
    [SerializeField] private Button firstseat4;
    [SerializeField] private Button secondseat1;
    [SerializeField] private Button secondseat2;
    [SerializeField] private Button secondseat3;
    [SerializeField] private Button secondseat4;
    [SerializeField] private Button thirdseat1;
    [SerializeField] private Button thirdseat2;
    [SerializeField] private Button thirdseat3;
    [SerializeField] private Button thirdseat4;
    [SerializeField] private Button Lastseat1;
    [SerializeField] private Button Lastseat2;
    [SerializeField] private Button Lastseat3;
    [SerializeField] private Button Lastseat4;

    private static Dictionary<string, int> seats = new Dictionary<string, int>();

    public ScrollRect scrollView;
    public GameObject listItemPrefab;

    void Start()
    {
        if (Context.IsLogin)
        {
            show();
            DataModels.Instance.OnAddSchedule += OnsheduleChanged;
            DataModels.Instance.OnUpdateSchedule += OnUpdateScheduleChanged;
            DataModels.Instance.OnCountSchedule += OnCountScheduleChanged;
            DataModels.Instance.OnDriverGetSchedule += OnDriverGetSchedule;
        }
    }

    private void OnDriverGetSchedule(List<ScheduledTransaction> model)
    {
        if (model != null)
        {
            UpdateSeat(model.FirstOrDefault());
            seat.gameObject.SetActive(true);
            bookedobjt.gameObject.SetActive(false);
            seats.Clear();
        }
    }

    public void SeatStatusTapped()
    {
    }

    public void AddtoUpdateSeats(string seatNumber)
    {
        if (!seats.ContainsKey(seatNumber))
        {
            seats.Add(seatNumber, 1);
        }
        else
        {
            seats.Remove(seatNumber);
        }
    }

    public void bookedTap()
    {
        string formattedSeats = string.Join(", ", seats.Keys);
        message3.text = $"Your about to book on seat {formattedSeats}";
        bookedobjt.gameObject.SetActive(true);
    }

    public void bookedYesTap()
    {
        bookedobjt.gameObject.SetActive(true);

        DataModels.Instance.UpdateQueues(DataModels.Instance.DriversId, seats);
    }

    public void bookedNoTap()
    {
        bookedobjt.gameObject.SetActive(false);
    }

    public void UpdateSeat(ScheduledTransaction model)
    {
        if (model != null)
        {
            driver.interactable = false;
            driverarea2.interactable = model.FrontSeat1 == 1 ? false : true;
            driverarea3.interactable = model.FrontSeat2 == 1 ? false : true;
            firstseat1.interactable = model.FirstSeat1 == 1 ? false : true;
            firstseat2.interactable = model.FirstSeat2 == 1 ? false : true;
            firstseat3.interactable = model.FirstSeat3 == 1 ? false : true;
            firstseat4.interactable = model.FirstSeat4 == 1 ? false : true;
            secondseat1.interactable = model.SecondSeat1 == 1 ? false : true;
            secondseat2.interactable = model.SecondSeat2 == 1 ? false : true;
            secondseat3.interactable = model.SecondSeat2 == 1 ? false : true;
            secondseat4.interactable = model.SecondSeat2 == 1 ? false : true;
            thirdseat1.interactable = model.ThirdSeat1 == 1 ? false : true;
            thirdseat2.interactable = model.ThirdSeat2 == 1 ? false : true;
            thirdseat3.interactable = model.ThirdSeat3 == 1 ? false : true;
            thirdseat4.interactable = model.ThirdSeat4 == 1 ? false : true;
            Lastseat1.interactable = model.FourthSeat1 == 1 ? false : true;
            Lastseat2.interactable = model.FourthSeat2 == 1 ? false : true;
            Lastseat3.interactable = model.FourthSeat3 == 1 ? false : true;
            Lastseat4.interactable = model.FourthSeat4 == 1 ? false : true;
        }
    }

    private void OnCountScheduleChanged(int obj)
    {
        ModalMessage2.gameObject.SetActive(false);
        ModalAddSchedule.gameObject.SetActive(true);
        var total = obj + 1;
        count.text = total.ToString();
    }

    private void OnUpdateScheduleChanged()
    {
        scheduleGameObject.gameObject.SetActive(true);
        PopulateList();
    }

    void PopulateList()
    {
        RectTransform contentTransform = scrollView.content;
        foreach (var data in DataModels.Instance.Queue.Where(x => x.Status == 1))
        {
            DataModels.Instance.DriversId = data.DriversId;
            GameObject listItem = Instantiate(listItemPrefab, contentTransform);

            TMP_Text itemText = listItem.GetComponentInChildren<TMP_Text>();

            Transform driverNameTransform = listItem.transform.Find("DriversNameValue");
            Transform plateNumberTransform = listItem.transform.Find("PlateNumber");
            Transform totalPassengerform = listItem.transform.Find("TotalPassenger");
            Transform maxCapacityform = listItem.transform.Find("MaxCapacity");
            Transform driversIdform = listItem.transform.Find("DriversId");
            Transform QueuesIdform = listItem.transform.Find("QueuesId");

            if (QueuesIdform != null)
            {
                TMP_Text queuesIdText = driversIdform.GetComponent<TMP_Text>();
                if (queuesIdText != null)
                {
                    queuesIdText.text = data.Id.ToString();
                }
            }

            if (driversIdform != null)
            {
                TMP_Text driverIdText = driversIdform.GetComponent<TMP_Text>();
                if (driverIdText != null)
                {
                    driverIdText.text = data.Id.ToString() +";"+ data.DriversId.ToString();
                }
            }

            if (driverNameTransform != null)
            {
                TMP_Text driverNameText = driverNameTransform.GetComponent<TMP_Text>();
                if (driverNameText != null)
                {
                    driverNameText.text = data.DriversId.ToString();
                }
            }

            if (maxCapacityform != null)
            {
                TMP_Text maxCapacityText = maxCapacityform.GetComponent<TMP_Text>();
                if (maxCapacityText != null)
                {
                    maxCapacityText.text = "18";
                }
            }

            if (plateNumberTransform != null)
            {
                TMP_Text plateNumberText = plateNumberTransform.GetComponent<TMP_Text>();
                if (plateNumberText != null)
                {
                    plateNumberText.text = data.VanPlateNumber.ToString();
                }
            }

            if (totalPassengerform != null)
            {
                TMP_Text totalPassengerText = totalPassengerform.GetComponent<TMP_Text>();
                if (totalPassengerText != null)
                {
                    totalPassengerText.text = "N/A";
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);
        }

        GameObject templateObject = contentTransform.Find("Image").gameObject;
        templateObject.SetActive(false);

    }

    public void scheduleBack()
    {
        scheduleGameObject.gameObject.SetActive(false);
    }

    public void ScheduleTapped()
    {
        DataModels.Instance.GetQueues();
    }

    private void OnsheduleChanged(bool obj)
    {
        ModalMessage2.gameObject.SetActive(true);
        if (obj)
        {
            message2.text = "Succesfully added a scheduled.";
        }
        else
        {
            message2.text = "Failed to add a scheduled.";
        }
    }

    public void AddTapped()
    {
        ModalMEssage.gameObject.SetActive(true);
    }

    public void NoTapped()
    {
        show();
        ModalMEssage.gameObject.SetActive(false);
        ModalAddSchedule.gameObject.SetActive(false);
        ModalMessage2.gameObject.SetActive(false);
        seat.gameObject.SetActive(false);
    }

    public void YesTapped()
    {
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void show()
    {
        name.text = Context.firstname + "  " + Context.lastname;
        ModalAddSchedule.gameObject.SetActive(false);
        canvasMenu.gameObject.SetActive(true);
        seat.gameObject.SetActive(false);
        bookedobjt.gameObject.SetActive(false);
    }

    public void AddBookingsTapped()
    {
        DataModels.Instance.GetCountQueues();
    }

    public void GotoSeat(TMP_Text QueueId)
    {
        var data = QueueId.text.ToString().Split(';');
        DataModels.Instance.GetDriverSchedule(data[1], data[0]);

    }

    public void SeatBack()
    {
        show();
        ModalMEssage.gameObject.SetActive(false);
        ModalAddSchedule.gameObject.SetActive(false);
        ModalMessage2.gameObject.SetActive(false);
        seat.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Context.IsLogin && DataModels.Instance != null)
        {
            DataModels.Instance.OnAddSchedule -= OnsheduleChanged;
            DataModels.Instance.OnUpdateSchedule -= OnUpdateScheduleChanged;
            DataModels.Instance.OnCountSchedule -= OnCountScheduleChanged;
            DataModels.Instance.OnDriverGetSchedule -= OnDriverGetSchedule;
        }
    }

    public void LogoutTapped()
    {
        SceneManager.LoadScene("LoginScene");
        SceneManager.UnloadSceneAsync("Passenger");
    }

    public void SaveTapped()
    {
        DataModels.Instance.CreateQueues(count.text);
        DataModels.Instance.ProcessScheduleTransactions(count.text);
    }
}
