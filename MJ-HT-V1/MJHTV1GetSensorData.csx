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

	try
	{
		var sensorData = await GetMJHTV1SensorDataAsync(deviceId);

		Console.WriteLine();
		Console.WriteLine($"Temperature {sensorData.Temperature} °C");
		Console.WriteLine($"Air humidity {sensorData.AirHumidity} %");
	}
	catch(Exception e)
	{
		Console.WriteLine($"Error: {e.Message}");
	}
}
