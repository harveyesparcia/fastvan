<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    $userusername = @$_REQUEST["username"];
    $userpassword = @$_REQUEST["password"];
    $firstname = @$_REQUEST["firstname"];
    $lastname = @$_REQUEST["lastname"];
    $typeofaccount = @$_REQUEST["typeofaccount"];
    $BirthDate = @$_REQUEST["BirthDate"];
    $Address = @$_REQUEST["Address"];
    $IsResign = @$_REQUEST["IsResign"];
    $IsBlocked = @$_REQUEST["IsBlocked"];
    $contactNumber = @$_REQUEST["ContactNumber"];
    $Email = @$_REQUEST["Email"];
    $isDriver = @$_REQUEST["isDriver"];
    $PlateNumber = @$_REQUEST["PlateNumber"];
    $DriversLicenseNumber = @$_REQUEST["DriversLicenseNumber"];

    $conn = new mysqli($servername, $username, $password, $database);

    // Check for database connection errors
    if ($conn->connect_error) {
        $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
    } else {
        // Use placeholders in the SQL query
        if ($isDriver == 1) {
            // If isDriver is 1, generate DriversId
            $stmt = $conn->prepare("INSERT INTO `account` (`username`, `password`, `firstname`, `lastname`, `typeofaccount`, `BirthDate`, `Address`, `IsResign`, `IsBlocked`, `ContactNumber`, `Email`, `Avatar`, `DriversId`, `PlateNumber`, `DriversLicenseNumber`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '', ?, ?, ?)");

            // Generate new username and password for Driver
            $userusername = $firstname . date("mdYs");
            $userpassword = $firstname . date("mdYs");

            // Create the DriverId by concatenating Username and date (mmddyyyy)
            $DriverId = $userusername . date("mdY");

            // Bind parameters to the prepared statement
            $stmt->bind_param("ssssisssssssss", $userusername, $userpassword, $firstname, $lastname, $typeofaccount, $BirthDate, $Address, $IsResign, $IsBlocked, $contactNumber, $Email, $DriverId, $PlateNumber, $DriversLicenseNumber);
        } else {
            // If isDriver is 0, generate PassengersId
            $stmt = $conn->prepare("INSERT INTO `account` (`username`, `password`, `firstname`, `lastname`, `typeofaccount`, `BirthDate`, `Address`, `IsResign`, `IsBlocked`, `ContactNumber`, `Email`, `Avatar`, `PassengersId`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '', ?)");

            // Create the PassengersId by concatenating Username and date (mmddyyyy)
            $PassengersId = $userusername . date("mdY");

            // Bind parameters to the prepared statement
            $stmt->bind_param("ssssisssssss", $userusername, $userpassword, $firstname, $lastname, $typeofaccount, $BirthDate, $Address, $IsResign, $IsBlocked, $contactNumber, $Email, $PassengersId);

            }

        // Execute the statement
        $stmt->execute();

        // Check for query errors
        if ($stmt->error) {
            $response = array("status" => "failed", "error" => "Database query error: " . $stmt->error);
            echo json_encode($response);
        } else {
            // Insert successful, provide success response with additional details
            $response = array(
                "status" => "success",
                "username" => $userusername,
                "password" => $userpassword,
                "Role" => $typeofaccount,
                "Username" => $userusername,
                "Firstname" => $firstname,
                "Lastname" => $lastname
            );
            echo json_encode($response);
        }

        // Close the prepared statement
        $stmt->close();

        // Close the database connection
        $conn->close();
    }
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>
