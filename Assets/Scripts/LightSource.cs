using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float fadeDuration = 5.0f;
    public float minIntensity = 0.0f;
    public float refillThreshold = 0.2f;
    public float maxIntensity = 1.0f; // Maximum light intensity capacity

    private Light lightSource;
    private float initialIntensity;
    private float timer;
    private bool needsRefill;

    public float refillDistance = 2.0f; // The distance at which the player can refill the light

    private bool isRefilling; // Flag to indicate if the player is currently refilling
    private bool isLightOn; // Flag to indicate if the light is turned on
    private float timeLightHasBeenOn; // Timer to track how long the light has been on
    private bool hasBeenRefilled; // Flag to indicate if the light has been refilled

    void Start()
    {
        lightSource = GetComponent<Light>();
        initialIntensity = lightSource.intensity;
        timer = fadeDuration;
        needsRefill = false;
        isRefilling = false;
        isLightOn = true; // Turn on the light at the start
        timeLightHasBeenOn = 0.0f;
        hasBeenRefilled = false;
    }

    void Update()
    {
        if (timer > 0 && isLightOn)
        {
            timer -= Time.deltaTime;
            float newIntensity = Mathf.Lerp(minIntensity, initialIntensity, timer / fadeDuration);
            lightSource.intensity = newIntensity;

            if (newIntensity < refillThreshold * initialIntensity)
            {
                needsRefill = true;
            }
        }
        else if (isLightOn)
        {
            lightSource.intensity = minIntensity;
            needsRefill = false;
        }

        // Check if the light is on and track how long it has been on
        if (isLightOn)
        {
            timeLightHasBeenOn += Time.deltaTime;

            // If the light has been on for 10 seconds, turn it off
            if (timeLightHasBeenOn >= 10.0f)
            {
                lightSource.intensity = minIntensity;
                isLightOn = false;
                timeLightHasBeenOn = 0.0f; // Reset the timer
            }
        }

        if (IsPlayerNearby() && !isRefilling && !isLightOn && !hasBeenRefilled)
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartRefill();
                hasBeenRefilled = true; // Set the flag to prevent continuous refilling
            }
        }

        if (!isRefilling)
        {
            StopRefill();
        }

        if (isRefilling)
        {
            RefillLight(1);
        }
    }

    public bool NeedsRefill()
    {
        return needsRefill;
    }

    public void RefillLight(float amountToRefill)
    {
        float newIntensity = lightSource.intensity + amountToRefill;
        lightSource.intensity = Mathf.Clamp(newIntensity, minIntensity, maxIntensity);

        // Display a debug log message to indicate that the refill is going up
        Debug.Log("Light refilled by " + amountToRefill.ToString("F2"));

        // Keep the light on after refilling
        isLightOn = true;
        timeLightHasBeenOn = 0.0f; // Reset the timer after refilling
    }

    bool IsPlayerNearby()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, refillDistance);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player")) // Adjust the tag as per your player's tag
            {
                return true;
            }
        }
        return false;
    }

    void StartRefill()
    {
        isRefilling = true;
    }

    void StopRefill()
    {
        isRefilling = false;
    }
}