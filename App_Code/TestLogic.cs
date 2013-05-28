using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class TestLogic
{
    static Random r = new Random();
    public const double QuestionIncrement = 1.11;
    public const double DomainIncrement = 1.09;
    public const double DomainDecrement = 1.0 / DomainIncrement;
    public const double MinDomainWeight = 3.0;
    public const double MaxDomainWeight = 10.0;
    public const double MaxQuestionWeight = 15.0;

    public static int ChooseQuestion(Dictionary<int, WeightInfo> d,Dictionary<int, WeightInfo> f)
    {
        double SumChoice = 0.0;
        foreach (WeightInfo wi in d.Values)
        {
            SumChoice += wi.QuestionWeight;
        }
        SumChoice = r.NextDouble() * SumChoice;
        double PartialSum = 0.0;
        foreach (KeyValuePair<int, WeightInfo> entry in d)
        {
            PartialSum += entry.Value.QuestionWeight;
            if (SumChoice < PartialSum)
            {
                f.Add(entry.Key, entry.Value);
                d.Remove(entry.Key);
                return entry.Key;
            }
        }
        return d.ElementAt(d.Count - 1).Key;
    }

    public static int[] FisherYatesShuffle()
    {
        int n = 4;
        int[] p = { 0, 1, 2, 3 };
        while (n > 1)
        {
            int k = r.Next(n);
            n--;
            int v = p[k];
            p[k] = p[n];
            p[n] = v;
        }
        return p;
    }

    public static Dictionary<string, int> ChooseDomains(Dictionary<string,WeightInfo> dw,int n) {

        Dictionary<string, int> ret = new Dictionary<string, int>();

        double SumChoice = 0.0;
        foreach (WeightInfo wi in dw.Values)
        {
            SumChoice += wi.QuestionWeight;
        }
        double Choice;

        for (int i = 0; i < n; i++) {
            Choice = r.NextDouble() * SumChoice;
            double PartialSum = 0.0;
            foreach (KeyValuePair<string, WeightInfo> entry in dw)
            {
                PartialSum += entry.Value.QuestionWeight;
                if (Choice < PartialSum)
                {
                    if (!ret.ContainsKey(entry.Key))
                    {
                        ret.Add(entry.Key,1);
                    }
                    else
                    {
                        ret[entry.Key]++;
                    }
                    break;
                }
            }
        }
        return ret;
    }
}