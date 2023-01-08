using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TerrainGenerator))]
public class World : MonoBehaviour
{
    [SerializeField]
    private int size = 200;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private DayManager dayManager;
    [SerializeField]
    private Tilemap tilemapGround;
    [SerializeField]
    private Tilemap tilemapSoil;
    [SerializeField]
    private Tilemap tilemapPlants;
    [SerializeField]
    private TileBase[] tiles;
    [SerializeField]
    private TileBase lightGrass;
    [SerializeField]
    private TileBase darkGrass;
    [SerializeField]
    private TileBase darkHoed;
    [SerializeField]
    private TileBase darkWet;
    [SerializeField]
    private TileBase[] tomato;
    [SerializeField]
    private TileBase[] melon;
    [SerializeField]
    private TileBase[] wheat;
    [SerializeField]
    private TileBase[] harvestables;
    [SerializeField]
    private TileBase[] ramps;

    private TerrainGenerator generator;
    

    void Start() {
        generator = GetComponent<TerrainGenerator>();
        generator.SetHeight(tiles.Length);
        generator.SetSize(size);
        generator.Pregenerate();
        Generate();
        Debug.Log("World Wake");
        dayManager.StartDay();
    }

    private void TryGrowPlant(Vector3Int position, TileBase[] stages) {
        TileBase plant = tilemapPlants.GetTile(position);
        for (int p = 0; p < stages.Length - 1; p++) {
            if (plant.Equals(stages[p])) {
                tilemapPlants.SetTile(position, stages[p+1]);
            }
        }
    }

