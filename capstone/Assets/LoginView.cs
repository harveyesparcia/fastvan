using Newtonsoft.Json;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginView
    : MonoBehaviour
{
    [SerializeField] private DataController dataController;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private Button ButtonSubmit;
    [SerializeField] private GameObject Modal;
    [SerializeField] private Button ButtonClosed;
    [SerializeField] private GameObject Register;
    [SerializeField] private GameObject Login;
    [SerializeField] private GameObject modalMessage2;
    [SerializeField] private TMP_Text message;


    [SerializeField] private TMP_InputField firstname;
    [SerializeField] private TMP_InputField lastname;
    [SerializeField] private TMP_InputField address;
    [SerializeField] private TMP_InputField newusername;
    [SerializeField] private TMP_InputField newpassword;
    [SerializeField] private TMP_InputField contactnumber;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField date;
    [SerializeField] private GameObject modalspinner;


    private bool isScene1Active = true;

    void Start()
    {
        Login.gameObject.SetActive(true);
        Register.gameObject.SetActive(false);
        Modal.gameObject.SetActive(false);
        modalMessage2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {

    }

    public void NoTapped()
    {
        Login.gameObject.SetActive(false);
        Register.gameObject.SetActive(true);
        modalMessage2.gameObject.SetActive(false);
    }

    public void LoggindTapped()
    {
        if (string.IsNullOrEmpty(input.text) || string.IsNullOrEmpty(password.text))
        {
            message.text = "Username and password cannot be empty.";
            Modal.gameObject.SetActive(true);
            return;
        }

        modalspinner.gameObject.SetActive(true);
        StartCoroutine(Authentication(input.text, password.text));
    }

    public void confirmationTapped()
    {
        modalMessage2.SetActive(true);
    }
    public void SaveTapped()
    {

        if (string.IsNullOrEmpty(address.text) || string.IsNullOrEmpty(firstname.text) || string.IsNullOrEmpty(lastname.text))
        {
            message.text = "address, firstname or lastname is empty.";
            Modal.gameObject.SetActive(true);
            modalMessage2.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(Registration());
        modalMessage2.gameObject.SetActive(false);
    }

    public void ClosedTapped()
    {
        Modal.gameObject.SetActive(false);
    }

    public void RegisterTapped()
    {
        Login.gameObject.SetActive(false);
        Register.gameObject.SetActive(true);
    }

    public void GobackTapped()
    {
        Login.gameObject.SetActive(true);
        Register.gameObject.SetActive(false);
    }

    IEnumerator Authentication(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/login.php", form);

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
                if (response.Role != null)
                {
                    Context.IsLogin = true;
                    Context.DriversId = response.DriversId;

                    Context.lastname = response.LastName;
                    Context.firstname = response.FirstName;
                    Context.Address = response.Address;
                    Context.Birth = response.BirthDate;
                    Context.username = response.Username;
                    Context.Password = response.Password;
                    Context.ContactNumber = response.Contactnumber;

                    DataModels.Instance.GetQueues(false);
                    if (response.Role.Contains("Admin"))
                    {
                        SceneManager.LoadScene("DashBoard");
                    }

                    if (response.Role.Contains("Driver"))
                    {
                        SceneManager.LoadScene("Driver");

                    }

                    if (response.Role.Contains("Passenger"))
                    {

                        SceneManager.LoadScene("Passenger");
                    }
                }
                else
                {
                    Context.IsLogin = false;
                    message.text = "User is not registered";
                    Modal.gameObject.SetActive(true);
                    modalspinner.gameObject.SetActive(false);
                }
            }
            catch (Exception ex)
            {
                Context.IsLogin = false;
                message.text = ex.Message;
                Modal.gameObject.SetActive(true);
                modalspinner.gameObject.SetActive(false);
            }

        }
    }

    IEnumerator Registration()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", newusername.text);
        form.AddField("password", newpassword.text);
        form.AddField("firstname", firstname.text);
        form.AddField("lastname", lastname.text);
        form.AddField("typeofaccount", 2);
        form.AddField("BirthDate", date.text);
        form.AddField("Address", address.text);
        form.AddField("IsResign", 0);
        form.AddField("IsBlocked", 0);
        form.AddField("ContactNumber", contactnumber.text);
        form.AddField("Email", email.text);
        form.AddField("isDriver", 0);

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
                var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
                Debug.Log("Response: " + jsonResponse);
                if (response.status.Contains("success"))
                {
                    message.text = "User successfully registered.";
                    Modal.gameObject.SetActive(true);


                    newusername.text = string.Empty;
                    newpassword.text = string.Empty;
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

}
