using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text count;
    [SerializeField]
    private GameObject countPanel;
    [SerializeField]
    private Image selector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetItem(Item item) {
        if (item == null || (item.Count == 0 && item is not WateringCan)) {
            icon.enabled = false;
            countPanel.SetActive(false);
        } else {
            icon.enabled = true;
            icon.sprite = item.Icon;
            countPanel.SetActive(item.HasCount());
            count.text = item.Count.ToString();
            
        }
    }

    public void Deselect() {
        selector.enabled = false;
    }

    public void Select() {
        selector.enabled = true;
    }
}
