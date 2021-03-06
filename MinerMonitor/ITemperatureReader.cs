namespace MinerMonitor
{
    internal interface ITemperatureReader
    {
        int ReadGPUTemperature();
        void WaitUntilGPUTemperatureReachesValueOrTimeout(int temperature, int seconds);
    }
}