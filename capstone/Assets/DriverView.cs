using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DriverView : MonoBehaviour
{
    [SerializeField] private GameObject ModalAddSchedule;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject ModalMEssage;
    [SerializeField] private TMP_Text count;
    [SerializeField] private GameObject ModalMessage2;
    [SerializeField] private TMP_Text message2;
    [SerializeField] private GameObject scheduleGameObject;

    public ScrollRect scrollView;
    public GameObject listItemPrefab;

    void Start()
    {
        if (Context.IsLogin)
        {
            show();
            DataModels.Instance.OnAddSchedule += OnsheduleChanged;
            DataModels.Instance.OnUpdateSchedule += OnUpdateScheduleChanged;
        }
    }

    private void OnUpdateScheduleChanged()
    {
        PopulateList();
        DataModels.Instance.OnUpdateSchedule -= OnUpdateScheduleChanged;
    }

    void PopulateList()
    {
        RectTransform contentTransform = scrollView.content;

        foreach (var data in DataModels.Instance.Queue)
        {
            GameObject listItem = Instantiate(listItemPrefab, contentTransform);

            TMP_Text itemText = listItem.GetComponentInChildren<TMP_Text>();

            Transform driverIdTransform = listItem.transform.Find("DriversNameValue");
            Transform plateNumberTransform = listItem.transform.Find("PlateNumber");
            Transform totalPassengerform = listItem.transform.Find("TotalPassenger");
            Transform maxCapacityform = listItem.transform.Find("MaxCapacity");

            if (driverIdTransform != null)
            {
                TMP_Text driverIdText = driverIdTransform.GetComponent<TMP_Text>();
                if (driverIdText != null)
                {
                    driverIdText.text = data.DriversId.ToString();
                }
            }

            if (maxCapacityform != null)
            {
                TMP_Text maxCapacityText = maxCapacityform.GetComponent<TMP_Text>();
                if (maxCapacityText != null)
                {
                    maxCapacityText.text = "24";
                }
            }

            if (plateNumberTransform != null)
            {
                TMP_Text plateNumberText = plateNumberTransform.GetComponent<TMP_Text>();
                if (plateNumberText != null)
                {
                    plateNumberText.text = "JKK 445";
                }
            }

            if (totalPassengerform != null)
            {
                TMP_Text totalPassengerText = totalPassengerform.GetComponent<TMP_Text>();
                if (totalPassengerText != null)
                {
                    totalPassengerText.text = "10";
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);
        }

        GameObject templateObject = contentTransform.Find("Image").gameObject;
        templateObject.SetActive(false);
    }



    public void ScheduleTapped()
    {
        scheduleGameObject.gameObject.SetActive(true);

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
        ModalAddSchedule.gameObject.SetActive(false);
        canvasMenu.gameObject.SetActive(true);
        DataModels.Instance.GetQueues();
    }

    public void AddBookingsTapped()
    {
        ModalMessage2.gameObject.SetActive(false);
        ModalAddSchedule.gameObject.SetActive(true);
        var total = DataModels.Instance.CurrentQueue + 1;
        count.text = total.ToString();
    }

    private void OnDestroy()
    {
        if (Context.IsLogin && DataModels.Instance != null)
        {
            DataModels.Instance.OnAddSchedule -= OnsheduleChanged;
            DataModels.Instance.OnUpdateSchedule -= OnUpdateScheduleChanged;
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
        DataModels.Instance.ProcessScheduleTransactions();
    }



}
