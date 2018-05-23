namespace StatisticReaderLibrary
{
    public class SystemProperties
    {
        public double TimeToTransmit { get; }
        public double TransmitEnergy { get; }
        public double TimeToReceive { get; }
        public double ReceiveEnergy { get; }
        public double IdleEnergy { get; }

        public SystemProperties(double timeToTransmit, double transmitEnergy, double timeToReceive, double receiveEnergy, double idleEnergy)
        {
            TimeToTransmit = timeToTransmit;
            TransmitEnergy = transmitEnergy;
            TimeToReceive = TimeToReceive;
            ReceiveEnergy = receiveEnergy;
            IdleEnergy = idleEnergy;
        }
    }
}
