using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI KeyCount;
    

    void Start()
    {
        KeyCount = GetComponent<TextMeshProUGUI>();
        
    }

    public void UpdateKeyCount(PlayerInventory playerInventory)
    {
        KeyCount.text = playerInventory.NumberOfKeys.ToString();
    }
    
}
