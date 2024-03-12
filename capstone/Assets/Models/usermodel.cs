using System.Collections.Generic;

public class UserModel
{
    public string Role { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string DriversId { get; set; }
    public string PassengersId { get; set; }
    public string PlateNumber { get; set; }
    public string Avatar { get; set; }
    public string BirthDate { get; set; }
    public string Address { get; set; }
    public string DriversLicenseNumber { get; set; }

    public string Status { get; set; }


    public UserModel(string role, string username, string firstName, string lastName, string password, string status,string driversId , string address)
    {
        Role = role;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Status = status;
        DriversId = driversId;
        Address = address;
    }
}


public class Response
{
    public string status { get; set; }
    public Response(string _status)
    {
        status = _status;
    }
}

public class DriverResponse
{
    public string status { get; set; }
    public List<UserModel> data { get; set; }
    public DriverResponse(string _status, List<UserModel> _data)
    {
        status = _status;
        data = _data;
    }
}