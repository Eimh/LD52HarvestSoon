using System;
using UnityEngine;
using Random = System.Random;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField]
    private float frequency = 1f;
    [SerializeField]
    private float itemFrequency = 1f;

    private int size, height;
    private int[,] map;
    private bool[,] ramps;

    private bool pregenerated;

    private FastNoiseLite noise;
    private void Awake() {
        noise = new FastNoiseLite(new Random().Next());
        pregenerated = false;
    }

    public void SetSize(int size) {
        map = new int[size, size];
        ramps = new bool[size, size];
        this.size = size-1;
    }

    public void SetHeight(int height) {
        this.height = height;
    }

    public void Pregenerate() {
        for (int i = 0; i <= size; i++) {
            for (int j = 0; j <= size; j++) {
                map[i,j] = GetTile(i, j);
            }
        }
        PostProcess();
        pregenerated = true;
    }

    private int CountNeighbors(int height, int x, int y) {
        int c = 0;
        if (map[x - 1, y] == height)
            c++;
        if (map[x + 1, y] == height)
            c++;
        if (map[x, y + 1] == height)
            c++;
        if (map[x, y - 1] == height)
            c++;
        return c;
    }

    public void SetRamp(int x, int y) {
        ramps[x, y] = true;
    }

    public bool IsRamp(int x, int y) {
        if (ramps[x, y]) return true;
        int c = 0, s = 0;
        if (map[x - 1, y] == map[x, y] - 1)
            c++;
        if (map[x + 1, y] == map[x, y] - 1)
            c++;
        if (map[x, y + 1] == map[x, y] - 1)
            c++;
        if (map[x, y - 1] == map[x, y] - 1)
            c++;
        if (map[x - 1, y] == map[x, y])
            s++;
        if (map[x + 1, y] == map[x, y])
            s++;
        if (map[x, y + 1] == map[x, y])
            s++;
        if (map[x, y - 1] == map[x, y])
            s++;
        ramps[x,y] = s == 1 && c == 3;
        return ramps[x, y];
    }



    private void PostProcess() {
        for (int i = 1; i < size; i++) {
            for (int j = 1; j < size; j++) {
                if (map[i, j] == height - 1) {
                    if (CountNeighbors(height-1, i, j) <= 1)
                        map[i, j]--;
                } else if (map[i, j] == 0) {
                    if (CountNeighbors(0, i, j) <= 1)
                        map[i, j]++;
                }
            }
        }
    }

    public int GetTile(int x, int y) {
        if (pregenerated) return map[x, y];
        if (x == 0 || y == 0 || x == size || y == size) return height - 1;
        if (x == 1 || y == 1 || x == size - 1 || y == size - 1) return height - 1;
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise.SetFrequency(frequency);
        int val = (int)((noise.GetNoise(x + 0.5f, y + 0.5f) + 1) / 2 * height);
        if (x == 2 || y == 2 || x == size - 2 || y == size - 2) return Math.Max(height - 2, val);
        if (x == 3 || y == 3 || x == size - 3 || y == size - 3) return Math.Max(height - 2, val);
        if (x == 4 || y == 4 || x == size - 4 || y == size - 4) return Math.Max(height - 3, val);
        if (x == 5 || y == 5 || x == size - 5 || y == size - 5) return Math.Max(height - 3, val);
        if (x == 6 || y == 6 || x == size - 6 || y == size - 6) return Math.Max(height - 4, val);
        if (x == 7 || y == 7 || x == size - 7 || y == size - 7) return Math.Max(height - 4, val);
        return val;
    }

    public int GetHarvestable(int x, int y) {
        noise.SetNoiseType(FastNoiseLite.NoiseType.Value);
        noise.SetFrequency(itemFrequency);
        float val = noise.GetNoise(x + 0.5f, y + 0.5f);
        if (Mathf.Abs(val) < 0.65f) return 0;
        else if (Mathf.Abs(val) < 0.75f) return 1;
        else if (Mathf.Abs(val) < 0.9f) return 2;
        else return 3;
    }
}
