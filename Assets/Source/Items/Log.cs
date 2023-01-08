using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Log : Item {
    public Log() {
        Id = 9;
    }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        if (Count > 0) {
            if (world.TryPlaceLog(position)) {
                return true;
            }
        }
        SoundManager.PlaySound(SoundManager.Effect.Invalid);
        return false;
    }
}

