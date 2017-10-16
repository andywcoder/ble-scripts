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

    var status = await GetFlowerCareStatusAsync(deviceId);

    Console.WriteLine();
    Console.WriteLine($"Battery level {status.BatteryLevel}%");
    Console.WriteLine($"Firmware version {status.FirmwareVersion}");
}
