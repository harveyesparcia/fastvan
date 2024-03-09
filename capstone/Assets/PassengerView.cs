using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PassengerView : MonoBehaviour
{

    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private TMP_Text name;

    void Start()
    {
        if (Context.IsLogin)
        {
            show();
        }
    }

    private void show()
    {
        name.text = Context.firstname + "  " + Context.lastname;
        canvasMenu.gameObject.SetActive(true);
        DataModels.Instance.GetQueues();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LogoutTapped()
    {
        SceneManager.LoadScene("LoginScene");
        SceneManager.UnloadSceneAsync("Passenger");
    }

    public void AddBookingsTapped()
    {
        StartCoroutine(Update_ScheduledTransactions());
    }

    IEnumerator Update_ScheduledTransactions()
    {
        WWWForm form = new WWWForm();
        form.AddField("DriversId", Context.DriversId);

        using UnityWebRequest request = UnityWebRequest.Post("http://www.aasimudin.cctc-ccs.net/Api/update_scheduledtransactions.php", form);

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

