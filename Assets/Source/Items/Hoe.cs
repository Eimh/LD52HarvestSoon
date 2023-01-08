using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hoe : Item {
    public Hoe() {
        Id = 0;
    }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        if (world.TryHoe(position)) {
            SoundManager.PlaySound(SoundManager.Effect.Hoe);
        } else {
            SoundManager.PlaySound(SoundManager.Effect.Invalid);
        }
        return false;
    }
    public override bool HasCount() {
        return false;
    }
}

