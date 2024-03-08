using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    void Start()
    {
        if (Context.IsLogin)
        {
            show();
            DataModels.Instance.OnAddSchedule += OnsheduleChanged;
        }
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
