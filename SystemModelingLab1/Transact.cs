using System;

namespace SystemModelingLab1
{
    public class Transact
    {
        public TypeEnum Type { get; set; }
        private double next;
        private double processTime;
        public double Next
        {
            get => next;
            set => next = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        }
        public double ProcessTime
        {
            get => processTime;
            set =>  processTime = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        }

        public Transact(TypeEnum type, double t)
        {
            Type = type;
            Next = t + GetNextTransact(type);
            ProcessTime = GetProcessTime(type);
        }

        private double GetProcessTime(TypeEnum type)
        {
            if (type == TypeEnum.L1)
            {
                return MyRandom.NormalDistribution();
            }
            else
            {
                return MyRandom.ExpDistribution(2);
            }
        }

        private double GetNextTransact(TypeEnum type)
        {
            if (type == TypeEnum.L1)
            {
                return MyRandom.ErlandDistrib();
            }
            else
            {
                return MyRandom.ExpDistribution(0.5);
            }
        }
    }
}
