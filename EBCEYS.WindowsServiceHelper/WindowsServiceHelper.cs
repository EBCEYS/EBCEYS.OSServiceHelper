using System.Diagnostics;
using System.Runtime.Versioning;
using System.ServiceProcess;
using EBCEYS.OSServiceHelper.Extensions;
using EBCEYS.OSServiceHelper.Interfaces;
using EBCEYS.OSServiceHelper.Models;
using Microsoft.Extensions.Logging;

namespace EBCEYS.OSServiceHelper
{
    /// <summary>
    /// The <see cref="WindowsServiceHelper"/>.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class WindowsServiceHelper : IDisposable, IWindowsServiceHelper
    {
        //T0D0: Make a proposal to get married for a Star
        /// <summary>
        /// The service name.
        /// </summary>
        public string ServiceName { get; }
        private ServiceController? service;
        private readonly ILogger logger;

        /// <summary>
        /// Gets the <see cref="ServiceController"/> entity of selected service. Or <c>null</c> if service does not exists.
        /// </summary>
        public ServiceController? Service
        {
            get
            {
                return service ?? GetService();
            }
        }
        /// <summary>
        /// Initiates a new instance of the <see cref="WindowsServiceHelper"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceName">The service name.</param>
        /// <exception cref="ArgumentException"></exception>
        public WindowsServiceHelper(ILogger logger, string serviceName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(serviceName, nameof(serviceName));
            this.logger = logger;
            ServiceName = serviceName;
        }
        private ServiceController? GetService()
        {
            service = ServiceController.GetServices()?.FirstOrDefault(x => x.ServiceName == ServiceName);
            return service;
        }
        /// <summary>
        /// Recreates an instance of the <see cref="Service"/>.<br/>
        /// </summary>
        /// <returns><c>true</c> if service was recreated sucessfully; if service was not found: <c>false</c>.</returns>
        public bool RecreateService()
        {
            TryDisposeService(out _);
            ServiceController? serv = GetService();
            if (serv == null)
            {
                service = null;
                return false;
            }
            service = serv;
            return true;
        }

        /// <summary>
        /// Indicates that service is exists.
        /// </summary>
        /// <returns><c>true</c> if service exists; otherwise <c>false</c>.</returns>
        public bool IsServiceExists()
        {
            return Service != default;
        }
        /// <summary>
        /// Gets the service status.
        /// </summary>
        /// <returns><see cref="ServiceControllerStatus"/> if service exists; otherwise <c>null</c>.</returns>
        public ServiceControllerStatus? GetServiceStatus()
        {
            return Service?.Status;
        }
        /// <summary>
        /// Indicates that service is in status <see cref="ServiceControllerStatus.Running"/>.
        /// </summary>
        /// <returns><c>true</c> if service is in status <see cref="ServiceControllerStatus.Running"/></returns>
        public bool IsServiceRunning()
        {
            return GetServiceStatus() == ServiceControllerStatus.Running;
        }
        /// <summary>
        /// Indicates that service is in status <see cref="ServiceControllerStatus.Stopped"/>.
        /// </summary>
        /// <returns><c>true</c> if service is in status <see cref="ServiceControllerStatus.Stopped"/>.</returns>
        public bool IsServiceStoped()
        {
            return GetServiceStatus() == ServiceControllerStatus.Stopped;
        }
        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <param name="args">The arguments to start the service. [optional]</param>
        /// <param name="waitFor">The wait for status options. [optional]</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void StartService(string[]? args = default, WaitForStatusModel waitFor = default)
        {
            if (!IsServiceExists())
            {
                throw new InvalidOperationException("Service is not installed!");
            }
            if (args != default)
            {
                Service?.Start(args);
            }
            else
            {
                Service?.Start();
            }
            Service?.WaitForStatus(ServiceControllerStatus.Running, waitFor);
        }
        /// <summary>
        /// Stops the service.
        /// </summary>
        /// <param name="stopDependetServices">Indicates should system stop dependent serivces. [default <c>false</c>]</param>
        /// <param name="waitFor">The wait for status change options. [optional]</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void StopService(bool stopDependetServices = false, WaitForStatusModel waitFor = default)
        {
            if (!IsServiceExists())
            {
                throw new InvalidOperationException("Service is not installed!");
            }
            Service?.Stop(stopDependetServices);
            Service?.WaitForStatus(ServiceControllerStatus.Stopped, waitFor);
            logger.LogDebug("Service stoped...");
        }
        /// <summary>
        /// Pauses the service.
        /// </summary>
        /// <param name="waitFor">The wait for status change options. [optional]</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void PauseService(WaitForStatusModel waitFor = default)
        {
            if (!IsServiceExists())
            {
                throw new InvalidOperationException("Service is not installed!");
            }
            Service?.Pause();
            Service?.WaitForStatus(ServiceControllerStatus.Paused, waitFor);
        }
        /// <summary>
        /// Deletes the service.<br/>
        /// <b>WARNING!!!</b> service should be in status <see cref="ServiceControllerStatus.Stopped"/>.
        /// </summary>
        /// <param name="waitForExit">The time for process exition awaiting.</param>
        /// <returns><c>true</c> if deletion process was exited; otherwise <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool DeleteService(TimeSpan waitForExit)
        {
            if (!IsServiceExists())
            {
                throw new InvalidOperationException("Service is not installed!");
            }
            if (!IsServiceStoped())
            {
                throw new InvalidOperationException("Service should be stoped");
            }
            ProcessStartInfo installInfo = new()
            {
                FileName = "sc.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                Arguments = $"delete {ServiceName}"
            };
            using Process deleteProcess = new()
            {
                StartInfo = installInfo,
            };
            logger.LogDebug($"Try to execute service uninstall process {installInfo.FileName} {installInfo.Arguments}");
            deleteProcess.Start();
            bool res = deleteProcess.WaitForExit(waitForExit);

            string output = deleteProcess.StandardOutput.ReadToEnd();
            logger.LogDebug($"Process result output: {output}");
            logger.LogDebug("Please remove files from service working directory");
            return res;

        }
        /// <summary>
        /// Installs the service.<br/>
        /// <b>WARNING!!!</b> service should not exists.
        /// </summary>
        /// <param name="path">The execution file path.</param>
        /// <param name="startMode">The service start mode.</param>
        /// <param name="waitFor">The wait for installation process exit model.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void InstallService(string path, InstallServiceStartMode startMode = default, WaitForStatusModel waitFor = default)
        {
            if (IsServiceExists())
            {
                throw new InvalidOperationException($"Service is already installed! Current service status is {GetServiceStatus()}");
            }
            ProcessStartInfo installInfo = new()
            {
                FileName = "sc.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            };
            List<string> argsList = [];
            argsList.Add($"create {ServiceName}");
            argsList.Add($"binPath= \"{path}\"");
            argsList.Add($"DisplayName=\"{ServiceName}\"");
            argsList.Add($"start= {startMode}");
            string installArgs = string.Join(" ", argsList);
            installInfo.Arguments = installArgs;
            logger.LogDebug($"Start installing process: {installInfo.FileName} {installArgs}");
            using Process installProcess = new()
            {
                StartInfo = installInfo
            };
            installProcess.Start();
            logger.LogDebug("Start installation process...");
            installProcess.WaitForExit(waitFor);

            string? output = installProcess.StandardOutput?.ReadToEnd() ?? "ERROR: no out!";
            logger.LogDebug($"Installation result:");
            logger.LogDebug(output);
        }
        /// <inheritdoc/>
        public string? SetDescriptionForService(string description, WaitForStatusModel waitFor = default)
        {
            Process setDescProcess = new()
            {
                StartInfo = new()
                {
                    FileName = "sc.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    Arguments = $"description {ServiceName} \"{description}\""
                }
            };
            setDescProcess.Start();
            setDescProcess.WaitForExit(waitFor);

            return setDescProcess.StandardOutput?.ReadToEnd();
        }

        private bool TryDisposeService(out Exception? ex)
        {
            ex = null;
            try
            {
                service?.Close();
                service?.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                ex = exception;
                return false;
            }
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            TryDisposeService(out _);
        }
        /// <inheritdoc/>
        public void RefreshService()
        {
            Service?.Refresh();
        }
    }
}