    public void OvernightProcess() {
        Debug.Log("procssing");
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Vector3Int pos = new Vector3Int(i, j);
                if (tilemapSoil.HasTile(pos) && tilemapSoil.GetTile(pos).Equals(darkWet)) {
                    tilemapSoil.SetTile(pos, darkHoed);
                    if (tilemapPlants.HasTile(pos)) {
                        TryGrowPlant(pos, tomato);
                        TryGrowPlant(pos, melon);
                        TryGrowPlant(pos, wheat);
                    }
                }
                //tilemapGround.SetTile(new Vector3Int(i, j), tiles[generator.GetTile(i, j)]);
            }
        }
    }

    public bool TryHoe(Vector2Int position) {
        Vector3Int position3 = new Vector3Int(position.x, position.y, 0);
        TileBase current = tilemapGround.GetTile(position3);
        if (current == null) return false;
        if (tilemapSoil.HasTile(position3)) return false;
        if (current.Equals(lightGrass)) {
            tilemapSoil.SetTile(position3, darkHoed);
            return true;
        }
        else if (current.Equals(darkGrass)) {
            tilemapSoil.SetTile(position3, darkHoed);
            return true;
        }
        return false;
    }

    public bool TryPlaceLog(Vector2Int position) {
        Vector3Int position3 = new Vector3Int(position.x, position.y, 0);
        TileBase current = tilemapSoil.GetTile(position3);
        if (current != null) return false;
        int center = generator.GetTile(position.x, position.y);
        int north = generator.GetTile(position.x, position.y + 1);
        int east = generator.GetTile(position.x + 1, position.y);
        int south = generator.GetTile(position.x, position.y - 1);
        int west = generator.GetTile(position.x - 1, position.y);
        if (center == 1 || center == 2) {
            if (north == center + 1) {
                tilemapSoil.SetTile(position3, ramps[0]);
                generator.SetRamp(position.x, position.y);
                return true;
            }
            else if ( east == center + 1) {
                tilemapSoil.SetTile(position3, ramps[1]);
                generator.SetRamp(position.x, position.y);
                return true;
            }
            else if (south == center + 1) {
                tilemapSoil.SetTile(position3, ramps[2]);
                generator.SetRamp(position.x, position.y);
                return true;
            }
            else if (west == center + 1) {
                tilemapSoil.SetTile(position3, ramps[3]);
                generator.SetRamp(position.x, position.y);
                return true;
            }
        }
        return false;
    }

    public bool TryAxe(Vector2Int position) {
        Vector3Int position3 = new Vector3Int(position.x, position.y, 0);
        TileBase current = tilemapPlants.GetTile(position3);
        if (current == null) return false;
        if (current.Equals(harvestables[0]) ||
            current.Equals(harvestables[1]) ||
            current.Equals(harvestables[2])) {
            tilemapPlants.SetTile(position3, null);
            return true;
        }
        return false;
    }

    public Item TryScythe(Vector2Int position) {
        Vector3Int position3 = new Vector3Int(position.x, position.y, 0);
        TileBase current = tilemapPlants.GetTile(position3);
        if (current == null) return null;
        if (current.Equals(harvestables[3]) ||
            current.Equals(harvestables[4])) {
            tilemapPlants.SetTile(position3, null);
            int rand = Random.Range(1, 10);
            if (rand <= 4) {
                return new WheatSeed();
            }else if (rand <= 7) {
                return new MelonSeed();
            } else {
                return new TomatoSeed();
            }
        } else if (current.Equals(tomato[tomato.Length - 1])) {
            tilemapPlants.SetTile(position3, null);
            tilemapSoil.SetTile(position3, null);
            return new Tomato();
        } else if (current.Equals(melon[melon.Length - 1])) {
            tilemapPlants.SetTile(position3, null);
            tilemapSoil.SetTile(position3, null);
            return new Melon();
        } else if (current.Equals(wheat[wheat.Length - 1])) {
            tilemapPlants.SetTile(position3, null);
            tilemapSoil.SetTile(position3, null);
            return new Wheat();
        }
        return null;
    }



    public bool TryWater(Vector2Int position) {
        Vector3Int position3 = new Vector3Int(position.x, position.y, 0);
        TileBase current = tilemapSoil.GetTile(position3);
        if (current != null && current.Equals(darkHoed)) {
            tilemapSoil.SetTile(position3, darkWet);
            return true;
        }
        return false;
    }

    public bool TryFill(Vector2Int position) {
        return (generator.GetTile(position.x - 1, position.y) == 0 ||
             generator.GetTile(position.x + 1, position.y) == 0 ||
             generator.GetTile(position.x, position.y + 1) == 0 ||
             generator.GetTile(position.x, position.y - 1) == 0);
    }

    public bool TryPlant(Vector2Int position, Seed seed) {
        Vector3Int position3 = new Vector3Int(position.x, position.y, 0);
        TileBase current = tilemapSoil.GetTile(position3);
        if (current == null) return false;
        if (tilemapPlants.GetTile(position3) == null && (current.Equals(darkHoed) || (current.Equals(darkWet)))) {
            if (seed is TomatoSeed) {
                tilemapPlants.SetTile(position3, tomato[0]);
            }
            else if (seed is MelonSeed) {
                tilemapPlants.SetTile(position3, melon[0]);
            } else {
                tilemapPlants.SetTile(position3, wheat[0]);
            }
            return true;
        }
        return false;
    }

    public bool CanMove(int startX, int startY, int endX, int endY) {
        if (startX == endX && startY == endY) return true;
        if (generator.GetTile(startX, startY).Equals(generator.GetTile(endX, endY))) return true;
        if (generator.IsRamp(endX, endY)) return true;
        if (generator.IsRamp(startX, startY)) return true;
        return false;
    }

    public bool CanExist(Vector2Int BL, Vector2Int TL, Vector2Int TR, Vector2Int BR) {
        List<Vector2Int> corners = new List<Vector2Int>();
        if (!generator.IsRamp(TL.x, TL.y))
            corners.Add(TL);
        if (!generator.IsRamp(BL.x, BL.y))
            corners.Add(BL);
        if (!generator.IsRamp(BR.x, BR.y))
            corners.Add(BR);
        if (!generator.IsRamp(TR.x, TR.y))
            corners.Add(TR);
        if (corners.Count <= 1) return true;
        int val = generator.GetTile(corners[0].x, corners[0].y);
        for (int i = 1; i < corners.Count; i++) {
            if (generator.GetTile(corners[i].x, corners[i].y) != val) return false;
        }
        return true;
    }

    public void Generate() {
        tilemapGround.ClearAllTiles();
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                int tile = generator.GetTile(i, j);
                Vector3Int pos = new Vector3Int(i, j);
                tilemapGround.SetTile(pos, tiles[tile]);
                if (tile == 2) {
                    int harvest = generator.GetHarvestable(i, j);
                    switch (harvest) {
                        case 1:
                            tilemapPlants.SetTile(pos, harvestables[4]);
                            break;
                        case 2:
                            tilemapPlants.SetTile(pos, harvestables[2]);
                            break;
                        case 3:
                            tilemapPlants.SetTile(pos, harvestables[0]);
                            break;
                    }
                }else if (tile == 3) {
                    int harvest = generator.GetHarvestable(i, j);
                    switch (harvest) {
                        case 1:
                            tilemapPlants.SetTile(pos, harvestables[3]);
                            break;
                        case 2:
                            tilemapPlants.SetTile(pos, harvestables[1]);
                            break;
                        case 3:
                            tilemapPlants.SetTile(pos, harvestables[0]);
                            break;
                    }
                }
            }
        }
        FindStartPos();
    }

    private void FindStartPos() {
        Vector2Int start = new Vector2Int(size / 2, size / 2), dir = Vector2Int.up;
        int perim = 1, perimCount = 1;
        while (true) {
            Debug.Log(start);
            int tile = generator.GetTile(start.x, start.y);
            //if (tile == 2 || tile == 3) { // grass
            //    break;
            //}
            if (tile == 1) { // sand
                break;
            }
            start += dir;
            perimCount--;
            if (perimCount == 0) {
                if (dir.Equals(Vector2Int.up)) {
                    dir = Vector2Int.left;
                    perimCount = perim;
                }
                else if (dir.Equals(Vector2Int.left)) {
                    dir = Vector2Int.down;
                    perim++;
                    perimCount = perim;
                }
                else if (dir.Equals(Vector2Int.down)) {
                    dir = Vector2Int.right;
                    perimCount = perim;
                }
                else {
                    dir = Vector2Int.up;
                    perim++;
                    perimCount = perim;
                }
            }
        }
        player.SetStartPosition(new Vector3(start.x + 0.5f, start.y + 0.5f, 0));
    }
}
