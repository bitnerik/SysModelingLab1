using System;

public static class MyRandom
{
	static readonly Random Rnd = new Random((int)DateTime.Now.Ticks);
    public static double RandomDouble
    {
        get { return GetRandomDouble(); }
    }

    public static double GetRandomDouble ()
    {
        double rndDouble = 0; 
            while (rndDouble == 0) rndDouble = Rnd.NextDouble();

        return rndDouble;
    }

    public static double NormalDistribution()
    {
        double randomNorm = 0;
        while (randomNorm < 0.05) randomNorm = 12.0 + 2.0 * Math.Sin(2 * Math.PI * RandomDouble) * Math.Sqrt(-2 * Math.Log(RandomDouble));

        return randomNorm;
    }

    public static double ExpDistribution(double alpha)
    {
        double exp = 0;
        while (exp < 0.05) exp = Math.Log((double)1 - RandomDouble) / (-alpha);

        return exp;
    }

    public static double ErlandDistrib()
    {
        double erland = 0, length = 3, alpha = 0.25;

        for (int i = 0; i <= length; i++)
        {
            erland += ExpDistribution(alpha);
        }

        while (erland < 0.05) erland = ErlandDistrib();

        return erland;
    }
}
