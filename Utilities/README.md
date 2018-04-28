# Utilities

c# scripts to enumerate and discover services and characteristics of BLE devices

## Enumerate BLE devices

```dude EnumerateBleDevices.csx```

### Sample output

```
Creating device watcher
Starting device enumeration

Device found, Id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16, Name=Flower care
Device property, Key=System.ItemNameDisplay, Value=Flower care
Device property, Key=System.Devices.DeviceInstanceId, Value=
Device property, Key=System.Devices.Icon, Value=C:\Windows\System32\DDORes.dll,-2001
Device property, Key=System.Devices.GlyphIcon, Value=C:\Windows\System32\DDORes.dll,-3001
Device property, Key=System.Devices.InterfaceEnabled, Value=
Device property, Key=System.Devices.IsDefault, Value=
Device property, Key=System.Devices.PhysicalDeviceLocation, Value=
Device property, Key={A35996AB-11CF-4935-8B61-A6761081ECDF} 19, Value=
Device property, Key=System.Devices.Aep.CanPair, Value=True
Device property, Key=System.Devices.Aep.IsPaired, Value=False
Device property, Key={A35996AB-11CF-4935-8B61-A6761081ECDF} 18, Value=False
Device property, Key={A35996AB-11CF-4935-8B61-A6761081ECDF} 13, Value=False
Device property, Key=System.Devices.Aep.DeviceAddress, Value=c4:7c:8d:65:a9:16
Device property, Key=System.Devices.Aep.IsConnected, Value=False

Device enumeration completed
```

## Discover BLE device services and characteristics

```dude DiscoverBleDeviceSercicesAndCharacteristics.csx --device-id=BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16```

### Sample output

```
Connect to device BluetoothLE#BluetoothLE98:5f:d3:3b:b0:55-c4:7c:8d:65:a9:16
Connected to device, Name=Flower care
Get GATT services
Get characteristics for service, Uuid=00001800-0000-1000-8000-00805f9b34fb
Characteristic, Uuid=00002a00-0000-1000-8000-00805f9b34fb, AttributeHandle=2, Properties=Read
Characteristic, Uuid=00002a01-0000-1000-8000-00805f9b34fb, AttributeHandle=4, Properties=Read
Characteristic, Uuid=00002a02-0000-1000-8000-00805f9b34fb, AttributeHandle=6, Properties=Read, Write
Characteristic, Uuid=00002a04-0000-1000-8000-00805f9b34fb, AttributeHandle=8, Properties=Read
Get characteristics for service, Uuid=00001801-0000-1000-8000-00805f9b34fb
Characteristic, Uuid=00002a05-0000-1000-8000-00805f9b34fb, AttributeHandle=13, Properties=Read, Indicate
Get characteristics for service, Uuid=0000fe95-0000-1000-8000-00805f9b34fb
Characteristic, Uuid=00000001-0000-1000-8000-00805f9b34fb, AttributeHandle=17, Properties=Read, Write, Notify
Characteristic, Uuid=00000002-0000-1000-8000-00805f9b34fb, AttributeHandle=20, Properties=Read
Characteristic, Uuid=00000004-0000-1000-8000-00805f9b34fb, AttributeHandle=22, Properties=Read, Notify
Characteristic, Uuid=00000007-0000-1000-8000-00805f9b34fb, AttributeHandle=24, Properties=Write
Characteristic, Uuid=00000010-0000-1000-8000-00805f9b34fb, AttributeHandle=26, Properties=Write
Characteristic, Uuid=00000013-0000-1000-8000-00805f9b34fb, AttributeHandle=28, Properties=Read, Write
Characteristic, Uuid=00000014-0000-1000-8000-00805f9b34fb, AttributeHandle=30, Properties=Read
Characteristic, Uuid=00001001-0000-1000-8000-00805f9b34fb, AttributeHandle=32, Properties=Notify
Get characteristics for service, Uuid=0000fef5-0000-1000-8000-00805f9b34fb
Characteristic, Uuid=8082caa8-41a6-4021-91c6-56f9b954cc34, AttributeHandle=36, Properties=Read, Write
Characteristic, Uuid=724249f0-5ec3-4b5f-8804-42345af08651, AttributeHandle=38, Properties=Read, Write
Characteristic, Uuid=6c53db25-47a1-45fe-a022-7c92fb334fd4, AttributeHandle=40, Properties=Read
Characteristic, Uuid=9d84b9a3-000c-49d8-9183-855b673fda31, AttributeHandle=42, Properties=Read, Write
Characteristic, Uuid=457871e8-d516-4ca1-9116-57d0b17b9cb2, AttributeHandle=44, Properties=Read, WriteWithoutResponse, Write
Characteristic, Uuid=5f78df94-798c-46f5-990a-b3eb6a065c88, AttributeHandle=46, Properties=Read, Notify
Get characteristics for service, Uuid=00001204-0000-1000-8000-00805f9b34fb
Characteristic, Uuid=00001a00-0000-1000-8000-00805f9b34fb, AttributeHandle=50, Properties=Read, Write
Characteristic, Uuid=00001a01-0000-1000-8000-00805f9b34fb, AttributeHandle=52, Properties=Read, Write, Notify
Characteristic, Uuid=00001a02-0000-1000-8000-00805f9b34fb, AttributeHandle=55, Properties=Read
Get characteristics for service, Uuid=00001206-0000-1000-8000-00805f9b34fb
Characteristic, Uuid=00001a11-0000-1000-8000-00805f9b34fb, AttributeHandle=59, Properties=Read
Characteristic, Uuid=00001a10-0000-1000-8000-00805f9b34fb, AttributeHandle=61, Properties=Read, Write, Notify
Characteristic, Uuid=00001a12-0000-1000-8000-00805f9b34fb, AttributeHandle=64, Properties=Read
```
