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
            set => next = Math.Round(value, Const.roundTo, MidpointRounding.AwayFromZero);
        }
        public double ProcessTime
        {
            get => processTime;
            set =>  processTime = Math.Round(value, Const.roundTo, MidpointRounding.AwayFromZero);
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
                return MyRandom.Normal;
            }
            else
            {
                return MyRandom.Exp;
            }
        }

        private double GetNextTransact(TypeEnum type)
        {
            if (type == TypeEnum.L1)
            {
                return MyRandom.Erl;
            }
            else
            {
                return MyRandom.Puas;
            }
        }
    }
}
