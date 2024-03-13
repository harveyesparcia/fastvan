using Newtonsoft.Json;
using System.Collections.Generic;

public static class Context {
    [JsonIgnore]
    public static bool IsLogin { get; set; }

    public static string firstname { get; set; }
    public static string lastname { get; set; }
    public static string username { get; set; }
    public static string role { get; set; }
    public static string DriversId { get; set; }
    public static string VanPlateNumber { get; set; }
    public static string Address { get; set; }
    public static string ContactNumber { get; set; }
    public static string Birth { get; set; }
    public static string Username { get; set; }
    public static string Password { get; set; }

    public static List<QueuesModel> Queues { get; set; }

}