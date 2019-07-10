#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Windows.winmd"

#load "..\Include\CommandLine.csx"
#load "..\Include\Ble.csx"

using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

var commandLineArgs = ParseCommandLineArgs();
if (!commandLineArgs.ContainsKey("--device-id"))
{
    Console.WriteLine($"Required parameter --device-id is missing");
}
else if (!commandLineArgs.ContainsKey("--service-id"))
{
    Console.WriteLine($"Required parameter --service-id is missing");
}
else if (!commandLineArgs.ContainsKey("--characteristic-id"))
{
    Console.WriteLine($"Required parameter --characteristic-id is missing");
}
else
{
    var deviceId = commandLineArgs["--device-id"];
	var serviceId = commandLineArgs["--service-id"];
	var characteristicId = commandLineArgs["--characteristic-id"];

    var device = await ConnectToDeviceAsync(deviceId);
    var service = await GetServiceAsync(device, new Guid(serviceId));
    var characteristic = await GetCharacteristicAsync(service, new Guid(characteristicId));

    await ReadCharacteristicAsync(characteristic);
}
