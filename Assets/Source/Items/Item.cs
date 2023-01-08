using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Item {
    public Item() {
        Count = 1;
    }
    public int Id { get; protected set; }
    public int Count { get; set; }
    public string Name { get; set; }
    public virtual bool HasCount() { return true; }
    public Sprite Icon { get { return ItemManager.GetIcon(Id); }}
    public abstract bool Use(Vector2Int position, World world, PlayerController player);
}

