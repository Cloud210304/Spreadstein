using UnityEngine;
using UnityEngine.SceneManagement;

public class AuraEventManager : MonoBehaviour
{
    [Header("References")]
    public CurrencyManager currencyManager;
    public GameObject eventWarningObject; // Assign in Inspector (starts hidden)

    [Header("Event Settings")]
    public float checkInterval = 5f;      // How often we roll for event
    public float baseChance = 0.05f;      // Base 5% chance
    public float auraMultiplier = 0.01f;  // Each Aura adds 1% chance
    public float eventDuration = 30f;     // 30 seconds to click button

    private float timer;
    private bool eventActive = false;
    private float eventTimer;

    void Start()
    {
        if (eventWarningObject != null)
            eventWarningObject.SetActive(false);

        timer = checkInterval;
    }

    void Update()
    {
        if (!eventActive)
        {
            HandleEventRoll();
        }
        else
        {
            HandleActiveEvent();
        }
    }

    void HandleEventRoll()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = checkInterval;

            int aura = currencyManager.GetCurrentAura();

            float finalChance = baseChance + (aura * auraMultiplier);

            if (Random.value <= finalChance)
            {
                StartEvent();
            }
        }
    }

    void StartEvent()
    {
        eventActive = true;
        eventTimer = eventDuration;

        if (eventWarningObject != null)
            eventWarningObject.SetActive(true);

        Debug.Log("Aura Event Started! Click the button!");
    }

    void HandleActiveEvent()
    {
        eventTimer -= Time.deltaTime;

        if (eventTimer <= 0f)
        {
            GameOver();
        }
    }

    // Hook this to your UI Button
    public void OnPlayerClickedButton()
    {
        if (!eventActive) return;

        Debug.Log("Player survived the Aura Event!");

        eventActive = false;

        if (eventWarningObject != null)
            eventWarningObject.SetActive(false);
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

    }
}