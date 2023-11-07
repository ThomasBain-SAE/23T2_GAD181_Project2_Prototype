using UnityEngine;
using UnityEngine.AI;
using System.Collections;
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

    private bool isLightOn; // Flag to indicate if the light is turned on
    private NavMeshObstacle obstacle;
    public float lightOnDuration = 10.0f;

    void Start()
    {
        lightSource = GetComponent<Light>();
        initialIntensity = lightSource.intensity;
        lightSource.intensity = 0;
        timer = fadeDuration;
        needsRefill = false;
        isLightOn = false;
        obstacle = GetComponent<NavMeshObstacle>();
    }

    void Update()
    {
        HandleRefillInput();
        isLightOn = lightSource.intensity > 0f;
        obstacle.enabled = isLightOn;
    }

    void HandleRefillInput()
    {
        if (IsPlayerNearby()&& !isLightOn)
        {
            if (Input.GetKey(KeyCode.E))
            {
                TurnOnLamp(1);
            }
        }
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

    void TurnOnLamp( int amountToRefill)
    {
        if (isLightOn) { return; }
        float newIntensity = lightSource.intensity + amountToRefill;
        lightSource.intensity = Mathf.Clamp(newIntensity, minIntensity, maxIntensity);

        // Display a debug log message to indicate that the refill is going up
        Debug.Log("Light refilled by " + amountToRefill.ToString("F2"));

        StartCoroutine(FadeLight(lightSource.intensity, 0f, lightOnDuration));
    }

    IEnumerator FadeLight(float startIntensity, float endIntensity, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newIntensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / duration);
            lightSource.intensity = newIntensity;
            yield return null;
        }
        lightSource.intensity = endIntensity;
    }
}
