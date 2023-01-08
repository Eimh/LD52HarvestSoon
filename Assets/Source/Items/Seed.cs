using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Seed : Item {
    public Seed() {
        Count = 1;
    }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        if (Count > 0) {
            if (world.TryPlant(position, this)) {
                SoundManager.PlaySound(SoundManager.Effect.Seeds);
                return true;
            }
        }
        SoundManager.PlaySound(SoundManager.Effect.Invalid);
        return false;
    }
}

