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

    public TMP_Dropdown hourDropdown;
    public TMP_Dropdown minuteDropdown;
    public TMP_Dropdown amPMDropdown;

    private List<TMP_Text> hoursList = new List<TMP_Text>();
    private List<TMP_Text> minutesList = new List<TMP_Text>();
    private List<TMP_Text> amPMList = new List<TMP_Text>();

    void Start()
    {
        if (Context.IsLogin)
        {
            show();

            for (int i = 0; i < 24; i++)
            {
                TMP_Text text = new GameObject().AddComponent<TextMeshProUGUI>();
                text.text = i.ToString("00");
                hoursList.Add(text);
            }

            for (int i = 0; i < 60; i++)
            {
                TMP_Text text = new GameObject().AddComponent<TextMeshProUGUI>();
                text.text = i.ToString("00");
                minutesList.Add(text);
            }

            amPMList.Add(CreateTMPText("AM"));
            amPMList.Add(CreateTMPText("PM"));

            // Add TMP_Text options to TMP_Dropdowns
            hourDropdown.AddOptions(hoursList.ConvertAll(option => new TMP_Dropdown.OptionData(option.text)));
            minuteDropdown.AddOptions(minutesList.ConvertAll(option => new TMP_Dropdown.OptionData(option.text)));
        }
    }

    private TMP_Text CreateTMPText(string text)
    {
        TMP_Text tmpText = new GameObject().AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        return tmpText;
    }

    public void AddTapped()
    {
        ModalMEssage.gameObject.SetActive(true);
    }

    public void OnDropdownValueChanged()
    {
        int selectedHour = int.Parse(hoursList[hourDropdown.value].text);
        int selectedMinute = int.Parse(minutesList[minuteDropdown.value].text);
        string selectedAmPm = amPMList[amPMDropdown.value].text;

        Debug.Log("Selected Time: " + selectedHour + ":" + selectedMinute + " " + selectedAmPm);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void show()
    {
        ModalAddSchedule.gameObject.SetActive(false);
        canvasMenu.gameObject.SetActive(true);
    }

    public void AddBookingsTapped()
    {
        ModalAddSchedule.gameObject.SetActive(true);
    }

    public void LogoutTapped()
    {
        SceneManager.LoadScene("LoginScene");
        SceneManager.UnloadSceneAsync("Driver");
    }

    public void SaveTapped()
    {
        var model = new DataModels();
        model.ProcessScheduleTransactions();
    }

   

}
