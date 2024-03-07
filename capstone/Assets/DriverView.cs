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
        StartCoroutine(Create_ScheduledTransactions());
    }

    IEnumerator Create_ScheduledTransactions()
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
