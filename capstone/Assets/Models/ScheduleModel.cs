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

    [JsonProperty("FrontSeat2")]
    public int FrontSeat2 { get; set; }

    [JsonProperty("1stSeat1")]
    public int FirstSeat1 { get; set; }

    [JsonProperty("1stSeat2")]
    public int FirstSeat2 { get; set; }

    [JsonProperty("1stSeat3")]
    public int FirstSeat3 { get; set; }

    [JsonProperty("1stSeat4")]
    public int FirstSeat4 { get; set; }

    [JsonProperty("2ndSeat1")]
    public int SecondSeat1 { get; set; }

    [JsonProperty("2ndSeat2")]
    public int SecondSeat2 { get; set; }

    [JsonProperty("2ndSeat3")]
    public int SecondSeat3 { get; set; }

    [JsonProperty("2ndSeat4")]
    public int SecondSeat4 { get; set; }

    [JsonProperty("3rdSeat1")]
    public int ThirdSeat1 { get; set; }

    [JsonProperty("3rdSeat2")]
    public int ThirdSeat2 { get; set; }

    [JsonProperty("3rdSeat3")]
    public int ThirdSeat3 { get; set; }

    [JsonProperty("3rdSeat4")]
    public int ThirdSeat4 { get; set; }

    [JsonProperty("4thSeat1")]
    public int FourthSeat1 { get; set; }

    [JsonProperty("4thSeat2")]
    public int FourthSeat2 { get; set; }

    [JsonProperty("4thSeat3")]
    public int FourthSeat3 { get; set; }

    [JsonProperty("4thSeat4")]
    public int FourthSeat4 { get; set; }

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