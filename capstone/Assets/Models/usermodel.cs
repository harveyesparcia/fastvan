public class UserModel
{
    public string Role { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }

    public string Status { get; set; }

    public UserModel(string role, string username, string firstName, string lastName, string password, string status)
    {
        Role = role;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Status = status;
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