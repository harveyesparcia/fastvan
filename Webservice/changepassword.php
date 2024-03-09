<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    $userusername = @$_REQUEST["username"];
    $oldpassword = @$_REQUEST["oldpassword"];
    $newpassword = @$_REQUEST["newpassword"];
    
    // Output received data for debugging
    $debugInfo = array("username" => $userusername, "oldpassword" => $oldpassword, "newpassword" => $newpassword);

    $conn = new mysqli($servername, $username, $password, $database);

    // Check for database connection errors
    if ($conn->connect_error) {
        $response = array("error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
    } else {
        // Use prepared statement to prevent SQL injection
        $stmt = $conn->prepare("SELECT * FROM account WHERE username=? AND password=?");
        $stmt->bind_param("ss", $userusername, $oldpassword);
        $stmt->execute();
        $result = $stmt->get_result();

        // Check for query errors
        if (!$result) {
            $response = array("error" => "Database query error: " . $stmt->error);
            echo json_encode($response);
        } else {
            if ($result->num_rows == 1) {
                // Password verification successful, update the password
                $updateStmt = $conn->prepare("UPDATE account SET password=? WHERE username=?");
                $updateStmt->bind_param("ss", $newpassword, $userusername);
                $updateStmt->execute();

                // Check for update errors
                if ($updateStmt->error) {
                    $response = array("error" => "Failed to update password: " . $updateStmt->error);
                    echo json_encode($response);
                } else {
                    // Password update successful
                    $response = array("status" => "success");
                    echo json_encode($response);
                }

                // Close the update statement
                $updateStmt->close();
            } else {
                $response = array("error" => "Invalid credentials");
                echo json_encode($response);
            }
        }

        // Close the prepared statement
        $stmt->close();

        // Close the database connection
        $conn->close();
    }
} else {
    $response = array("error" => "Invalid request method");
    echo json_encode($response);
}
?>
