using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    bool hoseOn = false;
    public ParticleSystem particles;
    private ParticleSystem.MainModule main;

    public float waterSupplyDuration = 60f;
    private float waterRemaining;
    public TMP_Text waterUIText;
    public AudioSource hoseSource;

    public RectTransform waterBar;

    void Start()
    {
        if (particles != null)
        {
            particles.Stop(true);
            main = particles.main;
        }

        waterRemaining = waterSupplyDuration;
        UpdateWaterUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (particles != null)
        {
            if (Input.GetMouseButtonDown(0) && !hoseOn && waterRemaining > 0f)
            {
                if (hoseSource != null)
                {
                    hoseSource.Play();
                }
                particles.Play();
                hoseOn = true;
            }

            if (Input.GetMouseButtonUp(0) && hoseOn)
            {
                if (hoseSource != null)
                {
                    hoseSource.Pause();
                }
                particles.Stop();
                hoseOn = false;
            }
            // drains water continuously
            if (hoseOn && waterRemaining > 0f)
            {
                waterRemaining -= Time.deltaTime;
                if (waterRemaining <= 0f)
                {
                    waterRemaining = 0f;
                    particles.Stop();
                    hoseOn = false;
                }
                UpdateWaterUI();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hydrant"))
        {
            waterRemaining = waterSupplyDuration;
            UpdateWaterUI();
            Debug.Log("Water supply refilled at hydrant!");
        }
    }

    void UpdateWaterUI()
    {
        if (waterUIText != null)
        {
            float percentage = (waterRemaining / waterSupplyDuration) * 100f;
            waterUIText.text = "Water: " + percentage.ToString("F0") + "%";
        }

        if (waterBar != null)
        {
            float waterPercent = waterRemaining / waterSupplyDuration;
            waterBar.sizeDelta = new Vector2(225 * waterPercent, 50);
        }
    }
}
