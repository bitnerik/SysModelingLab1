using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SystemModelingLab1
{
    public class Main
    {
        public double t = 0;
        public double tEnd = 500;
        private double h = 501;
        public int n = 0;
        public bool s = false;
        public List<Transact> q;
        public Transact transact1;
        public Transact transact2;
        public Excel excel;
        public List<int> queueCount = new List<int>();
        public double serverFreeTime = 0;
        public int averageQueueCount = 0;
        public double freeTimeCoeff = 0;

        public double H
        {
            get => h;
            set => h = Math.Round(value, Const.roundTo, MidpointRounding.AwayFromZero);
        }

        public double T
        {
            get => t;
            set => t = Math.Round(value, Const.roundTo, MidpointRounding.AwayFromZero);
        }

        public double ServerFreeTime
        {
            get => serverFreeTime;
            set => serverFreeTime = Math.Round(value, Const.roundTo, MidpointRounding.AwayFromZero);
        }

        public double FreeTimeCoeff
        {
            get => freeTimeCoeff;
            set => freeTimeCoeff = Math.Round(value, Const.roundTo, MidpointRounding.AwayFromZero);
        }

        public Main()
        {
            transact1 = new Transact(TypeEnum.L1, T);
            transact2 = new Transact(TypeEnum.L2, T);
            q = new List<Transact>();
            excel = new Excel();
        }

        public void Start()
        {
            WriteInFile(transact1, transact2);

            while (T <= 500)
            {
                queueCount.Add(q.Count);

                if (T == H)
                {
                    if (q.Count == 0)
                    {
                        H = 501;
                        s = false;
                    }
                    else
                    {
                        var transact = q.First();
                        H = T + transact.ProcessTime;
                        s = true;
                        q.Remove(transact);

                        //WriteInFile();
                    }
                }

                if (T == transact1.Next)
                {

                    if (s)
                    {
                        q.Add(transact1);
                        transact1 = new Transact(TypeEnum.L1, T);
                        WriteInFile();
                    }
                    else
                    {
                        s = true;
                        H = T + transact1.ProcessTime;
                        transact1 = new Transact(TypeEnum.L1, T);
                        WriteInFile();
                    }
                }
                if (T == transact2.Next)
                {
                    if (s)
                    {
                        q.Add(transact2);
                        transact2 = new Transact(TypeEnum.L2, T);
                        WriteInFile();
                    }
                    else
                    {
                        s = true;
                        H = T + transact2.ProcessTime;
                        transact2 = new Transact(TypeEnum.L2, T);
                        WriteInFile();
                    }
                }
                if (T == tEnd)
                {
                    WriteInFile(transact1, transact2);
                    break;
                }

                if (!s) ServerFreeTime += Const.notLessThan;
                T = Math.Round(T + Const.notLessThan, Const.roundTo);

                averageQueueCount = queueCount.Sum() / queueCount.Count;
                freeTimeCoeff = ServerFreeTime / T * 100;

            }


            excel.SaveAndClose();
        }

        public void WriteInFile(Transact transactL1, Transact transactL2)
        {
            var dataRow = new string[]
            {
                "", T.ToString(), transactL1.Next.ToString(), transactL2.Next.ToString(), H.ToString(), s.ToString(), q.Count.ToString(), string.Join(", ", q.Select(x => x.Type.ToString())), FreeTimeCoeff.ToString() + '%', averageQueueCount.ToString()
            };
            excel.WriteRow(dataRow);
        }

        public void WriteInFile(TypeEnum eventType)
        {

            var tr1 = q.OrderBy(x => x.Next).FirstOrDefault(x => x.Type == TypeEnum.L1);
            if (tr1 == null)
            {
                tr1 = transact1;
            }

            var tr2 = q.OrderBy(x => x.Next).FirstOrDefault(x => x.Type == TypeEnum.L2);
            if (tr2 == null)
            {
                tr2 = transact2;
            }

            var dataRow = new string[]
            {
                eventType.ToString(), T.ToString(), tr1.Next.ToString(), tr2.Next.ToString(), H.ToString(), s.ToString(), q.Count.ToString(), string.Join(", ", q.Select(x => x.Type.ToString()))
            };
            excel.WriteRow(dataRow);
        }

        public void WriteInFile()
        {
            var eventType = transact1.Next < transact2.Next ? TypeEnum.L1 : TypeEnum.L2;

            var dataRow = new string[]
            {
                eventType.ToString(), T.ToString(), transact1.Next.ToString(), transact2.Next.ToString(), H.ToString(), s.ToString(), q.Count.ToString(), string.Join(", ", q.Select(x => x.Type.ToString())), FreeTimeCoeff.ToString() + '%', averageQueueCount.ToString()
            };

            excel.WriteRow(dataRow);
        }
    }
}
