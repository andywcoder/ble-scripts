# Flower care

c# scripts to read the sensor data of the <a href="http://www.huahuacaocao.com/product">Flower Care Smart Monitor</a> (temperature, light intensity, watering and fertility).

Some people already did the reverse engineering work (thanks a lot) and they have written python scripts to read the sensor data.
* <a href="https://www.open-homeautomation.com/2016/08/23/reverse-engineering-the-mi-plant-sensor/">https://www.open-homeautomation.com/2016/08/23/reverse-engineering-the-mi-plant-sensor/</a>
* <a href="https://wiki.hackerspace.pl/projects:xiaomi-flora">https://wiki.hackerspace.pl/projects:xiaomi-flora</a>
* <a href="https://community.home-assistant.io/t/xiaomi-mi-plants-monitor-flower/3388">https://community.home-assistant.io/t/xiaomi-mi-plants-monitor-flower/3388</a>

## Read the Flower Care device status

```csi FlowerCareGetStatus.csx --device-id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16```

### Sample output

```
Connect to device, Id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16
Connected to device, Name=Bluetooth c4:7c:8d:65:a9:16
Get GATT service, Uuid=00001204-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00001a02-0000-1000-8000-00805f9b34fb
Read value, Uuid=00001a02-0000-1000-8000-00805f9b34fb
Value read, Length=7, Data=63-27-33-2E-31-2E-38

Battery level 99%
Firmware version 3.1.8
```

## Read the Flower Care sensor data

```csi FlowerCareGetSensorData.csx --device-id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16```

### Sample output

```
Connect to device, Id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16
Connected to device, Name=Bluetooth c4:7c:8d:65:a9:16
Get GATT service, Uuid=00001204-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00001a00-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00001a01-0000-1000-8000-00805f9b34fb
Write value, Uuid=00001a00-0000-1000-8000-00805f9b34fb, Data=A0-1F
Read value, Uuid=00001a01-0000-1000-8000-00805f9b34fb
Value read, Length=16, Data=F5-00-00-5F-00-00-00-00-00-00-02-3C-00-FB-34-9B

Temperature 24.5 °C
Light intensity 95 lux
Soil moisture 35%
Soil fertility 12 µS/cm
```

## Trigger a web hook

```csi FlowerCareSendSensorWebHook.csx --device-id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16 --device-name=Ficus --web-hook-url=https://hooks.zapier.com/hooks/catch/...```

### Sample output

```
Connect to device, Id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16
Connected to device, Name=Bluetooth c4:7c:8d:65:a9:16
Get GATT service, Uuid=00001204-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00001a00-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00001a01-0000-1000-8000-00805f9b34fb
Write value, Uuid=00001a00-0000-1000-8000-00805f9b34fb, Data=A0-1F
Read value, Uuid=00001a01-0000-1000-8000-00805f9b34fb
Value read, Length=16, Data=F7-00-00-5A-00-00-00-00-00-00-02-3C-00-FB-34-9B

Temperature 24.7 °C
Light intensity 90 lux
Soil moisture 33%
Soil fertility 10 µS/cm
Post json data, Url=https://hooks.zapier.com/hooks/catch/..., JsonContent={"name":"Ficus","soilMoisture":0}
```
