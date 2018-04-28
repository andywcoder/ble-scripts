// Infos about the characteristics can be found at
// https://www.open-homeautomation.com/2016/08/23/reverse-engineering-the-mi-plant-sensor/
// https://wiki.hackerspace.pl/projects:xiaomi-flora
// https://community.home-assistant.io/t/xiaomi-mi-plants-monitor-flower/3388

#r "C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.16299.0\Windows.winmd"

#load "..\..\Include\Ble.csx"

struct FlowerCareStatus
{
    internal byte BatteryLevel { get; set; }
    internal string FirmwareVersion { get; set; }
}

struct FlowerCareSensorData
{
    internal float Temperature { get; set; }
    internal int LightIntensity { get; set; }
    internal byte SoilMoisture { get; set; }
    internal short SoilFertility { get; set; }
}

async Task<FlowerCareStatus> GetFlowerCareStatusAsync(string deviceId)
{
    var device = await ConnectToDeviceAsync(deviceId);
    var service = await GetServiceAsync(device, new Guid("00001204-0000-1000-8000-00805f9b34fb"));
    var statusCharacteristic = await GetCharacteristicAsync(service, new Guid("00001a02-0000-1000-8000-00805f9b34fb"));

    var status = await ReadCharacteristicAsync(statusCharacteristic);

    return new FlowerCareStatus()
    {
        BatteryLevel = status[0],
        FirmwareVersion = Encoding.ASCII.GetString(status.Skip(2).ToArray()).TrimEnd(new char[] { (char)0 })
    };
}

async Task<FlowerCareSensorData> GetFlowerCareSensorDataAsync(string deviceId)
{
    var device = await ConnectToDeviceAsync(deviceId);
    var service = await GetServiceAsync(device, new Guid("00001204-0000-1000-8000-00805f9b34fb"));
    var initializationCharacteristic = await GetCharacteristicAsync(service, new Guid("00001a00-0000-1000-8000-00805f9b34fb"));
    var sensorDataCharacteristic = await GetCharacteristicAsync(service, new Guid("00001a01-0000-1000-8000-00805f9b34fb"));

    await WriteCharacteristicAsync(initializationCharacteristic, new byte[] { 0xa0, 0x1f });

    var sensorData = await ReadCharacteristicAsync(sensorDataCharacteristic);

    return new FlowerCareSensorData()
    {
        Temperature = BitConverter.ToInt16(sensorData.Take(2).ToArray(), 0) / 10f,
        LightIntensity = BitConverter.ToInt32(sensorData.Skip(3).Take(4).ToArray(), 0),
        SoilMoisture = sensorData[7],
        SoilFertility = BitConverter.ToInt16(sensorData.Skip(8).Take(2).ToArray(), 0)
    };
}
