using System.Diagnostics;
using EBCEYS.OSServiceHelper.Models;

namespace EBCEYS.OSServiceHelper.Extensions
{
    internal static class ProcessExtensions
    {
        internal static bool WaitForExit(this Process process, WaitForStatusModel model)
        {
            if (model.ShouldWaitForStatus)
            {
                if (model.WaitTime.HasValue)
                {
                    return process.WaitForExit(model.WaitTime.Value);
                }
                process.WaitForExit();
                return true;
            }
            return true;
        }
    }
}
