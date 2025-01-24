using System;
using System.Data;
using UnityEngine;

public class CalculatorModel : MonoBehaviour
{
    private string expression = "";  // 当前输入的表达式

    public string GetExpression()
    { 
        return expression; 
    }

    // 计算结果
    public bool ComputeResult()
    {
        try
        {
            // 使用 C# 的 System.Data.DataTable 来计算表达式
            var result = new System.Data.DataTable().Compute(expression, null);
            expression = result.ToString();  // 保存计算结果，继续计算
            return true;
        }
        catch (System.Exception ex)
        {
            expression = "";
            Debug.LogError("计算错误: " + ex.Message);  // 打印详细的错误信息
            return false;   
        }
    }

    // 添加到表达式
    public void Add(string input)
    {
        //Debug.Log(input);
        expression += input;
    }

    // 删除表达式的最后一个字符
    public void DeleteLastCharacter()
    {
        if (expression.Length > 0)
        {
            expression = expression.Substring(0, expression.Length - 1);
        }
    }

    // 清除表达式
    public void ClearExpression()
    {
        expression = "";
    }
}
