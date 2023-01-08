using UnityEngine;

public abstract class Food : Item {
    public int Heal { get; protected set; }
    public override bool Use(Vector2Int position, World world, PlayerController player) {
        if (Count > 0) {
            if (player.hunger.Eat(Heal)) {
                SoundManager.PlaySound(SoundManager.Effect.Eat);
                return true;
            }
        }
        SoundManager.PlaySound(SoundManager.Effect.Invalid);
        return false;
    }
}
