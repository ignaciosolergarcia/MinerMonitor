namespace HardwareTemperature
{
    internal interface ITemperatureController
    {
        void ReadGPUTemperatureAndProcessValue();
    }
}