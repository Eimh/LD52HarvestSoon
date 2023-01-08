using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Axe : Item {
    public Axe() {
        Id = 2;
    }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        if (world.TryAxe(position)) {
            player.inventory.Add(new Log());
            SoundManager.PlaySound(SoundManager.Effect.Axe);
        } else { 
            SoundManager.PlaySound(SoundManager.Effect.Invalid);
        }
        return false;
    }

    public override bool HasCount() {
        return false;
    }
}

