using OpenHardwareMonitor.Hardware;
using System;
using System.Timers;

namespace MinerMonitor
{
    public class Program
    {
        #region Attributes

        private readonly IMinerController minerController;
        private readonly ITemperatureReader temperatureReader;
        private readonly ITemperatureController temperatureController;
        private readonly Computer computer;

        #endregion

        static void Main(string[] args)
        {
            Program instance = new Program();

            Console.WriteLine("Started on {0}", DateTime.Now);

            while (true)
            {
                instance.temperatureController.ReadGPUTemperatureAndProcessValue();

                System.Threading.Thread.Sleep(Properties.Settings.Default.MonitorIntervalInSeconds * 1000);
            }
        }

        public Program()
        {
            this.computer = new Computer();
            this.minerController = new MinerController();
            this.temperatureReader = new TemperatureReader(this.computer);
            this.temperatureController = new TemperatureController(this.temperatureReader, this.minerController);
        }
    }
}
