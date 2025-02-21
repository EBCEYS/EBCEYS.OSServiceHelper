using System.Runtime.Versioning;
using System.ServiceProcess;
using EBCEYS.OSServiceHelper.Models;

namespace EBCEYS.OSServiceHelper.Interfaces
{
    /// <summary>
    /// The windows service helper interface.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public interface IWindowsServiceHelper : IOSServiceHelper
    {
        /// <summary>
        /// The service instance.
        /// </summary>
        ServiceController? Service { get; }
        /// <summary>
        /// Deletes the service from service controller.
        /// </summary>
        /// <param name="waitForExit">The time to wait for process execution.</param>
        /// <returns><c>true</c> if service was deleted suceffully; otherwise<c>false</c>.</returns>
        [Obsolete($"It's better to use {nameof(DeleteServiceWithOutput)}")]
        bool DeleteService(TimeSpan waitForExit);
        /// <summary>
        /// Deletes the service from service controller.
        /// </summary>
        /// <param name="waitForExit">The time to wait for process execution.</param>
        /// <returns>Standat output or <c>null</c> if there's no output.</returns>
        string? DeleteServiceWithOutput(TimeSpan waitForExit);
        /// <summary>
        /// Gets the service status <b>WINDOWS ONLY!!!</b>.
        /// </summary>
        /// <returns>The service status <see cref="ServiceControllerStatus"/> if exists; otherwise <c>null</c>.</returns>
        ServiceControllerStatus? GetServiceStatus();
        /// <summary>
        /// Installs the service to OS.
        /// </summary>
        /// <param name="path">The absolute path of executable file.</param>
        /// <param name="startMode">The start mode.</param>
        /// <param name="model">The wait for installation process exit model.</param>
        /// <returns>Standart output or <c>null</c> if there's no output.</returns>
        string? InstallService(string path, InstallServiceStartMode startMode, WaitForStatusModel model = default);
        /// <summary>
        /// Recreates the instance of service API instance from service controller.
        /// </summary>
        /// <returns><c>true</c> if service was recreated sucessfully; otherwise <c>false</c>.</returns>
        bool RecreateService();
        /// <summary>
        /// Refreshs the service.
        /// </summary>
        void RefreshService();
        /// <summary>
        /// Sets the description for service.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="waitFor">The wait for process exit model.</param>
        /// <returns>The standart process out or <c>null</c> if there's no output.</returns>
        string? SetDescriptionForService(string description, WaitForStatusModel waitFor = default);
        /// <summary>
        /// Pauses the service.
        /// </summary>
        /// <param name="waitFor">The wait for status model.</param>
        void PauseService(WaitForStatusModel waitFor = default);
    }
}