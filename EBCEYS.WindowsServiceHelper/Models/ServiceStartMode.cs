using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;

namespace EBCEYS.OSServiceHelper.Models
{
    /// <summary>
    /// The service start mode.
    /// </summary>
    public readonly struct InstallServiceStartMode
    {
        /// <summary>
        /// The service start mode from <see href="https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/sc-create"/> <br/>
        /// See "Parameters:type=".
        /// </summary>
        public string Mode { get; }
        private InstallServiceStartMode(string mode = "demand")
        {
            Mode = mode;
        }
        /// <summary>
        /// Gets the <see cref="InstallServiceStartMode"/> instance from <see cref="ServiceStartMode"/> enum.
        /// </summary>
        /// <param name="startMode">The service start mode.</param>
        /// <returns>The <see cref="InstallServiceStartMode"/> instance or null if <see cref="ServiceStartMode.Disabled"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static InstallServiceStartMode? GetFromEnum(ServiceStartMode startMode)
        {
            return startMode switch
            {
                ServiceStartMode.Boot => Boot,
                ServiceStartMode.System => System,
                ServiceStartMode.Automatic => Auto,
                ServiceStartMode.Manual => Demand,
                ServiceStartMode.Disabled => null,
                _ => throw new NotImplementedException()
            };
        }
        /// <summary>
        /// boot - Specifies a device driver that is loaded by the boot loader.
        /// </summary>
        public static InstallServiceStartMode Boot => new("boot");
        /// <summary>
        /// system - Specifies a device driver that is started during kernel initialization.
        /// </summary>
        public static InstallServiceStartMode System => new("system");
        /// <summary>
        /// auto - Specifies a service that automatically starts each time the computer is restarted and runs even if no one logs on to the computer.
        /// </summary>
        public static InstallServiceStartMode Auto => new("auto");
        /// <summary>
        /// demand - Specifies a service that must be started manually. This is the default value if start= is not specified.
        /// </summary>
        public static InstallServiceStartMode Demand => new("demand");
        /// <summary>
        /// disabled - Specifies a service that cannot be started. To start a disabled service, change the start type to some other value.
        /// </summary>
        public static InstallServiceStartMode Disabled => new("disabled");
        /// <summary>
        /// delayed-auto - Specifies a service that starts automatically a short time after other auto services are started.
        /// </summary>
        public static InstallServiceStartMode DelayedAuto => new("delayed-auto");
        /// <summary>
        /// The default. Equal to <see cref="Demand"/>.
        /// </summary>
        public static InstallServiceStartMode Default => Demand;
        /// <summary>
        /// Returns the service start mode.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Mode;
        }
        /// <inheritdoc/>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is InstallServiceStartMode mode)
            {
                return Mode == mode.Mode;
            }
            return false;
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Mode);
        }
        /// <summary>
        /// Checks the first <see cref="InstallServiceStartMode"/> is equal to second.
        /// </summary>
        /// <param name="first">The first object.</param>
        /// <param name="second">The second object.</param>
        /// <returns><c>true</c> if <see cref="Mode"/> of the first object is equal with the second; otherwise <c>false</c>.</returns>
        public static bool operator ==(InstallServiceStartMode first, InstallServiceStartMode second)
        {
            return first.Mode == second.Mode;
        }
        /// <summary>
        /// Checks the first <see cref="InstallServiceStartMode"/> is not equal to second.
        /// </summary>
        /// <param name="first">The first object.</param>
        /// <param name="second">The second object.</param>
        /// <returns><c>true</c> if <see cref="Mode"/> of the first object is not equal with the second; otherwise <c>false</c>.</returns>
        public static bool operator !=(InstallServiceStartMode first, InstallServiceStartMode second)
        {
            return !(first.Mode == second.Mode);
        }
    }
}
