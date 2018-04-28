#load "..\Include\CommandLine.csx"
#load "..\Include\WebRequest.csx"
#load "Include\FlowerCareDevice.csx"

var commandLineArgs = ParseCommandLineArgs();
if (!commandLineArgs.ContainsKey("--device-id"))
{
    Console.WriteLine($"Required parameter --device-id is missing");
}
else if (!commandLineArgs.ContainsKey("--device-name"))
{
    Console.WriteLine($"Required parameter --device-name is missing");
}
else if (!commandLineArgs.ContainsKey("--web-hook-url"))
{
    Console.WriteLine($"Required parameter --web-hook-url is missing");
}
else
{
    var deviceId = commandLineArgs["--device-id"];
    var deviceName = commandLineArgs["--device-name"];
    var webHookUrl = commandLineArgs["--web-hook-url"];

	try
	{
		var sensorData = await GetFlowerCareSensorDataAsync(deviceId);

		Console.WriteLine();
		Console.WriteLine($"Temperature {sensorData.Temperature} °C");
		Console.WriteLine($"Light intensity {sensorData.LightIntensity} lux");
		Console.WriteLine($"Soil moisture {sensorData.SoilMoisture}%");
		Console.WriteLine($"Soil fertility {sensorData.SoilFertility} µS/cm");

		await PostJsonData(webHookUrl, @"{""name"": """ + deviceName + @""", ""soilMoisture"": " + sensorData.SoilMoisture + "}");
	}
	catch(Exception e)
	{
		Console.WriteLine($"Error: {e.Message}");
	}
}
