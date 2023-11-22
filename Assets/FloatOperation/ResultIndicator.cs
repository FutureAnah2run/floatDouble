using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultIndicator : MonoBehaviour
{
    [SerializeField]
    private Text field;
    [SerializeField]
    private Text contents;

    [ContextMenu("SetSerialized")]
    public void SetSerialized()
    {
        Transform tr = transform;
        field = tr.Find("Field")?.GetComponent<Text>();
        contents = tr.Find("Contents")?.GetComponent<Text>();
    }

    public void SetFieldText(string text)
    {
        if (field)
        {
            field.text = text;
        }
    }

    public void ClearResult()
    {
        contents.text = string.Empty;
    }

    public void SetResult(double floatTime, double doubleTime)
    {
        if(contents)
        {
            double efficiency = (doubleTime - floatTime) / floatTime * 100;
            contents.text = $"float: {floatTime:0.00000}s\n double: {doubleTime:0.00000}s\n efficiency: {efficiency:0.000}%";
        }
    }
}
