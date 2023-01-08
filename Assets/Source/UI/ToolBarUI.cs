using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ToolBarUI : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private ItemUI[] items;
    private Inputs inputs;
    [SerializeField]
    private DayManager dayManager;
    // Start is called before the first frame update
    void Start() {
        inputs = new Inputs();
        inputs.Player.ToolbarScroll.performed += ctx => OnToolbarScroll(ctx.ReadValue<float>());      
        inventory = GetComponent<Inventory>();
        inventory.Subscribe(OnSelectorChanged, OnItemChanged);
        dayManager.Subscribe(OnSleep, OnWake);
    }

    private void OnToolbarScroll(float direction) {
        SoundManager.PlaySound(SoundManager.Effect.Select);
        if (direction < 0) {
            inventory.SetSelected(inventory.GetSelected()-1);
        }
        else if (direction > 0) {
            inventory.SetSelected(inventory.GetSelected() + 1);
        }
    }

    private void OnSelectorChanged() {
        Debug.Log("Toolbar seelector");
        foreach (ItemUI item in items) {
            item.Deselect();
        }
        items[inventory.GetSelected()].Select();
    }

    private void OnItemChanged() {
        Debug.Log("toolbar");
        int i = 0;
        foreach (ItemUI item in items) {
            item.SetItem(inventory.GetItem(i));
            i++;
        }
    }

    private void OnSleep() {
        inputs.Disable();
    }
    private void OnWake() {
        inputs.Enable();
    }

    private void OnDestroy() {
        inputs.Player.Disable();
    }
}
