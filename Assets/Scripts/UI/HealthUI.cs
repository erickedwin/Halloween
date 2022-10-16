using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private GameObject healthTarget;

    private IHealthSystem healthSystem;

    [SerializeField]
    private TextMeshProUGUI textHealth;

    [SerializeField]
    private Image healthBar;

    private void Start()
    {
        if (!healthTarget.TryGetComponent(out healthSystem))
        {
            Debug.LogError("Error: El objeto que esta apuntando no tiene un sistema de salud establecido.");
        }

        StartUI();
    }

    public void UpdateHealth()
    {
        UpdateHealthBar();
        UpdateHealthText();
    }

    private void StartUI()
    {
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged += UpdateHealth;
            UpdateHealthBar();
            UpdateHealthText();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.DOFillAmount(healthSystem.GetHealth() / healthSystem.GetMaxHealth(), 0.5f);
    }

    private void UpdateHealthText()
    {
        textHealth.text = $"{healthSystem.GetHealth()}/{healthSystem.GetMaxHealth()}";
    }

    private void OnEnable()
    {
        StartUI();
    }

    private void OnDisable()
    {
        healthSystem.OnHealthChanged -= UpdateHealth;
    }
}