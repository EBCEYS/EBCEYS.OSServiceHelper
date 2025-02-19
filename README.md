# EBCEYS.OSServiceHelper

[![.NET](https://github.com/EBCEYS/EBCEYS.OSServiceHelper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EBCEYS/EBCEYS.OSServiceHelper/actions/workflows/dotnet.yml)

## Описание:

Очень верхоуровневая абстракция поверх `ServiceController` для Windows.
Библиотека позволяет управлять службами в ОС Windows.

В методах установки, удаления и установки описания дергает sc.exe.

Библиотека на данный момент реализует интерфейс `IOSServiceHelper` и его имплементацию для ОС Windows `WindowsServiceHelper`.


## IOSServiceHelper

Базовый интерфейс для `ServiceHelper`.

Содержит в себе несколько методов:

* `string ServiceName { get; }` - имя службы.
* `bool IsServiceExists();` - проверка что служба установлена.
* `bool IsServiceRunning();` - проверка что служба запущена.
* `bool IsServiceStoped();` - проверка что служба остановлена.
* `void StartService(string[]? args = null, WaitForStatusModel waitFor = default);` - запуск службы.
* `void StopService(bool stopDependetServices = false, WaitForStatusModel waitFor = default);` - остановка службы.

## IWindowsServiceHelper

Дополняет `IOSServiceHelper`.

Содержит в себе методы для работы со службами Windows:

* `bool DeleteService(TimeSpan waitForExit);` - удаление службы.
* `string? SetDescriptionForService(string description, WaitForStatusModel waitFor = default);` - установка описания для службы.
* `ServiceControllerStatus? GetServiceStatus();` - получение статуса службы. 
* `bool RecreateService();` - пересоздание инстанса `ServiceController`.
* `void InstallService(string path, InstallServiceStartMode startMode, WaitForStatusModel model = default);` - установка службы.
* `void PauseService(WaitForStatusModel waitFor = default);` - постановка службы на паузу.



## WindowsServiceHelper

Имплементирует интерфейс `IWindowsServiceHelper` для работы со службами windows.

Логирование внутри осуществляется на уровне *Debug*.

## Примеры

GitHub - [EBCEYS.DayOfAllLoversService](https://github.com/EBCEYS/EBCEYS.DayOfAllLoversService)