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
		var status = await GetMJHTV1StatusAsync(deviceId);

		Console.WriteLine();
		Console.WriteLine($"Battery level {status.BatteryLevel}%");
		Console.WriteLine($"Firmware version {status.FirmwareVersion}");
	}
	catch(Exception e)
	{
		Console.WriteLine($"Error: {e.Message}");
	}
}
