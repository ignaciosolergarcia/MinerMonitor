using System;
using System.Diagnostics;

namespace HardwareTemperature
{
    internal class MinerController : IMinerController
    {
        public void StartMiner()
        {
            Console.WriteLine("Starting new miner process ...");

            Process newMiner = Process.Start(Properties.Settings.Default.Executable, Properties.Settings.Default.CommandLine);

            ConsoleExtension.WriteLineWithColorAndRestorePrevious(ConsoleColor.Green, "... started with Process Id:{0}", newMiner.Id);
        }

        public void StopMiner()
        {
            Process[] miners = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(Properties.Settings.Default.Executable));

            foreach (Process miner in miners)
            {
                Console.WriteLine("Killing miner with Process Id:{0} ...", miner.Id);

                miner.Kill();
                miner.WaitForExit();

                Console.WriteLine("... killed.");
            }
        }
    }

}
