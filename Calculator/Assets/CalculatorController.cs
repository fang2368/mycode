using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalculatorController : MonoBehaviour
{
    public CalculatorModel model;  // 数据模型
    public CalculatorUI view;    // 用户界面

    // 按钮文本映射
    public Button[] numberButtons;  // 数字按钮数组
    public Button[] operatorButtons;  // 运算符按钮数组
    public Button delButton;  // 删除按钮
    public Button acButton;   // 清除按钮
    public Button dotButton;  // 小数点按钮
    public Button equalsButton;  // 计算按钮

    public void Start()
    {
        // 打印出按钮事件监听器的数量
        Debug.Log("Number Button 0 Listeners: " + numberButtons[0].onClick.GetPersistentEventCount());

        // 确保按钮点击事件只绑定一次
        foreach (Button btn in numberButtons)
        {
            btn.onClick.RemoveAllListeners();  // 移除所有监听器
            
            string input = btn.GetComponentInChildren<TextMeshProUGUI>().text;

            if (string.IsNullOrEmpty(input))
            {
                Debug.Log("000");
            }
            else
            {
                btn.onClick.AddListener(() => OnNumberButtonClick(input));
            }
                
        }

        foreach (Button btn in operatorButtons)
        {
            btn.onClick.RemoveAllListeners();  // 移除所有监听器

            string input = btn.GetComponentInChildren<TextMeshProUGUI>().text;

            if (string.IsNullOrEmpty(input))
            {
                Debug.Log("000");
            }
            else
            {
                btn.onClick.AddListener(() => OnOperatorButtonClick(input));
            }
        }

        // 删除按钮
        delButton.onClick.RemoveAllListeners();  // 移除所有监听器
        delButton.onClick.AddListener(OnDeleteButtonClick);

        // 清除按钮
        acButton.onClick.RemoveAllListeners();  // 移除所有监听器
        acButton.onClick.AddListener(OnClearButtonClick);

        // 小数点按钮
        dotButton.onClick.RemoveAllListeners();  // 确保只绑定一次
        dotButton.onClick.AddListener(OnDotButtonClick);

        // 等号按钮
        equalsButton.onClick.RemoveAllListeners();
        equalsButton.onClick.AddListener(OnEqualsButtonClick);
    }

    // 数字按钮点击事件
    public void OnNumberButtonClick(string number)
    {
        //Debug.Log(number);
        Debug.Log("Number Button: " + number + " Clicked");  // 输出按钮名称
        model.Add(number);
        view.SetResultText(model.GetExpression());
    }

    // 运算符按钮点击事件
    public void OnOperatorButtonClick(string operatorSymbol)
    {
        //Debug.Log(operatorSymbol);
        Debug.Log("Operator Button: " + operatorSymbol + " Clicked");  // 输出按钮名称

        if (operatorSymbol == "×")
           operatorSymbol = "*";
        else if(operatorSymbol == "÷")
           operatorSymbol = "/";

        model.Add(operatorSymbol);
        view.SetResultText(model.GetExpression());
    }

    // 小数点按钮点击事件
    public void OnDotButtonClick()
    {
        Debug.Log("Dot Button Clicked");

        model.Add(".");
        view.SetResultText(model.GetExpression());
    }

    // 删除按钮点击事件
    public void OnDeleteButtonClick()
    {
        Debug.Log("Delete Button Clicked");

        model.DeleteLastCharacter();
        view.SetResultText(model.GetExpression());
    }

    // 清除按钮点击事件
    public void OnClearButtonClick()
    {
        Debug.Log("Clear Button Clicked");

        model.ClearExpression();
        view.SetResultText("0");
    }

    // 等号按钮点击事件，执行计算
    public void OnEqualsButtonClick()
    {
        Debug.Log("Equal Button Clicked");

        if (model.ComputeResult())
        {
            view.SetResultText(model.GetExpression());
        }
        else 
        {
            view.SetResultText("Error");
        }
    }

    // 监听键盘输入（可选）
    public void Update()
    {
        // 监听键盘输入
        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))  // 如果是数字
            {
                OnNumberButtonClick(c.ToString());
            }
            else if (c == '*')  // 处理乘法
            {
                OnOperatorButtonClick("×");  // 使用 * 代替 ×
            }
            else if (c == '/')  // 处理除法
            {
                OnOperatorButtonClick("÷");  // 使用 / 代替 ÷
            }
            else if ("+-".Contains(c.ToString()))  // 处理加减法
            {
                OnOperatorButtonClick(c.ToString());
            }
            else if (c == '\b')  // 回退键 (Backspace)
            {
                OnDeleteButtonClick();
            }
            else if (c == '.')   //小数点
            {
                OnDotButtonClick();
            }
            else if (c == '=')  // 回车键或等号
            {
                OnEqualsButtonClick();
            }
        }

        // 监听回车键（Enter）以触发等号
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnEqualsButtonClick();
        }
    }
}
