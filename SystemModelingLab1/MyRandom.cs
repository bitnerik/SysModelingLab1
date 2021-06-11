using System;
using System.IO;
using SystemModelingLab1;

public static class MyRandom
{
    static readonly Random Rnd = new Random((int)DateTime.Now.Ticks);
    public static double RandomDouble
    {
        get { return NotZero(Rnd.NextDouble); }
    }

    public static double Normal
    {
        get { return NotZero(NormalDistribution); }
    }

    public static double Exp
    {
        get { return NotZero(ExpDistribution, 2); }
    }

    public static double Erl
    {
        get { return NotZero(ErlandDistrib); }
    }

    public static double Puas
    {
        get { return NotZero(ExpDistribution, 0.5); }
    }


    public static double NormalDistribution()
    {
        var randomNorm = 12.0 + 2.0 * Math.Sin(2 * Math.PI * RandomDouble) * Math.Sqrt(-2 * Math.Log(RandomDouble));

        return randomNorm;
    }

    public static double ExpDistribution(double lam)
    {
        var exp = -Math.Log(RandomDouble) / lam;

        return exp;
    }

    public static double ErlandDistrib()
    {
        double erland = 0, length = 3, lam = 0.25;

        for (int i = 0; i < length; i++)
        {
            erland += ExpDistribution(lam);
        }

        return erland;
    }

    public static void RndTest()
    {
        var datArr = new string[1000];

        for (int i = 0; i < 1000; i++)
        {

            datArr[i] = NormalDistribution().ToString() + "," +
            ExpDistribution(2).ToString() + "," +
            ErlandDistrib().ToString() + "," +
            ExpDistribution(3).ToString() + "," +
            RandomDouble.ToString();
        }

        File.WriteAllLines("RndDat.csv", datArr);
    }

    public static double NotZero(Func<double> func)
    {
        double result = 0;
        while (result < Const.notLessThan) result = func.Invoke();

        return result;
    }

    public static double NotZero(Func<double, double> func, double lam)
    {
        double result = 0;
        while (result < Const.notLessThan) result = func.Invoke(lam);

        return result;
    }
}
