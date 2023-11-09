using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FloatDoubleTester : MonoBehaviour
{
    public float[] _floatList_A;
    public float[] _floatList_B;

    [Serializable]
    public struct sResult
    {
        public float result_imp_f;
        public float result_cvrt_f;
        public double result_d;        
    }

    public sResult[] _result_Win;
    public sResult[] _result_Mac;

    private const int _totalCount = 100;
    private int _caseCount => _operations.Length;

    public delegate double operationDelegate();
    private operationDelegate[] _operations;

    public void CreateCases()
    {
        InitFloatList(ref _floatList_A);
        InitFloatList(ref _floatList_B);
    }

    public void InitFloatList(ref float[] floats)
    {
        floats = new float[_totalCount];
        for(int i = 0; i < _totalCount; i++)
        {
            floats[i] = UnityEngine.Random.Range(0f, 10f);
        }
    }

    [ContextMenu("GetResult")]
    public void GetResult()
    {
        _operations = new operationDelegate[]
        {
            AddTwoFloatFloat,
            AddTwoFloatDouble,
            AddMulTwoFloatFloat,
            AddMulTwoFloatDouble,
            AddWholeFloatFloat,
            AddWholeFloatDouble,
            AddAndDivFloatFloat,
            AddAndDivFloatDouble
        };
        ref sResult[] results =
#if UNITY_EDITOR_WIN
        ref _result_Win;
#else
        ref _result_Mac;
#endif
        results = new sResult[_caseCount];
        for (int i = 0; i < _caseCount; i++)
        {
            double result = _operations[i]();
            results[i].result_d = result;
            results[i].result_cvrt_f = Convert.ToSingle(result);
            results[i].result_imp_f = (float)result;
            Debug.Assert(_result_Win[i].result_cvrt_f == _result_Win[i].result_imp_f, "convert result is different with implicit double");
        }
    }


    [ContextMenu("CheckResult")]
    public void CheckResult()
    {
        for (int i = 0; i < _caseCount; i+=2)
        {
            Debug.Assert(_result_Win[i].result_d == _result_Win[i+1].result_d, $"{i:000}: result_d is different.");
        }
    }

    [ContextMenu("CheckResult2")]
    public void CheckResult2()
    {
        for (int i = 0; i < _caseCount; i++)
        {
            Debug.Assert(_result_Win[i].result_d == _result_Mac[i].result_d, $"{i:000}: result_d is different.");
            Debug.Assert(_result_Win[i].result_cvrt_f == _result_Mac[i].result_cvrt_f, $"{i:000}: result_cvrt_f is different.");
            Debug.Assert(_result_Win[i].result_imp_f == _result_Mac[i].result_imp_f, $"{i:000}: result_imp_f is different.");
        }
    }

    public double AddTwoFloatFloat()
    {
        float result = 0f;
        result = _floatList_A[0] + _floatList_B[0];
        return result;
    }

    public double AddTwoFloatDouble()
    {
        double result = 0d;
        result = _floatList_A[0] + _floatList_B[0];
        return result;
    }

    public double AddMulTwoFloatFloat()
    {
        float result = 0f;
        for(int i = 0; i < _totalCount; i++)
        {
            result += _floatList_A[i] * _floatList_B[i];
        }
        return result;
    }

    public double AddMulTwoFloatDouble()
    {
        double result = 0d;
        for (int i = 0; i < _totalCount; i++)
        {
            result += _floatList_A[i] * _floatList_B[i];
        }
        return result;
    }

    public double AddWholeFloatFloat()
    {
        float result = 0f;
        for (int i = 0; i < _totalCount; i++)
        {
            result += _floatList_A[i];
        }
        return result;
    }

    public double AddWholeFloatDouble()
    {
        double result = 0d;
        for (int i = 0; i < _totalCount; i++)
        {
            result += _floatList_A[i];
        }
        return result;
    }

    public double AddAndDivFloatDouble()
    {
        float result = 0f;
        for (int i = 0; i < _totalCount; i++)
        {
            result *= _floatList_B[i];
            result += _floatList_A[i];
            result /= _floatList_B[i];
        }
        return result;
    }
    public double AddAndDivFloatFloat()
    {
        double result = 0d;
        for (int i = 0; i < _totalCount; i++)
        {
            result *= _floatList_B[i];
            result += _floatList_A[i];
            result /= _floatList_B[i];
        }
        return result;
    }
}
