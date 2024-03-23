using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class DriverView : MonoBehaviour
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
    [SerializeField] private GameObject seatstatus;
    [SerializeField] private TMP_Text message3;
    [SerializeField] private GameObject bookedobjt;
    [SerializeField] private GameObject messageExist;
    [SerializeField] private GameObject changepassView;
    [SerializeField] private GameObject accountSettingsVIew;
    [SerializeField] private TMP_Text headerMessage;
    [SerializeField] private GameObject cancelbookedobjt;
    [SerializeField] private TMP_Text cancelmessage3;
    [SerializeField] private GameObject cancelBooking;


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

  

    [SerializeField] private GameObject changepass;
    [SerializeField] private TMP_Text first;
    [SerializeField] private TMP_Text last;
    [SerializeField] private TMP_Text contact;
    [SerializeField] private TMP_Text address;
    [SerializeField] private TMP_Text birth;
    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text password;


    private static Dictionary<string, int> seats = new Dictionary<string, int>();
    private static Dictionary<string, int> cancelseats = new Dictionary<string, int>();
    private bool isTappped = false;

    public ScrollRect scrollView;
    public GameObject listItemPrefab;

    [SerializeField] private GameObject modalspinner;

    [SerializeField] private GameObject cancelseatlistView;
    [SerializeField] private Button canceldriverarea2;
    [SerializeField] private Button canceldriverarea3;
    [SerializeField] private Button cancelfirstseat1;
    [SerializeField] private Button cancelfirstseat2;
    [SerializeField] private Button cancelfirstseat3;
    [SerializeField] private Button cancelfirstseat4;
    [SerializeField] private Button cancelsecondseat1;
    [SerializeField] private Button cancelsecondseat2;
    [SerializeField] private Button cancelsecondseat3;
    [SerializeField] private Button cancelsecondseat4;
    [SerializeField] private Button cancelthirdseat1;
    [SerializeField] private Button cancelthirdseat2;
    [SerializeField] private Button cancelthirdseat3;
    [SerializeField] private Button cancelthirdseat4;
    [SerializeField] private Button cancelLastseat1;
    [SerializeField] private Button cancelLastseat2;
    [SerializeField] private Button cancelLastseat3;
    [SerializeField] private Button cancelLastseat4;

    [SerializeField] private TMP_Text nameheader;

    void Start()
    {
        if (Context.IsLogin)
        {
            isTappped = false;
            show();
            DataModels.Instance.OnAddSchedule += OnsheduleChanged;
            DataModels.Instance.OnUpdateSchedule += OnUpdateScheduleChanged;
            DataModels.Instance.OnCountSchedule += OnCountScheduleChanged;
            DataModels.Instance.OnDriverGetSchedule += OnDriverGetSchedule;
            DataModels.Instance.OnAddQueue += OnAddQueue;
            DataModels.Instance.OnCheckExist += OnCheckExist;
            DataModels.Instance.OnGetSeatSchedule += OnGetSeatSchedule;
            DataModels.Instance.OnGetPassengerSeatSchedule += OnGetPassengerSeatScheduleChanged;

        }
    }

    public void accountBackTapped()
    {
        changepass.gameObject.SetActive(false);
    }

    public void EditBackTapped()
    {
        accountSettingsVIew.gameObject.SetActive(false);
        changepassView.gameObject.SetActive(false);
    }


    private void OnGetSeatSchedule(ScheduledTransaction transaction)
    {
        modalspinner.gameObject.SetActive(false);
        seatstatus.gameObject.SetActive(true);
    }

    private void OnCheckExist(bool obj)
    {
        modalspinner.gameObject.SetActive(false);
        if (obj)
        {
            messageExist.gameObject.SetActive(true);
        }
        else
        {
            if (!isTappped)
            {
                isTappped = true;
                DataModels.Instance.GetCountQueues();
            }
        }

    }
    public void UpdateCancelSeat(ScheduledTransaction model)
    {
        if (model != null)
        {
            driver.interactable = false;

            DataModels.Instance.DriversId = model.DriversId;
            if (model.FrontSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FrontSeat1Name))
                {
                    canceldriverarea2.interactable = true;
                }
                else
                {
                    canceldriverarea2.interactable = false;
                }

                UpdateButtonText(canceldriverarea2, model.FrontSeat1Name);
            }
            else
            {
                canceldriverarea2.interactable = false;
                UpdateButtonText(driverarea2, Contants.Seat3);
            }

            if (model.FrontSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FrontSeat2Name))
                {
                    canceldriverarea3.interactable = true;
                }
                else
                {
                    canceldriverarea3.interactable = false;
                }

                UpdateButtonText(canceldriverarea3, model.FrontSeat2Name);
            }
            else
            {
                canceldriverarea3.interactable = false;
                UpdateButtonText(canceldriverarea3, Contants.Seat4);

            }

            if (model.FirstSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat1Name))
                {
                    cancelfirstseat1.interactable = true;
                }
                else
                {
                    cancelfirstseat1.interactable = false;
                }

                UpdateButtonText(cancelfirstseat1, model.FirstSeat1Name);
            }
            else
            {
                cancelfirstseat1.interactable = false;
                UpdateButtonText(cancelfirstseat1, Contants.Seat1);
            }

            if (model.FirstSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat2Name))
                {
                    cancelfirstseat2.interactable = true;
                }
                else
                {
                    cancelfirstseat2.interactable = false;
                }
                UpdateButtonText(cancelfirstseat2, model.FirstSeat2Name);
            }
            else
            {
                cancelfirstseat2.interactable = false;
                UpdateButtonText(cancelfirstseat2, Contants.Seat2);
            }

            if (model.FirstSeat3 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat3Name))
                {
                    cancelfirstseat3.interactable = true;
                }
                else
                {
                    cancelfirstseat3.interactable = false;
                }
                UpdateButtonText(cancelfirstseat3, model.FirstSeat3Name);
            }
            else
            {
                cancelfirstseat3.interactable = false;
                UpdateButtonText(firstseat3, Contants.Seat3);
            }

            if (model.FirstSeat4 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat4Name))
                {
                    cancelfirstseat4.interactable = true;
                }
                else
                {
                    cancelfirstseat4.interactable = false;
                }
                UpdateButtonText(cancelfirstseat4, model.FirstSeat4Name);
            }
            else
            {
                cancelfirstseat4.interactable = false;
                UpdateButtonText(cancelfirstseat4, Contants.Seat4);
            }

            if (model.SecondSeat1 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat1Name))
                {
                    cancelsecondseat1.interactable = true;
                }
                else
                {
                    cancelsecondseat1.interactable = false;
                }

                UpdateButtonText(cancelsecondseat1, model.SecondSeat1Name);
            }
            else
            {
                cancelsecondseat1.interactable = false;
                UpdateButtonText(cancelsecondseat1, Contants.Seat1);
            }

            if (model.SecondSeat2 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat2Name))
                {
                    cancelsecondseat2.interactable = true;
                }
                else
                {
                    cancelsecondseat2.interactable = false;
                }
                UpdateButtonText(cancelsecondseat2, model.SecondSeat2Name);
            }
            else
            {
                cancelsecondseat2.interactable = false;
                UpdateButtonText(cancelsecondseat2, Contants.Seat2);
            }

            if (model.SecondSeat3 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat3Name))
                {
                    cancelsecondseat3.interactable = true;
                }
                else
                {
                    cancelsecondseat3.interactable = false;
                }
                UpdateButtonText(cancelsecondseat3, model.SecondSeat3Name);
            }
            else
            {
                cancelsecondseat3.interactable = false;
                UpdateButtonText(cancelsecondseat3, Contants.Seat3);
            }

            if (model.SecondSeat4 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat4Name))
                {
                    cancelsecondseat4.interactable = true;
                }
                else
                {
                    cancelsecondseat4.interactable = false;
                }
                UpdateButtonText(cancelsecondseat4, model.SecondSeat4Name);
            }
            else
            {
                cancelsecondseat4.interactable = false;
                UpdateButtonText(cancelsecondseat4, Contants.Seat4);
            }

            if (model.ThirdSeat1 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat1Name))
                {
                    cancelthirdseat1.interactable = true;
                }
                else
                {
                    cancelthirdseat1.interactable = false;
                }

                UpdateButtonText(cancelthirdseat1, model.ThirdSeat1Name);
            }
            else
            {
                cancelthirdseat1.interactable = false;
                UpdateButtonText(cancelthirdseat1, Contants.Seat1);
            }

            if (model.ThirdSeat2 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat2Name))
                {
                    cancelthirdseat2.interactable = true;
                }
                else
                {
                    cancelthirdseat2.interactable = false;
                }
                UpdateButtonText(cancelthirdseat2, model.ThirdSeat2Name);
            }
            else
            {
                cancelthirdseat2.interactable = false;
                UpdateButtonText(cancelthirdseat2, Contants.Seat2);
            }

            if (model.ThirdSeat3 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat3Name))
                {
                    cancelthirdseat3.interactable = true;
                }
                else
                {
                    cancelthirdseat3.interactable = false;
                }
                UpdateButtonText(cancelthirdseat3, model.ThirdSeat3Name);
            }
            else
            {
                cancelthirdseat3.interactable = false;
                UpdateButtonText(cancelthirdseat3, Contants.Seat3);
            }

            if (model.ThirdSeat4 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat4Name))
                {
                    cancelthirdseat4.interactable = true;
                }
                else
                {
                    cancelthirdseat4.interactable = false;
                }
                UpdateButtonText(cancelthirdseat4, model.ThirdSeat4Name);
            }
            else
            {
                cancelthirdseat4.interactable = false;
                UpdateButtonText(cancelthirdseat4, Contants.Seat4);
            }


            if (model.FourthSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat1Name))
                {
                    cancelLastseat1.interactable = true;
                }
                else
                {
                    cancelLastseat1.interactable = false;
                }
                UpdateButtonText(cancelLastseat1, model.FourthSeat1Name);
            }
            else
            {
                cancelLastseat1.interactable = false;
                UpdateButtonText(cancelLastseat1, Contants.Seat1);
            }

            if (model.FourthSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat2Name))
                {
                    cancelLastseat2.interactable = true;
                }
                else
                {
                    cancelLastseat2.interactable = false;
                }
                UpdateButtonText(cancelLastseat2, model.FourthSeat2Name);
            }
            else
            {
                cancelLastseat2.interactable = false;
                UpdateButtonText(cancelLastseat2, Contants.Seat2);
            }

            if (model.FourthSeat3 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat3Name))
                {
                    cancelLastseat3.interactable = true;
                }
                else
                {
                    cancelLastseat3.interactable = false;
                }
                UpdateButtonText(cancelLastseat3, model.FourthSeat3Name);
            }
            else
            {
                cancelLastseat3.interactable = false;
                UpdateButtonText(cancelLastseat3, Contants.Seat3);
            }

            if (model.FourthSeat4 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat4Name))
                {
                    cancelLastseat4.interactable = true;
                }
                else
                {
                    cancelLastseat4.interactable = false;
                }
                UpdateButtonText(cancelLastseat4, model.FourthSeat4Name);
            }
            else
            {
                cancelLastseat4.interactable = false;
                UpdateButtonText(cancelLastseat4, Contants.Seat4);
            }
        }
    }


    private void OnGetPassengerSeatScheduleChanged(ScheduledTransaction transaction)
    {
        UpdateCancelSeat(transaction);
        cancelseatlistView.gameObject.SetActive(true);
    }

    public void cancelViewBackTapped()
    {
        cancelseatlistView.gameObject.SetActive(false);
    }

    public void cancelViewCancelTapped()
    {
        cancelseatlistView.gameObject.SetActive(false);

    }


    public void cancelbookedTap()
    {
        string formattedSeats = string.Join(", ", cancelseats.Keys);
        cancelmessage3.text = $"Your about to cancel a book on seat {formattedSeats}";
        cancelbookedobjt.gameObject.SetActive(true);
    }

    public void cancelbookedYesTap()
    {
        cancelbookedobjt.gameObject.SetActive(false);
        bookedobjt.gameObject.SetActive(false);
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.UpdateCancelSchedule(cancelseats);
    }

    public void AddtoCancelUpdateSeats(string seatNumber)
    {

        if (!cancelseats.ContainsKey(seatNumber))
        {
            cancelseats.Add(seatNumber, 0);
        }
        else
        {
            cancelseats.Remove(seatNumber);
        }
    }

    public void EditTapped(string type)
    {

        headerMessage.text = $"{type}";
        accountSettingsVIew.gameObject.SetActive(true);
    }

    private void UpdateButtonText(Button button, string name)
    {
        if (button != null)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                buttonText.text = name;
                Debug.Log($"Button text updated to '{buttonText.text}'");
            }
        }
    }

    private void OnAddQueue(int obj)
    {
        modalspinner.gameObject.SetActive(false);
        DataModels.Instance.ProcessScheduleTransactions(obj);
    }

    private void OnDriverGetSchedule(List<ScheduledTransaction> model)
    {
        modalspinner.gameObject.SetActive(false);
        if (model != null)
        {
            UpdateSeat(model.FirstOrDefault());
            seat.gameObject.SetActive(true);
            seatstatus.gameObject.SetActive(false);
            bookedobjt.gameObject.SetActive(false);
            seats.Clear();
        }
    }

    public void SeatStatusTapped()
    {
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.GetSeatSchedule(Context.DriversId);
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
    public void cancelMenuTapped()
    {
        DataModels.Instance.GetPassengerSeatSchedule(Context.DriversId);

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
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.UpdateQueues(Context.DriversId, seats);

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

            if (model.FrontSeat1 == 1)
            {
                UpdateButtonText(driverarea2, model.FrontSeat1Name);
            }
            else
            {
                UpdateButtonText(driverarea2, Contants.Seat3);
            }

            if (model.FrontSeat2 == 1)
            {
                UpdateButtonText(driverarea3, model.FrontSeat2Name);
            }
            else
            {
                UpdateButtonText(driverarea3, Contants.Seat4);

            }

            if (model.FirstSeat1 == 1)
            {
                UpdateButtonText(firstseat1, model.FirstSeat1Name);
            }
            else
            {
                UpdateButtonText(firstseat1, Contants.Seat1);
            }

            if (model.FirstSeat2 == 1)
            {
                UpdateButtonText(firstseat2, model.FirstSeat2Name);
            }
            else
            {
                UpdateButtonText(firstseat2, Contants.Seat2);
            }

            if (model.FirstSeat3 == 1)
            {
                UpdateButtonText(firstseat3, model.FirstSeat3Name);
            }
            else
            {
                UpdateButtonText(firstseat3, Contants.Seat3);
            }

            if (model.FirstSeat4 == 1)
            {
                UpdateButtonText(firstseat4, model.FirstSeat4Name);
            }
            else
            {
                UpdateButtonText(firstseat4, Contants.Seat4);
            }

            if (model.SecondSeat1 == 1)
            {
                UpdateButtonText(secondseat1, model.SecondSeat1Name);
            }
            else
            {
                UpdateButtonText(secondseat1, Contants.Seat1);
            }

            if (model.SecondSeat2 == 1)
            {
                UpdateButtonText(secondseat2, model.SecondSeat2Name);
            }
            else
            {
                UpdateButtonText(secondseat2, Contants.Seat2);
            }

            if (model.SecondSeat3 == 1)
            {
                UpdateButtonText(secondseat3, model.SecondSeat3Name);
            }
            else
            {
                UpdateButtonText(secondseat3, Contants.Seat3);
            }

            if (model.SecondSeat4 == 1)
            {
                UpdateButtonText(secondseat4, model.SecondSeat4Name);
            }
            else
            {
                UpdateButtonText(secondseat4, Contants.Seat4);
            }

            if (model.ThirdSeat1 == 1)
            {
                UpdateButtonText(thirdseat1, model.ThirdSeat1Name);
            }
            else
            {
                UpdateButtonText(thirdseat1, Contants.Seat1);
            }

            if (model.ThirdSeat2 == 1)
            {
                UpdateButtonText(thirdseat2, model.ThirdSeat2Name);
            }
            else
            {
                UpdateButtonText(thirdseat2, Contants.Seat2);
            }

            if (model.ThirdSeat3 == 1)
            {
                UpdateButtonText(thirdseat3, model.ThirdSeat3Name);
            }
            else
            {
                UpdateButtonText(thirdseat3, Contants.Seat3);
            }

            if (model.ThirdSeat4 == 1)
            {
                UpdateButtonText(thirdseat4, model.ThirdSeat4Name);
            }
            else
            {
                UpdateButtonText(thirdseat4, Contants.Seat4);
            }


            if (model.FourthSeat1 == 1)
            {
                UpdateButtonText(Lastseat1, model.FourthSeat1Name);
            }
            else
            {
                UpdateButtonText(Lastseat1, Contants.Seat1);
            }

            if (model.FourthSeat2 == 1)
            {
                UpdateButtonText(Lastseat2, model.FourthSeat2Name);
            }
            else
            {
                UpdateButtonText(Lastseat2, Contants.Seat2);
            }

            if (model.FourthSeat3 == 1)
            {
                UpdateButtonText(Lastseat3, model.FourthSeat3Name);
            }
            else
            {
                UpdateButtonText(Lastseat3, Contants.Seat3);
            }

            if (model.FourthSeat4 == 1)
            {
                UpdateButtonText(Lastseat4, model.FourthSeat4Name);
            }
            else
            {
                UpdateButtonText(Lastseat4, Contants.Seat4);
            }
        }
    }

    private void OnCountScheduleChanged(int obj)
    {
        modalspinner.gameObject.SetActive(false);
        ModalMessage2.gameObject.SetActive(false);
        ModalAddSchedule.gameObject.SetActive(true);
        var total = obj + 1;
        count.text = total.ToString();
    }

    private void OnUpdateScheduleChanged(bool onupdate)
    {
        modalspinner.gameObject.SetActive(false);
        if (onupdate)
        {
            scheduleGameObject.gameObject.SetActive(true);
        }

        PopulateList();
    }

    void PopulateList()
    {
        int count = 0;

        RectTransform contentTransform = scrollView.content;

        GameObject templateObject = contentTransform.Find("Image").gameObject;
        templateObject.SetActive(false);

        var queueList = DataModels.Instance.Queue.Where(x => x.DriversId == Context.DriversId && x.Status == 1);
        bool isFirstItem = true;

        foreach (var data in queueList)
        {
            GameObject listItem;

            if (isFirstItem)
            {
                listItem = listItemPrefab;
                isFirstItem = false;
            }
            else
            {
                listItem = Instantiate(listItemPrefab, contentTransform);
            }

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
                    driverIdText.text = data.Id.ToString() + ";" + data.SchedId.ToString();
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

            listItem.SetActive(true);
        }

    }

    public void CheckIfExistNoTapped()
    {
        messageExist.gameObject.SetActive(false);
    }

    public void scheduleBack()
    {
        scheduleGameObject.gameObject.SetActive(false);
    }

    public void ScheduleTapped()
    {
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.GetQueues(true);
    }

    private void OnsheduleChanged(bool obj)
    {
        modalspinner.gameObject.SetActive(false);
        DataModels.Instance.GetQueues(false);
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
        seatstatus.gameObject.SetActive(false);
    }

    public void YesTapped()
    {
    }

    public void cancelChangepasswordTapped()
    {
        changepassView.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void show()
    {
        name.text = Context.firstname + " " + Context.lastname;
        ModalAddSchedule.gameObject.SetActive(false);
        canvasMenu.gameObject.SetActive(true);
        seat.gameObject.SetActive(false);
        seatstatus.gameObject.SetActive(false);
        bookedobjt.gameObject.SetActive(false);
        changepassView.gameObject.SetActive(false);

        first.text = Context.firstname;
        last.text = Context.lastname;
        address.text = Context.Address;
        birth.text = Context.Birth;
        contact.text = Context.ContactNumber;

        username.text = Context.username;
        password.text = Context.Password;
        nameheader.text = Context.firstname+" "+Context.lastname;
    }

    public void changepasswordTapped()
    {
        changepass.gameObject.SetActive(true);
    }

    public void AccountChangepasswordTapped()
    {
        changepassView.gameObject.SetActive(true);

    }

    public void AddBookingsTapped()
    {
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.CheckIfQueuesExist(Context.DriversId);
    }

    public void GotoSeat(TMP_Text QueueId)
    {
        var data = QueueId.text.ToString().Split(';');
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.GetDriverSchedule(Context.DriversId, data[0], data[1]);
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
            DataModels.Instance.OnAddQueue -= OnAddQueue;
            DataModels.Instance.OnCheckExist -= OnCheckExist;
            DataModels.Instance.OnGetSeatSchedule -= OnGetSeatSchedule;
            DataModels.Instance.OnGetPassengerSeatSchedule -= OnGetPassengerSeatScheduleChanged;

        }
    }

    public void LogoutTapped()
    {
        SceneManager.LoadScene("LoginScene");
        SceneManager.UnloadSceneAsync("Driver");
    }

    public void SaveTapped()
    {
        DataModels.Instance.CreateQueues(count.text);
    }

    public void BackCancelBookingTapped()
    {


    }

    public void cancelmodalYes()
    {
        cancelbookedobjt.gameObject.SetActive(false);
    }
}
