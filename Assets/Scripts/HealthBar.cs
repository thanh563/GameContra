using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image redbar;

    public void UpdateBar(int currentValue, int maxValue)
    {
        redbar.fillAmount = (float)currentValue / (float)maxValue;
	}
}
