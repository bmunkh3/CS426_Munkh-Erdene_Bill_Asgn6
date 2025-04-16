using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public TMP_Text healthText;
    public int maxHealth = 100;
    private int currentHealth;

    public RectTransform healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth == 0)
        {
            SceneManager.LoadScene("Building");
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = $"Health: {currentHealth}";
        float healthPercent = (float)currentHealth / maxHealth;
        healthBar.sizeDelta = new Vector2(225 * healthPercent, 50);
    }
}

