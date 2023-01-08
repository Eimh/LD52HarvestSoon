using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Scythe : Item {
    public Scythe() {
        Id = 1;
    }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        Item item = world.TryScythe(position);
        if (item != null) {
            player.inventory.Add(item);
            SoundManager.PlaySound(SoundManager.Effect.Scythe);
        }
        else {
            SoundManager.PlaySound(SoundManager.Effect.Invalid);
        }
        return false;
    }
    public override bool HasCount() {
        return false;
    }
}

