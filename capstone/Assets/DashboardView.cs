using Newtonsoft.Json;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DashboardView : MonoBehaviour
{
    [SerializeField] private GameObject Register;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject Modal;
    [SerializeField] private TMP_Text message;
    [SerializeField] private TMP_Text message3;
    [SerializeField] private TMP_Text message2;

    [SerializeField] private TMP_InputField firstname;
    [SerializeField] private TMP_InputField lastname;
    [SerializeField] private TMP_InputField address;
    [SerializeField] private TMP_InputField contactnumber;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_Text date;
    [SerializeField] private TMP_InputField PlateNumber;
    [SerializeField] private TMP_InputField DriversLicenseNumber;
    [SerializeField] private TMP_Text name;

    void Start()
    {
        Register.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        name.text = Context.firstname + "  " + Context.lastname;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveTapped()
    {
        if (string.IsNullOrEmpty(address.text) || string.IsNullOrEmpty(firstname.text) || string.IsNullOrEmpty(lastname.text))
        {
            message.text = "address, firstname or lastname is empty.";
            Modal.gameObject.SetActive(true);
            return;
        }

        StartCoroutine(Registation());
    }

    public void LogoutTapped()
    {
        SceneManager.LoadScene("LoginScene");
        SceneManager.UnloadSceneAsync("Dashboard");
    }

    public void AddDriverTapped()
    {
        Register.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }

    public void BackTapped()
    {
        Register.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    IEnumerator Registation()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", string.Empty);
        form.AddField("password", string.Empty);
        form.AddField("firstname", firstname.text);
        form.AddField("lastname", lastname.text);
        form.AddField("typeofaccount", 3);
        form.AddField("BirthDate", date.text);
        form.AddField("Address", address.text);
        form.AddField("IsResign", 0);
        form.AddField("IsBlocked", 0);
        form.AddField("ContactNumber", contactnumber.text);
        form.AddField("Email", email.text);
        form.AddField("isDriver", 1);
        form.AddField("PlateNumber", PlateNumber.text);
        form.AddField("DriversLicenseNumber", DriversLicenseNumber.text);

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
                    message.text = "User successfully registered.";
                    message2.text = $"User Name:  {response.Username}";
                    message3.text = $"Password :  {response.Password}";
                    Modal.gameObject.SetActive(true);


                    firstname.text = string.Empty;
                    lastname.text = string.Empty;
                    address.text = string.Empty;
                }
                else
                {
                    message.text = "User registration failed.";
                    Modal.gameObject.SetActive(true);
                }
            }
            catch (Exception ex)
            {
                message.text = ex.Message;
                Modal.gameObject.SetActive(true);
            }

        }
    }

    public void AddBookingTapped()
    {
        //var model = new DataModels();
        //model.ProcessScheduleTransactions();
    }
}
