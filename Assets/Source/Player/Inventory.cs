using System;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int size;
    private Item[] items;

    private int selected = 0;
    private UnityEvent selectorChanged;
    private UnityEvent ItemChanged;

    void Start() {
        selectorChanged = new UnityEvent();
        ItemChanged = new UnityEvent();
        items = new Item[size];
        Add(new Hoe());
        Add(new Scythe());
        Add(new Axe());
        Add(new WateringCan());
        Add(new Log() { Count = 2 });
        //Add(new TomatoSeed());
        //Add(new MelonSeed());
        //Add(new WheatSeed());
        ItemChanged.Invoke();
    }

    public bool Add(Item item) {
        int firstEmpty = -1;
        for (int i = 0; i < size; i++) {
            if (items[i] != null && items[i].Count > 0) {
                if (items[i].Id == item.Id) {
                    items[i].Count += item.Count;
                    items[i].Count = Math.Clamp(items[i].Count, 0, 9);
                    ItemChanged.Invoke();
                    return true;
                }
            } else if (firstEmpty < 0 && items[i] is not WateringCan) {
                firstEmpty = i;
            }
        }
        if (firstEmpty >= 0) {
            items[firstEmpty] = item;
            ItemChanged.Invoke();
            return true;
        }
        return false;
    }

    public void Remove(int index) {
        items[index].Count--;
        ItemChanged.Invoke();
    }

    public void Subscribe(UnityAction onSelector, UnityAction onItem) {
        selectorChanged.AddListener(onSelector);
        ItemChanged.AddListener(onItem);
        onSelector();
        onItem();
    }

    public void SetSelected(int index) {
        index = index % size;
        if (index < 0) index += size;
        selected = index;
        selectorChanged.Invoke();
    }

    public int GetSelected() {
        return selected;
    }

    public Item GetItem(int index) { 
        return items[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
