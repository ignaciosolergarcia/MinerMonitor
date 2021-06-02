using OpenHardwareMonitor.Hardware;
using System;
using System.Timers;

namespace HardwareTemperature
{
    internal class TemperatureController : ITemperatureController
    {
        #region Attributes

        private readonly ITemperatureReader temperatureReader;
        private readonly IMinerController minerController;

        #endregion

        public TemperatureController(ITemperatureReader temperatureReader, IMinerController minerController)
        {
            this.temperatureReader = temperatureReader;
            this.minerController = minerController;
        }

        public void ReadGPUTemperatureAndProcessValue()
        {
            if (this.temperatureReader.ReadGPUTemperature() < Properties.Settings.Default.MinimumTemperature)
            {
                Console.WriteLine();
                this.minerController.StopMiner();
                this.minerController.StartMiner();
                this.temperatureReader.WaitUntilGPUTemperatureReachesValueOrTimeout(Properties.Settings.Default.MinimumTemperature, Properties.Settings.Default.TimeoutWaitingTemperatureInSeconds);
            }
            else
            {
                ConsoleExtension.WriteWithColorAndRestorePrevious(ConsoleColor.Green, ".");
            }
        }
    }
}
