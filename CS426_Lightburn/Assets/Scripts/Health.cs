using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hurtSound;
    public TMP_Text healthText;
    public int maxHealth = 100;
    private int currentHealth;
    public int regenAmt = 10;
    private float regenDelay = 5f;
    private float regenTimer; // counts time since last damage taken

    public RectTransform healthBar;
    public Image healthImpact;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        if (currentHealth < maxHealth)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenDelay)
            {
                regenTimer = 0f;
                currentHealth = Mathf.Clamp(currentHealth + regenAmt, 0, maxHealth);
                UpdateHealthUI();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        regenTimer = 0f;

        if (hurtSound != null) {
            audioSource.PlayOneShot(hurtSound);
        }

        if (currentHealth == 0)
        {
            SceneManager.LoadScene("Building");
        } 
    }

    void UpdateHealthUI()
    {
        healthText.text = $"Health: {currentHealth}%";
        float healthPercent = (float)currentHealth / maxHealth;
        healthBar.sizeDelta = new Vector2(225 * healthPercent, 50);

        if (healthImpact != null)
        {
            float alpha = 1f - healthPercent;
            Color c = healthImpact.color;
            c.a = alpha;
            healthImpact.color = c;
        }
    }
}

