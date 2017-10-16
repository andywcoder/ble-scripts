#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.15063.0\Windows.winmd"

#load "..\Include\CommandLine.csx"

using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

var commandLineArgs = ParseCommandLineArgs();
if (!commandLineArgs.ContainsKey("--device-id"))
{
    Console.WriteLine($"Required parameter --device-id is missing");
}
else
{
    var deviceId = commandLineArgs["--device-id"];

    Console.WriteLine($"Connect to device {deviceId}");
    var bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(deviceId);
    Console.WriteLine($"Connected to device, Name={bluetoothLeDevice.Name}");

    Console.WriteLine($"Get GATT services");
    var servicesResult = await bluetoothLeDevice.GetGattServicesAsync();
    if (servicesResult.Status == GattCommunicationStatus.Success)
    {
        foreach (var service in servicesResult.Services)
        {
            Console.WriteLine($"Get characteristics for service, Uuid={service.Uuid}");
            var characteristicsResult = await service.GetCharacteristicsAsync();
            if (characteristicsResult.Status == GattCommunicationStatus.Success)
            {
                foreach (var characteristic in characteristicsResult.Characteristics)
                {
                    Console.WriteLine($"Characteristic, Uuid={characteristic.Uuid}, AttributeHandle={characteristic.AttributeHandle}, Properties={characteristic.CharacteristicProperties}");
                }
            }
            else
            {
                Console.WriteLine($"Error: Couldn't discover any characteristics {characteristicsResult.Status}");
            }
        }
    }
    else
    {
        Console.WriteLine($"Error: Couldn't discover any services {servicesResult.Status}");
    }
}