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
else if (!commandLineArgs.ContainsKey("--web-hook-url-statistics"))
{
    Console.WriteLine($"Required parameter --web-hook-url-statistics is missing");
}
else if (!commandLineArgs.ContainsKey("--web-hook-url-soil-moisture-alert"))
{
    Console.WriteLine($"Required parameter --web-hook-url-soil-moisture-alert is missing");
}
else
{
    var deviceId = commandLineArgs["--device-id"];
    var deviceName = commandLineArgs["--device-name"];
    var webHookUrlStatistics = commandLineArgs["--web-hook-url-statistics"];
	var webHookUrlSoilMoistureAlert = commandLineArgs["--web-hook-url-soil-moisture-alert"];

	var sensorData = await GetFlowerCareSensorDataAsync(deviceId);

	Console.WriteLine();
	Console.WriteLine($"Temperature {sensorData.Temperature} °C");
	Console.WriteLine($"Light intensity {sensorData.LightIntensity} lux");
	Console.WriteLine($"Soil moisture {sensorData.SoilMoisture}%");
	Console.WriteLine($"Soil fertility {sensorData.SoilFertility} µS/cm");

    await PostJsonData(webHookUrlStatistics, @"{""timestamp"": """ + DateTime.UtcNow + @""", ""name"": """ + deviceName + @""", ""temperature"": " + sensorData.Temperature + @", ""lightIntensity"": " + sensorData.LightIntensity + @", ""soilMoisture"": " + sensorData.SoilMoisture + @", ""soilFertility"": " + sensorData.SoilFertility + "}");

	if(sensorData.SoilMoisture <= 15)
	{
	    await PostJsonData(webHookUrlSoilMoistureAlert, @"{""timestamp"": """ + DateTime.UtcNow + @""", ""name"": """ + deviceName + @""", ""temperature"": " + sensorData.Temperature + @", ""lightIntensity"": " + sensorData.LightIntensity + @", ""soilMoisture"": " + sensorData.SoilMoisture + @", ""soilFertility"": " + sensorData.SoilFertility + "}");
	}
}
