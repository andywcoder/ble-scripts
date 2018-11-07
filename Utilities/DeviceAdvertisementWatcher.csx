#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.16299.0\Windows.winmd"

#load "..\Include\CommandLine.csx"

using System.Threading;
using Windows.Storage.Streams;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Enumeration;

var commandLineArgs = ParseCommandLineArgs();
if (commandLineArgs.ContainsKey("--help"))
{
    Console.WriteLine($"--company-id              filter by company ID");
	Console.WriteLine($"--scan-time-in-seconds    set scan time");
}
else
{
    var companyId = commandLineArgs.ContainsKey("--company-id") ? ushort.Parse(commandLineArgs["--company-id"], System.Globalization.NumberStyles.AllowHexSpecifier) : (ushort)0;
	var scanTimeInSeconds = commandLineArgs.ContainsKey("--scan-time-in-seconds") ? int.Parse(commandLineArgs["--scan-time-in-seconds"]) : 10;

	Console.WriteLine("Creating advertisements watcher");
	var watcher = new BluetoothLEAdvertisementWatcher();

	if(companyId != 0)
	{
		var manufacturerDataFilter = new BluetoothLEManufacturerData();
		manufacturerDataFilter.CompanyId = companyId;
		watcher.AdvertisementFilter.Advertisement.ManufacturerData.Add(manufacturerDataFilter);
	}

	watcher.Received += (sender, args) =>
	{
		Console.WriteLine($"");
		Console.WriteLine($"Advertisment received, AdvertisementType={args.AdvertisementType}, BluetoothAddress={args.BluetoothAddress}, RawSignalStrengthInDBm={args.RawSignalStrengthInDBm}");
		Console.WriteLine($"LocalName={args.Advertisement.LocalName}");
		Console.WriteLine($"Flags={args.Advertisement.Flags}");
		foreach (var manufacturerData in args.Advertisement.ManufacturerData)
		{
			var data = new byte[manufacturerData.Data.Length];
			using (var reader = DataReader.FromBuffer(manufacturerData.Data))
			{
				reader.ReadBytes(data);
			}
			Console.WriteLine($"ManufacturerData, Company={manufacturerData.CompanyId.ToString("X")}, Data={BitConverter.ToString(data)}");
		}
		foreach (var serviceUuid in args.Advertisement.ServiceUuids)
		{
			Console.WriteLine($"ServiceUuids, ServiceUuid={serviceUuid}");
		}
	};
	
	Console.WriteLine("Starting advertisements watcher");
	watcher.Start();

	await Task.Delay(TimeSpan.FromSeconds(scanTimeInSeconds));

	Console.WriteLine("Stopping advertisements watcher");
	watcher.Stop();
}
