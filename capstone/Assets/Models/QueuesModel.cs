using System.Collections.Generic;

public class QueuesModel
{
    public string VanPlateNumber { get; set; }
    public string DepartureDateTime { get; set; }
    public string ArrivalDateTime { get; set; }
    public string DriversId { get; set; }
    public string Id { get; set; }

    public QueuesModel(string vanPlateNumber, string departureDateTime, string arivalDateTime, string driversId, string id)
    {
        VanPlateNumber = vanPlateNumber;
        DepartureDateTime = departureDateTime;
        ArrivalDateTime = arivalDateTime;
        DriversId = driversId;
        Id = id;
    }
}


public class ResponseQueue
{
    public string status { get; set; }
    public List<QueuesModel> data { get; set; }
    public ResponseQueue(string _status)
    {
        status = _status;
        data = new List<QueuesModel>();
    }
}