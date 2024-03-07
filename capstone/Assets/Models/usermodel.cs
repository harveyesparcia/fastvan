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
    public string DriversLicenseNumber { get; set; }

    public string Status { get; set; }


    public UserModel(string role, string username, string firstName, string lastName, string password, string status,string driversId )
    {
        Role = role;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Status = status;
        DriversId = driversId;
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