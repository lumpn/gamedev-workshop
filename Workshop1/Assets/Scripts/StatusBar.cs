using UnityEngine;
using UnityEngine.UI;

public sealed class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private BoundedFloat data;

    void Update()
    {
        slider.maxValue = data.maxValue;
        slider.value = data.value;
    }
}
