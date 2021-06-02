using OpenHardwareMonitor.Hardware;
using System;
using System.Linq;

namespace MinerMonitor
{
    internal class TemperatureReader : ITemperatureReader
    {
        #region Attributes

        private readonly Computer computer;

        #endregion

        public TemperatureReader(Computer computer)
        {
            this.computer = computer;

            computer.GPUEnabled = true;
            computer.Open();
        }

        public int ReadGPUTemperature()
        {
            IHardware hardware = this.computer.Hardware.First<IHardware>();

            hardware.Update();

            ISensor gpuSensor = hardware.Sensors.First((sensor) => sensor.SensorType == SensorType.Temperature);

            return gpuSensor.Value == null ? 0 : System.Convert.ToInt32(gpuSensor.Value);
        }

        public void WaitUntilGPUTemperatureReachesValueOrTimeout(int temperature, int seconds)
        {
            bool temperatureOk = false;
            int currentSeconds = 0;

            while (currentSeconds < seconds && !temperatureOk)
            {
                int currentTemperature = this.ReadGPUTemperature(); 

                if (currentTemperature >= temperature)
                {
                    temperatureOk = true;

                    Console.WriteLine();
                    ConsoleExtension.WriteLineWithColorAndRestorePrevious(ConsoleColor.Green, "Correct temperature reached - Miner operating normally");
                }
                else
                {
                    Console.WriteLine("Temperature not reached: {0}, waiting.", currentTemperature);
                }

                if (!temperatureOk)
                {
                    System.Threading.Thread.Sleep(1000);
                    currentSeconds++;
                }
            }

            if (!temperatureOk)
            {
                Console.WriteLine();
                ConsoleExtension.WriteLineWithColorAndRestorePrevious(ConsoleColor.Red, "Timeout waiting for temperature ok, leaving.");
            }
        }
    }
}
