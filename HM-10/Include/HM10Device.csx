// Infos about the characteristics can be found at
// https://www.open-homeautomation.com/2016/08/23/reverse-engineering-the-mi-plant-sensor/
// https://wiki.hackerspace.pl/projects:xiaomi-flora
// https://community.home-assistant.io/t/xiaomi-mi-plants-monitor-flower/3388

#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.16299.0\Windows.winmd"

#load "..\..\Include\Ble.csx"

using Windows.Storage.Streams;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

async Task SendAtCommandAsync(string deviceId, string atCommand, Action<string> atCommandResponseCallback)
{
    var device = await ConnectToDeviceAsync(deviceId);
    var service = await GetServiceAsync(device, new Guid("0000ffe0-0000-1000-8000-00805f9b34fb"));
    var atCommandCharacteristic = await GetCharacteristicAsync(service, new Guid("0000ffe1-0000-1000-8000-00805f9b34fb"));
	
	var notificationSubscriptionStatus = await atCommandCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
	if(notificationSubscriptionStatus == GattCommunicationStatus.Success)
	{
		Console.WriteLine("Subscribed to notifications");
	}
	else
	{
		Console.WriteLine("Couldn't subscribe to notifications");
	}
	
	atCommandCharacteristic.ValueChanged += (sender, args) => {	
		var dataReader = DataReader.FromBuffer(args.CharacteristicValue);
		var data = new byte[args.CharacteristicValue.Length];
		dataReader.ReadBytes(data);
		
		Console.WriteLine($"Value read, Length={args.CharacteristicValue.Length}, Data={string.Join("-", Array.ConvertAll(data, b => b.ToString("X2")))}");
		
		atCommandResponseCallback(System.Text.Encoding.UTF8.GetString(data));
	};

    await WriteCharacteristicAsync(atCommandCharacteristic, System.Text.Encoding.UTF8.GetBytes(atCommand));
}
