using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Weightinfo
/// </summary>
public class WeightInfo
{
    private double m_questionWeight;

    public double QuestionWeight
    {
        get { return m_questionWeight; }
    }

    private int m_number;

    public int Number
    {
        get { return m_number; }
    }

    private int m_mistakesNumber;

    public int MistakesNumber
    {
        get { return m_mistakesNumber; }
    }

    private int m_streakNumber;

    public int StreakNumber
    {
        get { return m_streakNumber; }
    }

    public WeightInfo(double weight, int number, int mistakesNumber, int streakNumber)
    {
        m_questionWeight = weight;
        m_number = number;
        m_mistakesNumber = mistakesNumber;
        m_streakNumber = streakNumber;
    }
}