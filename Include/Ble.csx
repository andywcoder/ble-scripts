#r "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll"

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;

async Task<BluetoothLEDevice> ConnectToDeviceAsync(string deviceId)
{
    Console.WriteLine($"Connect to device, Id={deviceId}");

    var bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(deviceId);
	if(bluetoothLeDevice != null)
	{
		Console.WriteLine($"Connected to device, Name={bluetoothLeDevice.Name}");
		return bluetoothLeDevice;
	}
	else
	{
		throw new Exception($"Couldn't connect to device");
	}
}

async Task<GattDeviceService> GetServiceAsync(BluetoothLEDevice device, Guid uuid)
{
    Console.WriteLine($"Get GATT service, Uuid={uuid}");

    var servicesResult = await device.GetGattServicesForUuidAsync(uuid);
    if (servicesResult.Status == GattCommunicationStatus.Success)
    {
        var service = servicesResult.Services.SingleOrDefault();
		if (service != null)
		{
			return service;
		}
		else
		{
			throw new Exception($"Couldn't discover service");
		}
	}
    else
    {
        throw new Exception($"Couldn't discover service {servicesResult.Status}");
    }
}

async Task<GattCharacteristic> GetCharacteristicAsync(GattDeviceService service, Guid uuid)
{
    Console.WriteLine($"Get characteristic for service, Uuid={uuid}");

    var characteristicsResult = await service.GetCharacteristicsForUuidAsync(uuid);
    if (characteristicsResult.Status == GattCommunicationStatus.Success)
    {
		var characteristic = characteristicsResult.Characteristics.SingleOrDefault();
		if (characteristic != null)
		{
			return characteristic;
		}
		else
		{
			throw new Exception($"Couldn't discover characteristic");
		}
    }
    else
    {
        throw new Exception($"Couldn't discover characteristic {characteristicsResult.Status}");
    }
}

async Task RegisterCharacteristicNotificationHandler(GattCharacteristic characteristic, TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> notificationHandler)
{
	Console.WriteLine($"Register characteristic notification handler, Uuid={characteristic.Uuid}");

	var communicationStatus  = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
	if(communicationStatus == GattCommunicationStatus.Success)
	{
		characteristic.ValueChanged += notificationHandler;
	}
	else
	{
		throw new Exception($"Couldn't register characteristic notification handler {communicationStatus}");
	}
}

async Task<byte[]> ReadCharacteristicAsync(GattCharacteristic characteristic)
{
    Console.WriteLine($"Read value, Uuid={characteristic.Uuid}");

    var readResult = await characteristic.ReadValueAsync();
    if (readResult.Status == GattCommunicationStatus.Success)
    {
        var valueBytes = readResult.Value.ToArray();
        Console.WriteLine($"Value read, Length={valueBytes.Length}, Data={string.Join("-", Array.ConvertAll(valueBytes, b => b.ToString("X2")))}");
        return valueBytes;
    }
    else
    {
        throw new Exception($"Couldn't read value {readResult.Status}");
    }
}

async Task WriteCharacteristicAsync(GattCharacteristic characteristic, byte[] data)
{
    Console.WriteLine($"Write value, Uuid={characteristic.Uuid}, Data={string.Join("-", Array.ConvertAll(data, b => b.ToString("X2")))}");

    var communicationStatus = await characteristic.WriteValueAsync(CryptographicBuffer.CreateFromByteArray(data));
    if(communicationStatus != GattCommunicationStatus.Success)
    {
        throw new Exception($"Couldn't write value {communicationStatus}");
    }
}
