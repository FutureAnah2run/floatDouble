using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FloatDoublTester : MonoBehaviour
{
    [SerializeField]
    private ResultIndicator plus;
    [SerializeField]
    private ResultIndicator minus;
    [SerializeField]
    private ResultIndicator multiply;
    [SerializeField]
    private ResultIndicator divide;
    [SerializeField]
    private ResultIndicator f2d;
    [SerializeField]
    private ResultIndicator d2f;
    [SerializeField]
    private int iterCount = 1000000;
    [SerializeField]
    private Text platorm;

    private double result;

    private Stopwatch timer = new Stopwatch();

    public double DoubleMultiply()
    {
        timer.Reset();
        timer.Start();
        
        double tmpd = 1322.0;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpd *= tmpd;
        }   
        timer.Stop();
        result = tmpd;
        return timer.Elapsed.TotalSeconds;
    }

    public double FloatMultiply()
    {
        timer.Reset();
        timer.Start();

        float tmpf = 1322.0f;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpf *= tmpf;
        }

        timer.Stop();
        result = tmpf;
        return timer.Elapsed.TotalSeconds;
    }

    public double DoubleDivide()
    {
        timer.Reset();
        timer.Start();

        double tmpd = 20399322.0;
        double divider = 1.002347;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpd /= divider;
        }

        timer.Stop();
        result = tmpd;
        return timer.Elapsed.TotalSeconds;
    }

    public double FloatDivide()
    {
        timer.Reset();
        timer.Start();

        float tmpf = 20399322.0f;
        float divider = 1.002347f;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpf /= divider;
        }

        timer.Stop();
        result = tmpf;
        return timer.Elapsed.TotalSeconds;
    }

    public double DoublePlus()
    {
        timer.Reset();
        timer.Start();

        double tmpd = 0.0;
        double divider = 1.002347;

        for (int i = 0; i < iterCount; ++i)
        { 
            tmpd += divider;
        }

        timer.Stop();
        result = tmpd;  
        return timer.Elapsed.TotalSeconds;
    }

    public double FloatPlus()
    {
        timer.Reset();
        timer.Start();

        float tmpf = 0.0f;
        float divider = 1.002347f;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpf += divider;
        }

        timer.Stop();
        result = tmpf;
        return timer.Elapsed.TotalSeconds;
    }

    public double DoubleMinus()
    {
        timer.Reset();
        timer.Start();

        double tmpd = 0.0;
        double divider = 1.002347;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpd -= divider;
        }

        timer.Stop();
        result = tmpd;
        return timer.Elapsed.TotalSeconds;
    }

    public double FloatMinus()
    {
        timer.Reset();
        timer.Start();

        float tmpf = 0.0f;
        float divider = 1.002347f;

        for (int i = 0; i < iterCount; ++i)
        {
            tmpf -= divider;
        }

        timer.Stop();
        result = tmpf;
        return timer.Elapsed.TotalSeconds;
    }

    public double FloatToDouble()
    {
        timer.Reset();
        timer.Start();

        float tmpf = Mathf.PI;
        for (int i = 0; i < iterCount; ++i)
        {
            result = (double)tmpf;
        }

        timer.Stop();
        return timer.Elapsed.TotalSeconds;

    }

    public double DoubleToFloat()
    {
        timer.Reset();
        timer.Start();

        double tmpf = Mathf.PI;
        float total = 0;
        for (int i = 0; i < iterCount; ++i)
        {
            total = (float)tmpf;
        }

        result = total;
        timer.Stop();
        return timer.Elapsed.TotalSeconds;
    }

    public void InitUI()
    {
        plus.ClearResult();
        minus.ClearResult();
        multiply.ClearResult();
        divide.ClearResult();
        f2d.ClearResult();
        d2f.ClearResult();

        plus.SetFieldText("A+B");
        minus.SetFieldText("A-B");
        multiply.SetFieldText("A*B");
        divide.SetFieldText("A/B");
        f2d.SetFieldText("f2d");
        d2f.SetFieldText("d2f");
    }

    public void StartTest()
    {
        plus.SetResult(FloatPlus(), DoublePlus());
        minus.SetResult(FloatMinus(), DoubleMinus());
        multiply.SetResult(FloatMultiply(), DoubleMultiply());
        divide.SetResult(FloatDivide(), DoubleDivide());
        f2d.SetResult(FloatToDouble(), 0);
        d2f.SetResult(DoubleToFloat(), 0);
    }

    public void OnClickTestButton()
    {
        StartTest();
    }

    public void SetPlatformText()
    {
        if (platorm)
        {
            platorm.text = $"{Application.platform}\nHighRes: {Stopwatch.IsHighResolution}";
        }
    }

    public void Start()
    {
        InitUI();
        SetPlatformText();
    }
}
