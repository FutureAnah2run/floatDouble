using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FloatDoubleTester : MonoBehaviour
{
    public float[] _floatList_A;
    public float[] _floatList_B;

    [Serializable]
    public struct sResult
    {
        public float result_imp_f;
        public float result_exp_f;
        public double result_imp_d;
        public double result_exp_d;
    }

    public sResult[] _result_Win;
    public sResult[] _result_Mac;

    private const int _totalCount = 10000;

#if UNITY_EDITOR_WIN 
    [ContextMenu("CreateResult")]
#endif
    public void CreateCases()
    {
        InitFloatList(ref _floatList_A);
        InitFloatList(ref _floatList_B);
    }

    public void InitFloatList(ref float[] floats)
    {
        floats = new float[_totalCount];
        for (int i = 0; i < _totalCount; i++)
        {
            floats[i] = UnityEngine.Random.Range(0f, 10f);
        }
    }

    [ContextMenu("GetResult")]
    public void GetResult()
    {
        ref sResult[] results =
#if UNITY_EDITOR_WIN
        ref _result_Win;
#else
        ref _result_Mac;
#endif
        results = new sResult[_totalCount];
        for (int i = 0; i < _totalCount; i++)
        {
            results[i].result_imp_f = FloatOperation(_floatList_A[i], _floatList_B[i]);
            results[i].result_exp_f = FloatPreciseOperation(_floatList_A[i], _floatList_B[i]);
            results[i].result_imp_d = DoubleOperation(_floatList_A[i], _floatList_B[i]);
            results[i].result_exp_d = DoublePreciseOperation(_floatList_A[i], _floatList_B[i]);
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }


    [ContextMenu("CheckResultLocal")]
    public void CheckResultLocal()
    {
        for (int i = 0; i < _totalCount; ++i)
        {
#if UNITY_EDITOR_WIN
            Debug.Assert(_result_Win[i].result_imp_d == _result_Win[i].result_exp_d, $"{i:000}: result_d is different.");
            Debug.Assert(_result_Win[i].result_imp_f == _result_Win[i].result_exp_f, $"{i:000}: result_f is different.");
#else
            Debug.Assert(_result_Mac[i].result_imp_d == _result_Mac[i].result_exp_d, $"{i:000}: result_d is different.");
            Debug.Assert(_result_Mac[i].result_imp_f == _result_Mac[i].result_exp_f, $"{i:000}: result_f is different.");
#endif
        }
    }

    [ContextMenu("CheckResultGlobal")]
    public void CheckResultGlobal()
    {
        if(_result_Mac.Length != _result_Win.Length)
        {
            return;
        }
        for (int i = 0; i < _totalCount; ++i)
        {
            Debug.Assert(_result_Win[i].result_imp_d == _result_Mac[i].result_imp_d, $"{i:000}: result_imp_d is different.");
            Debug.Assert(_result_Win[i].result_imp_f == _result_Mac[i].result_imp_f, $"{i:000}: result_imp_f is different.");
            Debug.Assert(_result_Win[i].result_exp_d == _result_Mac[i].result_exp_d, $"{i:000}: result_exp_d is different.");
            Debug.Assert(_result_Win[i].result_exp_f == _result_Mac[i].result_exp_f, $"{i:000}: result_exp_f is different.");
        }
    }

    const int iterCount = 10000;
    [BurstCompile(FloatPrecision.High, FloatMode.Default, CompileSynchronously = true)]
    public float FloatOperation(float a, float b)
    {
        float result = 0f;
        result = a * b * b * b * b * b * b* b;
        return result;
    }
    [BurstCompile(FloatPrecision.High, FloatMode.Default, CompileSynchronously = true)]
    public float FloatPreciseOperation(float a, float b)
    {
        double result = 0d;
        result = (double)a * b * b * b * b * b * b * b;
        return (float)result;
    }
    [BurstCompile(FloatPrecision.High, FloatMode.Default, CompileSynchronously = true)]
    public double DoubleOperation(float a, float b) 
    {
        double result = a;
        result += b;
        return result;
    }
    [BurstCompile(FloatPrecision.High, FloatMode.Default, CompileSynchronously = true)]
    public double DoublePreciseOperation(float a, float b)
    {
        double result = a + b;
        return result;
    }
}
