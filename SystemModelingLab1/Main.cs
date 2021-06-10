using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

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

        public double H
        {
            get => h;
            set => h = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        }

        public double T
        {
            get => t;
            set => t = Math.Round(value, 1, MidpointRounding.AwayFromZero);
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

                        WriteInFile(transact.Type);
                    }
                }

                if (T == transact1.Next)
                {

                    if (s)
                    {
                        q.Add(transact1);
                        transact1 = new Transact(TypeEnum.L1, T);
                    }
                    else
                    {
                        s = true;
                        H = T + transact1.ProcessTime;
                        transact1 = new Transact(TypeEnum.L1, T);
                        WriteInFile(transact1.Type);
                    }
                }
                if (T == transact2.Next)
                {
                    if (s)
                    {
                        q.Add(transact2);
                        transact2 = new Transact(TypeEnum.L2, T);
                    }
                    else
                    {
                        s = true;
                        H = T + transact2.ProcessTime;
                        transact2 = new Transact(TypeEnum.L2, T);
                        WriteInFile(transact2.Type);
                    }
                }
                if (T == tEnd)
                {
                    WriteInFile(transact1, transact2);
                    break;
                }

                T = Math.Round(T + 0.1, 1);
            }

            excel.SaveAndClose();
        }

        public void WriteInFile(Transact transactL1, Transact transactL2)
        {
            var dataRow = new string[]
            {
                "", T.ToString(), transactL1.Next.ToString(), transactL2.Next.ToString(), H.ToString(), s.ToString(), q.Count.ToString(), q.Count.ToString()
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
    }
}
