using EBCEYS.OSServiceHelper.Models;

namespace EBCEYS.OSServiceHelper.Interfaces
{
    /// <summary>
    /// The OS service helper interface.
    /// </summary>
    public interface IOSServiceHelper
    {
        /// <summary>
        /// The service name.
        /// </summary>
        string ServiceName { get; }
        /// <summary>
        /// Checks the service exists;
        /// </summary>
        /// <returns><c>true</c> if service exists; otherwise <c>false</c>.</returns>
        bool IsServiceExists();
        /// <summary>
        /// Checks the service is running.
        /// </summary>
        /// <returns><c>true</c> if service is running; otherwise <c>false</c>.</returns>
        bool IsServiceRunning();
        /// <summary>
        /// Checks the service is stoped.
        /// </summary>
        /// <returns><c>true</c> if service is stoped; otherwise <c>false</c>.</returns>
        bool IsServiceStoped();
        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <param name="args">The arguments for service start process.</param>
        /// <param name="waitFor">The wait for status model.</param>
        void StartService(string[]? args = null, WaitForStatusModel waitFor = default);
        /// <summary>
        /// Stops the service.
        /// </summary>
        /// <param name="stopDependetServices">Indicates the rule to stop dependent services.</param>
        /// <param name="waitFor">The wait for status model.</param>
        void StopService(bool stopDependetServices = false, WaitForStatusModel waitFor = default);
    }
}