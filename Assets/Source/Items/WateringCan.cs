using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WateringCan : Item {
    public WateringCan() {
        Id = 10;
        Count = 5;
    }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        if (Count > 0) {
            if (world.TryWater(position)) {
                SoundManager.PlaySound(SoundManager.Effect.UseWateringCan);
                return true;
            }
        }
        if (world.TryFill(position)) {
            Count += 2;
            if (Count > 10) {
                SoundManager.PlaySound(SoundManager.Effect.Invalid);
            }
            else {
                SoundManager.PlaySound(SoundManager.Effect.FillWateringCan);
            }
            Count = Math.Clamp(Count, 0, 10);
            return true;
        }
        SoundManager.PlaySound(SoundManager.Effect.Invalid);
        return false;
    }
}

