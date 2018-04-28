#load "..\Include\CommandLine.csx"
#load "Include\FlowerCareDevice.csx"

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
		var sensorData = await GetFlowerCareSensorDataAsync(deviceId);

		Console.WriteLine();
		Console.WriteLine($"Temperature {sensorData.Temperature} °C");
		Console.WriteLine($"Light intensity {sensorData.LightIntensity} lux");
		Console.WriteLine($"Soil moisture {sensorData.SoilMoisture}%");
		Console.WriteLine($"Soil fertility {sensorData.SoilFertility} µS/cm");
	}
	catch(Exception e)
	{
		Console.WriteLine($"Error: {e.Message}");
	}
}
