#load "..\Include\CommandLine.csx"
#load "Include\MJHTV1Device.csx"

var commandLineArgs = ParseCommandLineArgs();
if (!commandLineArgs.ContainsKey("--device-id"))
{
    Console.WriteLine($"Required parameter --device-id is missing");
}
else
{
    var deviceId = commandLineArgs["--device-id"];

    var sensorData = await GetMJHTV1SensorDataAsync(deviceId);

    Console.WriteLine();
    Console.WriteLine($"Temperature {sensorData.Temperature} °C");
    Console.WriteLine($"Air humidity {sensorData.AirHumidity} %");
}
