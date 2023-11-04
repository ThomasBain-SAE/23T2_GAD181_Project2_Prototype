using UnityEngine;

public class OilCollectionPoint : MonoBehaviour
{
    public float oilAmountToCollect = 5.0f;
    private OilCollector oilCollector;

    private void Start()
    {
        oilCollector = FindObjectOfType<OilCollector>(); // Find the OilCollector script in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the CollectOil method on the OilCollector script to add oil to the player's inventory.
            oilCollector.CollectOil(oilAmountToCollect);

            // Disable or destroy the collection point if needed.
            gameObject.SetActive(false);
            // Alternatively, you can destroy the collection point using: Destroy(gameObject);
        }
    }
}

