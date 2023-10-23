using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float fadeDuration = 5.0f;
    public float minIntensity = 0.0f;
    public float refillThreshold = 0.2f;
    public float maxIntensity = 1.0f; // Maximum light intensity capacity.

    private Light lightSource;
    private float initialIntensity;
    private float timer;
    private bool needsRefill;

    public float refillDistance = 2.0f; // The distance at which the player can refill the light.

    private bool isRefilling; // Flag to indicate if the player is currently refilling.
    private bool isLightOn; // Flag to indicate if the light is turned on.

    void Start()
    {
        lightSource = GetComponent<Light>();
        initialIntensity = lightSource.intensity;
        timer = fadeDuration;
        needsRefill = false;
        isRefilling = false;
        isLightOn = true; // Turn on the light at the start.
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

        if (Input.GetKey(KeyCode.E) && IsPlayerNearby() && !isRefilling)
        {
            StartRefill();
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

        // Display a debug log message to indicate that the refill is going up.
        Debug.Log("Light refilled by " + amountToRefill.ToString("F2"));

        // Keep the light on after refilling.
        isLightOn = true;
    }

    bool IsPlayerNearby()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, refillDistance);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player")) // Adjust the tag as per your player's tag.
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
