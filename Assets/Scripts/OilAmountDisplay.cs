using UnityEngine;
using TMPro;

public class OilAmountDisplay : MonoBehaviour
{
    public TextMeshProUGUI oilText; // Reference to the TextMeshPro Text element for displaying player's oil amount
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>(); // Get the PlayerController script
    }

    private void Update()
    {
        if (playerController != null)
        {
            // Update the TextMeshPro Text element to display the player's oil amount.
            oilText.text = "Oil: " + playerController.GetOilAmount().ToString("F1") + " / " + playerController.maxOilCapacity;
        }
    }
}
