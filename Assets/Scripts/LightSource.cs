using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float fadeDuration = 5.0f;
    public float minIntensity = 0.0f;

    private Light lightSource;
    private float initialIntensity;
    private float timer;

    void Start()
    {
        lightSource = GetComponent<Light>();
        initialIntensity = lightSource.intensity;
        timer = fadeDuration;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            float newIntensity = Mathf.Lerp(minIntensity, initialIntensity, timer / fadeDuration);
            lightSource.intensity = newIntensity;
        }
        else
        {
            lightSource.intensity = minIntensity;
            lightSource.enabled = false;
        }
    }
}
