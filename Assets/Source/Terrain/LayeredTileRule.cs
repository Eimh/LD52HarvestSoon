using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class LayeredRuleTile : RuleTile<LayeredRuleTile.Neighbor> {
    public enum Layers {
        Water,
        Sand,
        Light,
        Dark,
        Rock
    }

    public Layers layer;
    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Higher = 3;
        public const int Lower = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        if (tile is RuleOverrideTile)
            tile = (tile as RuleOverrideTile).m_InstanceTile;

        switch (neighbor) {
            case Neighbor.Lower:
                return tile is LayeredRuleTile
                    && (tile as LayeredRuleTile).layer < this.layer;
            case Neighbor.Higher:
                return tile is LayeredRuleTile
                    && (tile as LayeredRuleTile).layer > this.layer;
            case TilingRule.Neighbor.This: {
                    return tile is LayeredRuleTile
                        && (tile as LayeredRuleTile).layer == this.layer;
                }
        }

        return base.RuleMatch(neighbor, tile);
    }
}