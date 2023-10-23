using UnityEngine;

public class OilCollector : MonoBehaviour
{
    public float oilCapacity = 100.0f;
    public float refillRate = 10.0f;

    private float currentOilAmount;

    private LightSource[] lightSources;

    void Start()
    {
        currentOilAmount = oilCapacity;

        lightSources = FindObjectsOfType<LightSource>();
    }

    void Update()
    {
        if (currentOilAmount > 0)
        {
            foreach (LightSource lightSource in lightSources)
            {
                if (lightSource.NeedsRefill())
                {
                    float amountToRefill = Mathf.Min(currentOilAmount, refillRate * Time.deltaTime);
                    lightSource.RefillLight(amountToRefill);
                    currentOilAmount -= amountToRefill;
                }
            }
        }
    }

    public void CollectOil(float amount)
    {
        currentOilAmount = Mathf.Clamp(currentOilAmount + amount, 0, oilCapacity);
    }
}
