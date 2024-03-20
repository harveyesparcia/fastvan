using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
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
    [SerializeField] private TMP_Text cancelmessage3;
    [SerializeField] private GameObject bookedobjt;
    [SerializeField] private GameObject cancelbookedobjt;
    [SerializeField] private GameObject changepassView;
    [SerializeField] private GameObject cancelBooking;

    [SerializeField] private GameObject accountSettingsVIew;
    [SerializeField] private TMP_Text headerMessage;

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
    [SerializeField] private GameObject templateObject;

    [SerializeField] private GameObject changepass;
    [SerializeField] private TMP_Text first;
    [SerializeField] private TMP_Text last;
    [SerializeField] private TMP_Text contact;
    [SerializeField] private TMP_Text address;
    [SerializeField] private TMP_Text birth;
    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text password;

    [SerializeField] private GameObject modalCheckPassengerExist;

    private static Dictionary<string, int> seats = new Dictionary<string, int>();
    private static Dictionary<string, int> cancelseats = new Dictionary<string, int>();

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

    void Start()
    {
        if (Context.IsLogin)
        {
            show();
            DataModels.Instance.OnAddSchedule += OnsheduleChanged;
            DataModels.Instance.OnUpdateSchedule += OnUpdateScheduleChanged;
            DataModels.Instance.OnCountSchedule += OnCountScheduleChanged;
            DataModels.Instance.OnDriverGetSchedule += OnDriverGetSchedule;
            DataModels.Instance.OnAddQueue += OnAddQueue;
            DataModels.Instance.OnPassengerCheckExist += OnPassengerCheckExistChanged;
        }
    }

    public void okaybuttonTapped()
    {
        modalCheckPassengerExist.gameObject.SetActive(false);   
    }

    public void cancelMenuTapped()
    {
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

    private void OnPassengerCheckExistChanged(CheckPassengerExistResponse obj)
    {
        if (obj != null)
        {
            modalspinner.gameObject.SetActive(false);
            if (string.IsNullOrEmpty(obj.driversId))
            {
                DataModels.Instance.UpdateQueues(DataModels.Instance.DriversId, seats);
                return;
            }
            if (obj.driversId != DataModels.Instance.DriversId)
            {
                modalCheckPassengerExist.gameObject.SetActive(true);
            }
            else
            {
                DataModels.Instance.UpdateQueues(DataModels.Instance.DriversId, seats);
            }
        }

    }

    public void EditBackTapped()
    {
        accountSettingsVIew.gameObject.SetActive(false);
        changepassView.gameObject.SetActive(false);
       
    }

    public void EditTapped(string type)
    {

        headerMessage.text = $"Edit {type}";
        accountSettingsVIew.gameObject.SetActive(true);
    }

    private void OnAddQueue(int obj)
    {
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.ProcessScheduleTransactions(obj);
    }

    public void accountBackTapped()
    {
        changepass.gameObject.SetActive(false);
    }

    private void OnDriverGetSchedule(List<ScheduledTransaction> model)
    {
        modalspinner.gameObject.SetActive(false);

        if (model != null)
        {
            UpdateSeat(model.Where(x=>x.Status==1).FirstOrDefault());
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

    public void bookedTap()
    {
        string formattedSeats = string.Join(", ", seats.Keys);
        message3.text = $"Your about to book on seat {formattedSeats}";
        bookedobjt.gameObject.SetActive(true);
    }

    public void cancelbookedTap()
    {
        string formattedSeats = string.Join(", ", cancelseats.Keys);
        cancelmessage3.text = $"Your about to cancel a book on seat {formattedSeats}";
        cancelbookedobjt.gameObject.SetActive(true);
    }

    public void bookedYesTap()
    {
        bookedobjt.gameObject.SetActive(true);
        modalspinner.gameObject.SetActive(true);
       
        DataModels.Instance.CheckIfPassengerhasExistingSeat(Context.PassengerId);
    }

    private IEnumerator DisableSpinnerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        modalspinner.gameObject.SetActive(false);
    }

    public void bookedNoTap()
    {
        bookedobjt.gameObject.SetActive(false);
    }

    public void cancelChangepasswordTapped()
    {
        changepass.gameObject.SetActive(false);
        changepassView.gameObject.SetActive(false);
    }


    public void changepasswordTapped()
    {
        changepass.gameObject.SetActive(true);
    }

    public void changepasswordFromTapped()
    {
        changepassView.gameObject.SetActive(true);
    }

    public void UpdateSeat(ScheduledTransaction model)
    {
        if (model != null)
        {
            driver.interactable = false;

            DataModels.Instance.DriversId = model.DriversId;
            if (model.FrontSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FrontSeat1Name))
                {
                    driverarea2.interactable = true;
                }
                else
                {
                    driverarea2.interactable = false;
                }
               
                UpdateButtonText(driverarea2, model.FrontSeat1Name);
            }
            else
            {
                driverarea2.interactable = true;
                UpdateButtonText(driverarea2, Contants.Seat3);
            }

            if (model.FrontSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FrontSeat2Name))
                {
                    driverarea3.interactable = true;
                }
                else
                {
                    driverarea3.interactable = false;
                }

                UpdateButtonText(driverarea3, model.FrontSeat2Name);
            }
            else
            {
                driverarea3.interactable = true;
                UpdateButtonText(driverarea3, Contants.Seat4);

            }

            if (model.FirstSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat1Name))
                {
                    firstseat1.interactable = true;
                }
                else
                {
                    firstseat1.interactable = false;
                }

                UpdateButtonText(firstseat1, model.FirstSeat1Name);
            }
            else
            {
                firstseat1.interactable = true;
                UpdateButtonText(firstseat1, Contants.Seat1);
            }

            if (model.FirstSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat2Name))
                {
                    firstseat2.interactable = true;
                }
                else
                {
                    firstseat2.interactable = false;
                }
                UpdateButtonText(firstseat2, model.FirstSeat2Name);
            }
            else
            {
                firstseat2.interactable = true;
                UpdateButtonText(firstseat2, Contants.Seat2);
            }

            if (model.FirstSeat3 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat2Name))
                {
                    firstseat3.interactable = true;
                }
                else
                {
                    firstseat3.interactable = false;
                }
                UpdateButtonText(firstseat3, model.FirstSeat3Name);
            }
            else
            {
                firstseat3.interactable = true;
                UpdateButtonText(firstseat3, Contants.Seat3);
            }

            if (model.FirstSeat4 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat4Name))
                {
                    firstseat4.interactable = true;
                }
                else
                {
                    firstseat4.interactable = false;
                }
                UpdateButtonText(firstseat4, model.FirstSeat4Name);
            }
            else
            {
                firstseat4.interactable = true;
                UpdateButtonText(firstseat4, Contants.Seat4);
            }

            if (model.SecondSeat1 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat1Name))
                {
                    secondseat1.interactable = true;
                }
                else
                {
                    secondseat1.interactable = false;
                }

                UpdateButtonText(secondseat1, model.SecondSeat1Name);
            }
            else
            {
                secondseat1.interactable = true;
                UpdateButtonText(secondseat1, Contants.Seat1);
            }

            if (model.SecondSeat2 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat2Name))
                {
                    secondseat2.interactable = true;
                }
                else
                {
                    secondseat2.interactable = false;
                }
                UpdateButtonText(secondseat2, model.SecondSeat2Name);
            }
            else
            {
                secondseat2.interactable = true;
                UpdateButtonText(secondseat2, Contants.Seat2);
            }

            if (model.SecondSeat3 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat3Name))
                {
                    secondseat3.interactable = true;
                }
                else
                {
                    secondseat3.interactable = false;
                }
                UpdateButtonText(secondseat3, model.SecondSeat3Name);
            }
            else
            {
                secondseat3.interactable = true;
                UpdateButtonText(secondseat3, Contants.Seat3);
            }

            if (model.SecondSeat4 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat4Name))
                {
                    secondseat4.interactable = true;
                }
                else
                {
                    secondseat4.interactable = false;
                }
                UpdateButtonText(secondseat4, model.SecondSeat4Name);
            }
            else
            {
                secondseat4.interactable = true;
                UpdateButtonText(secondseat4, Contants.Seat4);
            }

            if (model.ThirdSeat1 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat1Name))
                {
                    thirdseat1.interactable = true;
                }
                else
                {
                    thirdseat1.interactable = false;
                }

                UpdateButtonText(thirdseat1, model.ThirdSeat1Name);
            }
            else
            {
                thirdseat1.interactable = true;
                UpdateButtonText(thirdseat1, Contants.Seat1);
            }

            if (model.ThirdSeat2 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat2Name))
                {
                    thirdseat2.interactable = true;
                }
                else
                {
                    thirdseat2.interactable = false;
                }
                UpdateButtonText(thirdseat2, model.ThirdSeat2Name);
            }
            else
            {
                thirdseat2.interactable = true;
                UpdateButtonText(thirdseat2, Contants.Seat2);
            }

            if (model.ThirdSeat3 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat3Name))
                {
                    thirdseat3.interactable = true;
                }
                else
                {
                    thirdseat3.interactable = false;
                }
                UpdateButtonText(thirdseat3, model.ThirdSeat3Name);
            }
            else
            {
                thirdseat3.interactable = true;
                UpdateButtonText(thirdseat3, Contants.Seat3);
            }

            if (model.ThirdSeat4 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat4Name))
                {
                    thirdseat4.interactable = true;
                }
                else
                {
                    thirdseat4.interactable = false;
                }
                UpdateButtonText(thirdseat4, model.ThirdSeat4Name);
            }
            else
            {
                thirdseat4.interactable = true;
                UpdateButtonText(thirdseat4, Contants.Seat4);
            }


            if (model.FourthSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat1Name))
                {
                    Lastseat1.interactable = true;
                }
                else
                {
                    Lastseat1.interactable = false;
                }
                UpdateButtonText(Lastseat1, model.FourthSeat1Name);
            }
            else
            {
                Lastseat1.interactable = true;
                UpdateButtonText(Lastseat1, Contants.Seat1);
            }

            if (model.FourthSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat2Name))
                {
                    Lastseat2.interactable = true;
                }
                else
                {
                    Lastseat2.interactable = false;
                }
                UpdateButtonText(Lastseat2, model.FourthSeat2Name);
            }
            else
            {
                Lastseat2.interactable = true;
                UpdateButtonText(Lastseat2, Contants.Seat2);
            }

            if (model.FourthSeat3 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat3Name))
                {
                    Lastseat3.interactable = true;
                }
                else
                {
                    Lastseat3.interactable = false;
                }
                UpdateButtonText(Lastseat3, model.FourthSeat3Name);
            }
            else
            {
                Lastseat3.interactable = true;
                UpdateButtonText(Lastseat3, Contants.Seat3);
            }

            if (model.FourthSeat4 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat4Name))
                {
                    Lastseat4.interactable = true;
                }
                else
                {
                    Lastseat4.interactable = false;
                }
                UpdateButtonText(Lastseat4, model.FourthSeat4Name);
            }
            else
            {
                Lastseat4.interactable = true;
                UpdateButtonText(Lastseat4, Contants.Seat4);
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
                    driverarea2.interactable = true;
                }
                else
                {
                    driverarea2.interactable = false;
                }

                UpdateButtonText(driverarea2, model.FrontSeat1Name);
            }
            else
            {
                driverarea2.interactable = true;
                UpdateButtonText(driverarea2, Contants.Seat3);
            }

            if (model.FrontSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FrontSeat2Name))
                {
                    driverarea3.interactable = true;
                }
                else
                {
                    driverarea3.interactable = false;
                }

                UpdateButtonText(driverarea3, model.FrontSeat2Name);
            }
            else
            {
                driverarea3.interactable = true;
                UpdateButtonText(driverarea3, Contants.Seat4);

            }

            if (model.FirstSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat1Name))
                {
                    firstseat1.interactable = true;
                }
                else
                {
                    firstseat1.interactable = false;
                }

                UpdateButtonText(firstseat1, model.FirstSeat1Name);
            }
            else
            {
                firstseat1.interactable = true;
                UpdateButtonText(firstseat1, Contants.Seat1);
            }

            if (model.FirstSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat2Name))
                {
                    firstseat2.interactable = true;
                }
                else
                {
                    firstseat2.interactable = false;
                }
                UpdateButtonText(firstseat2, model.FirstSeat2Name);
            }
            else
            {
                firstseat2.interactable = true;
                UpdateButtonText(firstseat2, Contants.Seat2);
            }

            if (model.FirstSeat3 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat2Name))
                {
                    firstseat3.interactable = true;
                }
                else
                {
                    firstseat3.interactable = false;
                }
                UpdateButtonText(firstseat3, model.FirstSeat3Name);
            }
            else
            {
                firstseat3.interactable = true;
                UpdateButtonText(firstseat3, Contants.Seat3);
            }

            if (model.FirstSeat4 == 1)
            {
                if (Context.firstname.Equals(model.FirstSeat4Name))
                {
                    firstseat4.interactable = true;
                }
                else
                {
                    firstseat4.interactable = false;
                }
                UpdateButtonText(firstseat4, model.FirstSeat4Name);
            }
            else
            {
                firstseat4.interactable = true;
                UpdateButtonText(firstseat4, Contants.Seat4);
            }

            if (model.SecondSeat1 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat1Name))
                {
                    secondseat1.interactable = true;
                }
                else
                {
                    secondseat1.interactable = false;
                }

                UpdateButtonText(secondseat1, model.SecondSeat1Name);
            }
            else
            {
                secondseat1.interactable = true;
                UpdateButtonText(secondseat1, Contants.Seat1);
            }

            if (model.SecondSeat2 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat2Name))
                {
                    secondseat2.interactable = true;
                }
                else
                {
                    secondseat2.interactable = false;
                }
                UpdateButtonText(secondseat2, model.SecondSeat2Name);
            }
            else
            {
                secondseat2.interactable = true;
                UpdateButtonText(secondseat2, Contants.Seat2);
            }

            if (model.SecondSeat3 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat3Name))
                {
                    secondseat3.interactable = true;
                }
                else
                {
                    secondseat3.interactable = false;
                }
                UpdateButtonText(secondseat3, model.SecondSeat3Name);
            }
            else
            {
                secondseat3.interactable = true;
                UpdateButtonText(secondseat3, Contants.Seat3);
            }

            if (model.SecondSeat4 == 1)
            {
                if (Context.firstname.Equals(model.SecondSeat4Name))
                {
                    secondseat4.interactable = true;
                }
                else
                {
                    secondseat4.interactable = false;
                }
                UpdateButtonText(secondseat4, model.SecondSeat4Name);
            }
            else
            {
                secondseat4.interactable = true;
                UpdateButtonText(secondseat4, Contants.Seat4);
            }

            if (model.ThirdSeat1 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat1Name))
                {
                    thirdseat1.interactable = true;
                }
                else
                {
                    thirdseat1.interactable = false;
                }

                UpdateButtonText(thirdseat1, model.ThirdSeat1Name);
            }
            else
            {
                thirdseat1.interactable = true;
                UpdateButtonText(thirdseat1, Contants.Seat1);
            }

            if (model.ThirdSeat2 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat2Name))
                {
                    thirdseat2.interactable = true;
                }
                else
                {
                    thirdseat2.interactable = false;
                }
                UpdateButtonText(thirdseat2, model.ThirdSeat2Name);
            }
            else
            {
                thirdseat2.interactable = true;
                UpdateButtonText(thirdseat2, Contants.Seat2);
            }

            if (model.ThirdSeat3 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat3Name))
                {
                    thirdseat3.interactable = true;
                }
                else
                {
                    thirdseat3.interactable = false;
                }
                UpdateButtonText(thirdseat3, model.ThirdSeat3Name);
            }
            else
            {
                thirdseat3.interactable = true;
                UpdateButtonText(thirdseat3, Contants.Seat3);
            }

            if (model.ThirdSeat4 == 1)
            {
                if (Context.firstname.Equals(model.ThirdSeat4Name))
                {
                    thirdseat4.interactable = true;
                }
                else
                {
                    thirdseat4.interactable = false;
                }
                UpdateButtonText(thirdseat4, model.ThirdSeat4Name);
            }
            else
            {
                thirdseat4.interactable = true;
                UpdateButtonText(thirdseat4, Contants.Seat4);
            }


            if (model.FourthSeat1 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat1Name))
                {
                    Lastseat1.interactable = true;
                }
                else
                {
                    Lastseat1.interactable = false;
                }
                UpdateButtonText(Lastseat1, model.FourthSeat1Name);
            }
            else
            {
                Lastseat1.interactable = true;
                UpdateButtonText(Lastseat1, Contants.Seat1);
            }

            if (model.FourthSeat2 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat2Name))
                {
                    Lastseat2.interactable = true;
                }
                else
                {
                    Lastseat2.interactable = false;
                }
                UpdateButtonText(Lastseat2, model.FourthSeat2Name);
            }
            else
            {
                Lastseat2.interactable = true;
                UpdateButtonText(Lastseat2, Contants.Seat2);
            }

            if (model.FourthSeat3 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat3Name))
                {
                    Lastseat3.interactable = true;
                }
                else
                {
                    Lastseat3.interactable = false;
                }
                UpdateButtonText(Lastseat3, model.FourthSeat3Name);
            }
            else
            {
                Lastseat3.interactable = true;
                UpdateButtonText(Lastseat3, Contants.Seat3);
            }

            if (model.FourthSeat4 == 1)
            {
                if (Context.firstname.Equals(model.FourthSeat4Name))
                {
                    Lastseat4.interactable = true;
                }
                else
                {
                    Lastseat4.interactable = false;
                }
                UpdateButtonText(Lastseat4, model.FourthSeat4Name);
            }
            else
            {
                Lastseat4.interactable = true;
                UpdateButtonText(Lastseat4, Contants.Seat4);
            }
        }
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
        RectTransform contentTransform = scrollView.content;
        int count = 0;

        GameObject templateObject = contentTransform.Find("Image").gameObject;
        templateObject.SetActive(false);

        foreach (Transform child in contentTransform)
        {
            if (child.gameObject != templateObject)
            {
                Destroy(child.gameObject);
            }
        }

        var queueList = DataModels.Instance.Queue.Where(x => x.Status == 1);
        bool isFirstItem = true;

        float horizontalMargin = 10f; 
        float verticalMargin = 10f; 
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
                RectTransform listItemRectTransform = listItem.GetComponent<RectTransform>();
                listItemRectTransform.offsetMin += new Vector2(horizontalMargin, verticalMargin);
                listItemRectTransform.offsetMax -= new Vector2(horizontalMargin, verticalMargin);
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
                    queuesIdText.text = data.Id.ToString() +";"+ data.DriversId.ToString();
                }
            }

            if (driversIdform != null)
            {
                TMP_Text driverIdText = driversIdform.GetComponent<TMP_Text>();
                if (driverIdText != null)
                {
                    driverIdText.text = data.Id.ToString() + ";" + data.DriversId.ToString() +";"+ data.SchedId.ToString();
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
        changepassView.gameObject.SetActive(false);

        first.text = Context.firstname;
        last.text = Context.lastname;
        address.text = Context.Address;
        birth.text = Context.Birth;
        contact.text = Context.ContactNumber;

        username.text = Context.username;
        password.text = Context.Password;
    }

    public void AddBookingsTapped()
    {
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.GetCountQueues();
    }

    public void GotoSeat(TMP_Text QueueId)
    {
        modalspinner.gameObject.SetActive(true);
        var data = QueueId.text.ToString().Split(';');
        DataModels.Instance.GetDriverSchedule(data[1], data[0], data[2]);

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
            DataModels.Instance.OnPassengerCheckExist -= OnPassengerCheckExistChanged;
        }
    }

    public void LogoutTapped()
    {
        modalspinner.gameObject.SetActive(true);
        SceneManager.LoadScene("LoginScene");
        SceneManager.UnloadSceneAsync("Passenger");
    }

    public void SaveTapped()
    {
        modalspinner.gameObject.SetActive(true);
        DataModels.Instance.CreateQueues(count.text);
    }

    public void BackCancelBookingTapped() { 
    

    }

    public void cancelmodalYes()
    { 
        cancelbookedobjt.gameObject.SetActive(false);
    }

}
