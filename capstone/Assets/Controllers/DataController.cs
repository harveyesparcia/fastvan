using UnityEngine;

public class DataController : MonoBehaviour
{
    private UserModel userModel; 
    private float fetchTimer = 60f;

    void Start()
    {
       // StartCoroutine(GetTransactions());

        InvokeRepeating("FetchData", 0f, fetchTimer);
    }


    void FetchData()
    {
       // StartCoroutine(FetchDataFromEndpoint());
    }

}
