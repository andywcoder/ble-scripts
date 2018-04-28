# MJ-HT-V1 (Xiaomi Mijia Bluetooth Temperature Humidity Sensor)

c# scripts to read the sensor data of the <a href="http://cleargrass.com/">MJ-HT-V1 (Xiaomi Mijia Bluetooth Temperature Humidity Sensor )</a> (temperature, air humidity).

## Read the MJ-HT-V1 device status

```dude MJHTV1GetStatus.csx --device-id=BluetoothLE#BluetoothLE18:5e:0f:94:d4:54-4c:65:a8:d3:ea:9e```

### Sample output

```
Connect to device, Id=BluetoothLE#BluetoothLE18:5e:0f:94:d4:54-4c:65:a8:d3:ea:9e
Connected to device, Name=MJ_HT_V1
Get GATT service, Uuid=0000180f-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00002a19-0000-1000-8000-00805f9b34fb
Read value, Uuid=00002a19-0000-1000-8000-00805f9b34fb
Value read, Length=1, Data=64
Get GATT service, Uuid=0000180a-0000-1000-8000-00805f9b34fb
Get characteristic for service, Uuid=00002a26-0000-1000-8000-00805f9b34fb
Read value, Uuid=00002a26-0000-1000-8000-00805f9b34fb
Value read, Length=8, Data=30-30-2E-30-30-2E-36-36

Battery level 100%
Firmware version 00.00.66
```

## Read the MJ-HT-V1 sensor data

```dude MJHTV1GetSensorData.csx --device-id=BluetoothLE#BluetoothLE18:5e:0f:94:d4:54-4c:65:a8:d3:ea:9e```

### Sample output

```
Connect to device, Id=BluetoothLE#BluetoothLE18:5e:0f:94:d4:54-4c:65:a8:d3:ea:9e
Connected to device, Name=MJ_HT_V1
Get GATT service, Uuid=226c0000-6476-4566-7562-66734470666d
Get characteristic for service, Uuid=226caa55-6476-4566-7562-66734470666d
Register characteristic notification handler, Uuid=226caa55-6476-4566-7562-66734470666d

Temperature 24.3 °C
Air humidity 44.6 %
```
