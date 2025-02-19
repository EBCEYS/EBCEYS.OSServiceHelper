# EBCEYS.OSServiceHelper

[![.NET](https://github.com/EBCEYS/EBCEYS.OSServiceHelper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EBCEYS/EBCEYS.OSServiceHelper/actions/workflows/dotnet.yml)

## ��������:

����� �������������� ���������� ������ `ServiceController` ��� Windows.
���������� ��������� ��������� �������� � �� Windows.

� ������� ���������, �������� � ��������� �������� ������� sc.exe.

���������� �� ������ ������ ��������� ��������� `IOSServiceHelper` � ��� ������������� ��� �� Windows `WindowsServiceHelper`.


## IOSServiceHelper

������� ��������� ��� `ServiceHelper`.

�������� � ���� ��������� �������:

* `string ServiceName { get; }` - ��� ������.
* `bool IsServiceExists();` - �������� ��� ������ �����������.
* `bool IsServiceRunning();` - �������� ��� ������ ��������.
* `bool IsServiceStoped();` - �������� ��� ������ �����������.
* `void StartService(string[]? args = null, WaitForStatusModel waitFor = default);` - ������ ������.
* `void StopService(bool stopDependetServices = false, WaitForStatusModel waitFor = default);` - ��������� ������.

## IWindowsServiceHelper

��������� `IOSServiceHelper`.

�������� � ���� ������ ��� ������ �� �������� Windows:

* `bool DeleteService(TimeSpan waitForExit);` - �������� ������.
* `string? SetDescriptionForService(string description, WaitForStatusModel waitFor = default);` - ��������� �������� ��� ������.
* `ServiceControllerStatus? GetServiceStatus();` - ��������� ������� ������. 
* `bool RecreateService();` - ������������ �������� `ServiceController`.
* `void InstallService(string path, InstallServiceStartMode startMode, WaitForStatusModel model = default);` - ��������� ������.
* `void PauseService(WaitForStatusModel waitFor = default);` - ���������� ������ �� �����.



## WindowsServiceHelper

�������������� ��������� `IWindowsServiceHelper` ��� ������ �� �������� windows.

����������� ������ �������������� �� ������ *Debug*.

## �������

GitHub - [EBCEYS.DayOfAllLoversService](https://github.com/EBCEYS/EBCEYS.DayOfAllLoversService)