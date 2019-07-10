#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Windows.winmd"

using System.Threading;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

var enumerationCompletedEvent = new ManualResetEvent(false);

Console.WriteLine("Creating device watcher");
var deviceWatcher = DeviceInformation.CreateWatcher(BluetoothLEDevice.GetDeviceSelectorFromPairingState(false), new string[] { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" }, DeviceInformationKind.AssociationEndpoint);
deviceWatcher.Added += (sender, args) =>
{
    Console.WriteLine("");
    Console.WriteLine($"Device found, Id={args.Id}, Name={args.Name}");

    foreach (var property in args.Properties)
    {
        Console.WriteLine($"Device property, Key={property.Key}, Value={property.Value}");
    }
};
deviceWatcher.EnumerationCompleted += async (sender, args) =>
{
    Console.WriteLine("");
    Console.WriteLine("Device enumeration completed");
    enumerationCompletedEvent.Set();
};

Console.WriteLine("Starting device enumeration");
deviceWatcher.Start();
enumerationCompletedEvent.WaitOne();
