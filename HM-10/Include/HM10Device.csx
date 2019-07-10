#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Windows.winmd"

#load "..\..\Include\Ble.csx"

using System.Threading;
using System.Text;
using Windows.Storage.Streams;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

async Task SendAtCommandAsync(string deviceId, string atCommand, Action<string> atCommandResponseCallback)
{
    var device = await ConnectToDeviceAsync(deviceId);
    var service = await GetServiceAsync(device, new Guid("0000ffe0-0000-1000-8000-00805f9b34fb"));
    var atCommandCharacteristic = await GetCharacteristicAsync(service, new Guid("0000ffe1-0000-1000-8000-00805f9b34fb"));
	
	var manualResetEvent = new ManualResetEvent(false);
	
	await RegisterCharacteristicNotificationHandler(atCommandCharacteristic, (sender, args) => 
	{
		var dataReader = DataReader.FromBuffer(args.CharacteristicValue);
		var data = new byte[args.CharacteristicValue.Length];
		dataReader.ReadBytes(data);
		
		Console.WriteLine($"Value read, Length={args.CharacteristicValue.Length}, Data={string.Join("-", Array.ConvertAll(data, b => b.ToString("X2")))}");
		
		atCommandResponseCallback(Encoding.UTF8.GetString(data));

		manualResetEvent.Set();
	});

    await WriteCharacteristicAsync(atCommandCharacteristic, System.Text.Encoding.UTF8.GetBytes(atCommand));
	
	manualResetEvent.WaitOne();
}
