using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Update()
    {
        var player = Player.main;
        slider.maxValue = player.maxHealth;
        slider.value = player.health;
    }
}
