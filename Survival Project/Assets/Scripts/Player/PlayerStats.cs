using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float Health;
    public float maxHealth = 100f;
    [Space]
    public float Hunger;
    public float maxHunger = 100f;
    [Space]
    public float Thirst;
    public float maxThirst = 100f;

    [Header("Stats Depletion")]
    public float HungerDepletion = 0.5f;
    public float ThirstDepletion = 0.75f;

    [Header("Stats Damages")]
    public float HungerDamage = 1.5f;
    public float ThirstDamage = 2.25f;

    [Header("UI")]
    public StatsBar HealthBar;
    public StatsBar HungerBar;
    public StatsBar ThirstBar;


    private void Start()
    {
        Health = maxHealth;
        Hunger = maxHunger;
        Thirst = maxThirst;
    }

    private void Update()
    {
        UpdateStats();
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update Health Bar
        if (HealthBar != null && HealthBar.bar != null)
        {
            HealthBar.numberText.text = Health.ToString("f0");
            HealthBar.bar.fillAmount = Mathf.Clamp01(Health / maxHealth);
        }

        // Update Hunger Bar
        if (HungerBar != null && HungerBar.bar != null)
        {
            HungerBar.numberText.text = Hunger.ToString("f0");
            HungerBar.bar.fillAmount = Mathf.Clamp01(Hunger / maxHunger);
        }

        // Update Thirst Bar
        if (ThirstBar != null && ThirstBar.bar != null)
        {
            ThirstBar.numberText.text = Thirst.ToString("f0");
            ThirstBar.bar.fillAmount = Mathf.Clamp01(Thirst / maxThirst);
        }
    }

    public void UpdateStats()
    {
        if (Health <= 0)
            Health = 0;
        if (Health >= maxHealth)
            Health = maxHealth;

        if (Thirst <= 0)
            Thirst = 0;
        if (Hunger >= maxHunger)
            Hunger = maxHunger;

        if (Hunger <= 0)
            Hunger = 0;
        if (Thirst >= maxThirst)
            Thirst = maxThirst;

        // DAMAGES

        if (Hunger <= 0)
            Health -= HungerDamage * Time.deltaTime;

        if (Thirst <= 0)
            Health -= ThirstDamage * Time.deltaTime;

        // DEPLETION

        if(Hunger > 0)
            Hunger -= HungerDepletion * Time.deltaTime;

        if (Thirst > 0)
            Thirst -= ThirstDamage * Time.deltaTime;

    }

    
    
        

}
