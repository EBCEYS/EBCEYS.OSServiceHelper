using System.Runtime.Versioning;
using System.ServiceProcess;
using EBCEYS.OSServiceHelper.Models;

namespace EBCEYS.OSServiceHelper.Extensions
{
    /// <summary>
    /// The service controller extensions.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class ServiceControllerExtensions
    {
        /// <summary>
        /// Waits for service change status to entered.
        /// </summary>
        /// <param name="sc">The service controller.</param>
        /// <param name="status">The status that would be awaited.</param>
        /// <param name="waitForStatusInfo">The wait for status info. </param>
        internal static void WaitForStatus(this ServiceController sc, ServiceControllerStatus status, WaitForStatusModel waitForStatusInfo = default)
        {
            if (waitForStatusInfo.ShouldWaitForStatus)
            {
                if (!waitForStatusInfo.WaitTime.HasValue)
                {
                    sc.WaitForStatus(status);
                    return;
                }
                sc.WaitForStatus(status, waitForStatusInfo.WaitTime.Value);
            }
        }
    }
}
