using System.Diagnostics.CodeAnalysis;

namespace EBCEYS.OSServiceHelper.Models
{
    /// <summary>
    /// The wait for status info.
    /// </summary>
    /// <param name="waitForStatus">Indicates should method wait service to change status.</param>
    /// <param name="timeout">Time that thread would be blocked while waiting status.</param>
    public readonly struct WaitForStatusModel(bool waitForStatus = true, TimeSpan? timeout = null)
    {
        /// <summary>
        /// Indicates should thread awaits service status change.
        /// </summary>
        public readonly bool ShouldWaitForStatus = waitForStatus;
        /// <summary>
        /// <see cref="TimeSpan"/> that thread would be blocked while awaiting service status change.
        /// </summary>
        public readonly TimeSpan? WaitTime = timeout;
        /// <summary>
        /// The default realization of <see cref="WaitForStatusModel"/>.<br/>
        /// <see cref="ShouldWaitForStatus"/> is <c>true</c>.<br/>
        /// <see cref="WaitTime"/> is <c>null</c>.
        /// </summary>
        public static WaitForStatusModel Default => new();
        /// <inheritdoc/>
        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is WaitForStatusModel waitFor)
            {
                return waitFor.ShouldWaitForStatus == ShouldWaitForStatus && waitFor.WaitTime == WaitTime;
            }
            return false;
        }
        /// <summary>
        /// Compare two instances of <see cref="WaitForStatusModel"/>.
        /// </summary>
        /// <param name="left">The first instance of <see cref="WaitForStatusModel"/>.</param>
        /// <param name="right">The second instance of <see cref="WaitForStatusModel"/>.</param>
        /// <returns><c>true</c> if objects are equal; otherwise <c>false</c>.</returns>
        public static bool operator ==(WaitForStatusModel left, WaitForStatusModel right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Compare two instances of <see cref="WaitForStatusModel"/>.
        /// </summary>
        /// <param name="left">The first instance of <see cref="WaitForStatusModel"/>.</param>
        /// <param name="right">The second instance of <see cref="WaitForStatusModel"/>.</param>
        /// <returns><c>true</c> if objects are <b>not</b> equal; otherwise <c>false</c>.</returns>
        public static bool operator !=(WaitForStatusModel left, WaitForStatusModel right)
        {
            return !(left == right);
        }
        /// <inheritdoc/>
        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
