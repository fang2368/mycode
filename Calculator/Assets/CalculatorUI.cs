using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CalculatorUI : MonoBehaviour
{
    public TextMeshProUGUI resultText;  // 显示输入和结果的Text

    public void SetResultText(string text)
    {
        resultText.text = text;
    }
}
