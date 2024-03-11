using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ScheduledTransaction
{
    [JsonProperty("DriversId")]
    public string DriversId { get; set; }

    [JsonProperty("ArrivalDateTime")]
    public string ArrivalDateTime { get; set; }

    [JsonProperty("DepartureDateTime")]
    public string DepartureDateTime { get; set; }

    [JsonProperty("FrontSeat1")]
    public int FrontSeat1 { get; set; }

    [JsonProperty("FrontSeat1Name")]
    public string FrontSeat1Name { get; set; }

    [JsonProperty("FrontSeat2")]
    public int FrontSeat2 { get; set; }

    [JsonProperty("FrontSeat2Name")]
    public string FrontSeat2Name { get; set; }

    [JsonProperty("1stSeat1")]
    public int FirstSeat1 { get; set; }

    [JsonProperty("1stSeat1Name")]
    public string FirstSeat1Name { get; set; }

    [JsonProperty("1stSeat2")]
    public int FirstSeat2 { get; set; }

    [JsonProperty("1stSeat2Name")]
    public string FirstSeat2Name { get; set; }

    [JsonProperty("1stSeat3")]
    public int FirstSeat3 { get; set; }

    [JsonProperty("1stSeat3Name")]
    public string FirstSeat3Name { get; set; }

    [JsonProperty("1stSeat4")]
    public int FirstSeat4 { get; set; }

    [JsonProperty("1stSeat4Name")]
    public string FirstSeat4Name { get; set; }

    [JsonProperty("2ndSeat1")]
    public int SecondSeat1 { get; set; }

    [JsonProperty("2ndSeat1Name")]
    public string SecondSeat1Name { get; set; }

    [JsonProperty("2ndSeat2")]
    public int SecondSeat2 { get; set; }

    [JsonProperty("2ndSeat2Name")]
    public string SecondSeat2Name { get; set; }

    [JsonProperty("2ndSeat3")]
    public int SecondSeat3 { get; set; }

    [JsonProperty("2ndSeat3Name")]
    public string SecondSeat3Name { get; set; }

    [JsonProperty("2ndSeat4")]
    public int SecondSeat4 { get; set; }

    [JsonProperty("2ndSeat4Name")]
    public string SecondSeat4Name { get; set; }

    [JsonProperty("3rdSeat1")]
    public int ThirdSeat1 { get; set; }

    [JsonProperty("3rdSeat1Name")]
    public string ThirdSeat1Name { get; set; }

    [JsonProperty("3rdSeat2")]
    public int ThirdSeat2 { get; set; }

    [JsonProperty("3rdSeat2Name")]
    public string ThirdSeat2Name { get; set; }

    [JsonProperty("3rdSeat3")]
    public int ThirdSeat3 { get; set; }

    [JsonProperty("3rdSeat3Name")]
    public string ThirdSeat3Name { get; set; }

    [JsonProperty("3rdSeat4")]
    public int ThirdSeat4 { get; set; }

    [JsonProperty("3rdSeat4Name")]
    public string ThirdSeat4Name { get; set; }

    [JsonProperty("4thSeat1")]
    public int FourthSeat1 { get; set; }

    [JsonProperty("4thSeat1Name")]
    public string FourthSeat1Name { get; set; }

    [JsonProperty("4thSeat2")]
    public int FourthSeat2 { get; set; }

    [JsonProperty("4thSeat2Name")]
    public string FourthSeat2Name { get; set; }

    [JsonProperty("4thSeat3")]
    public int FourthSeat3 { get; set; }

    [JsonProperty("4thSeat3Name")]
    public string FourthSeat3Name { get; set; }

    [JsonProperty("4thSeat4")]
    public int FourthSeat4 { get; set; }

    [JsonProperty("4thSeat4Name")]
    public string FourthSeat4Name { get; set; }

    [JsonProperty("ExtraSeat1")]
    public int ExtraSeat1 { get; set; }

    [JsonProperty("ExtraSeat2")]
    public int ExtraSeat2 { get; set; }

    [JsonProperty("ExtraSeat3")]
    public int ExtraSeat3 { get; set; }

    [JsonProperty("ExtraSeat4")]
    public int ExtraSeat4 { get; set; }

    [JsonProperty("Status")]
    public int Status { get; set; }

    [JsonProperty("Date")]
    public string Date { get; set; }

    [JsonProperty("QueuesId")]
    public int QueuesId { get; set; }
}

public class ApiResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("data")]
    public List<ScheduledTransaction> Data { get; set; }
}

public class ApiUpdateResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("data")]
    public ScheduledTransaction Data { get; set; }
}