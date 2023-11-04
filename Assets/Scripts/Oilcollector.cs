using UnityEngine;

public class OilCollector : MonoBehaviour
{
    public float maxOilCapacity = 10.0f;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void CollectOil(float amount)
    {
        float newOilAmount = Mathf.Clamp(playerController.GetOilAmount() + amount, 0, maxOilCapacity);
        playerController.SetOilAmount(newOilAmount);
    }

    // You may have other methods and variables related to oil collection and management here.
}