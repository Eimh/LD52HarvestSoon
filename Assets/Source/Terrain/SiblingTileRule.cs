using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SiblingRuleTile : RuleTile {
    public enum SiblingGroup {
        Soil
    }

    public SiblingGroup siblingGroup;

    public override bool RuleMatch(int neighbor, TileBase tile) {
        if (tile is RuleOverrideTile)
            tile = (tile as RuleOverrideTile).m_InstanceTile;

        switch (neighbor) {
            case TilingRule.Neighbor.This: {
                    return tile is SiblingRuleTile
                        && (tile as SiblingRuleTile).siblingGroup == this.siblingGroup;
                }
            case TilingRule.Neighbor.NotThis: {
                    return !(tile is SiblingRuleTile
                        && (tile as SiblingRuleTile).siblingGroup == this.siblingGroup);
                }
        }

        return base.RuleMatch(neighbor, tile);
    }
}