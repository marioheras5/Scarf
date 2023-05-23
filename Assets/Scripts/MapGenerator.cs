using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Grounds
    public Tilemap groundTilemap;
    public TileBase groundTile;
    public TileBase groundTile2;
    public TileBase groundTile3;
    public TileBase groundTile4;

    public Tilemap addonsTilemap;
    public TileBase addonsTile1;
    public TileBase addonsTile2;

    // Walls
    public Tilemap wallTilemap;
    public TileBase wallTile;

    public int maxX;
    public int maxY;


    int probabilityCont = 1500;
    int[,] map;

    // Enemigos
    List<Vector3Int> enemies;
    public int numEnemigos = 5;

    // Cofres
    List<Vector3Int> cofres;
    public int numCofres = 5;

    private void Start()
    {
        enemies = new();
        cofres = new();
        map = new int[maxX * 2, maxY * 2];
        GenerateLevel();
    }

    void GenerateLevel()
    {
        GenerateGround();
        PaintTerrain();
        SpawnearEnemigos();
        SpawnearCofres();
    }
    void GenerateGround()
    {
        // Usamos una poquita de BFS... my god...
        Queue<int[]> queue = new Queue<int[]>();
        queue.Enqueue(new int[] {0, 0});
        while (queue.Count > 0)
        {
            int[] pos = queue.Dequeue();

            if (map[pos[0] + maxX, pos[1] + maxY] == 1)
            {
                continue;
            }
            probabilityCont -= 10;
            map[pos[0] + maxX, pos[1] + maxY] = 1;

            TileBase tile;
            int randomNum = new System.Random().Next(0, 300);
            switch (randomNum)
            {
                case 0:
                case 1:
                    tile = groundTile2;
                    break;
                case 3:
                case 4:
                    tile = groundTile3;
                    break;
                case 6:
                case 7:
                    tile = groundTile4;
                    break;
                case 8:
                    tile = groundTile;
                    addonsTilemap.SetTile(new Vector3Int(pos[0], pos[1], 1), addonsTile1);
                    break;
                case 9:
                    tile = groundTile;
                    addonsTilemap.SetTile(new Vector3Int(pos[0], pos[1], 1), addonsTile2);
                    break;
                default:
                    tile = groundTile;
                    break;
            }
            groundTilemap.SetTile(new Vector3Int(pos[0], pos[1], 2), tile);

            if (pos[0] + 1 < maxX && RandomProbability())
            {
                queue.Enqueue(new int[] { pos[0] + 1, pos[1]});
            }
            if (pos[0] - 1 > -maxX && RandomProbability())
            {
                queue.Enqueue(new int[] { pos[0] - 1, pos[1]});
            }
            if (pos[1] + 1 < maxY && RandomProbability())
            {
                queue.Enqueue(new int[] { pos[0], pos[1] + 1 });
            }
            if (pos[1] - 1 > -maxY && RandomProbability())
            {
                queue.Enqueue(new int[] { pos[0], pos[1] - 1 });
            }
        }
    }
    bool RandomProbability()
    {
        int num = new System.Random().Next(0, Mathf.Max(probabilityCont, 2));

        int num2 = new System.Random().Next(0, Mathf.Max(probabilityCont, 2));

        if (num == num2) return false;
        else return true;
    }
    void PaintTerrain()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                {
                    // Ground
                    groundTilemap.SetTile(new Vector3Int(i - maxX, j - maxY, 2), groundTile);
                    // Wall
                    wallTilemap.SetTile(new Vector3Int(i - maxX, j - maxY, 1), wallTile);
                }
            }
        }
        // Offsets
        for (int i = - maxX - 20; i < maxX + 20; i++)
        {
            for (int j = - maxY - 20; j < maxY + 20; j++)
            {
                if (i >= -maxX && j >= -maxY && i < map.GetLength(0) - maxX && j < map.GetLength(1) - maxY)
                {
                    continue;
                }
                wallTilemap.SetTile(new Vector3Int(i, j, 1), wallTile);
            }
        }
    }
    public void SpawnearEnemigos()
    {
        enemies.Clear();
        int r1 = new System.Random().Next(0, map.GetLength(0));
        int r2 = new System.Random().Next(0, map.GetLength(1));
        for (int i = 0; i < numEnemigos; i++)
        {
            if (CheckMapPosition(r1, r2))
            {
                enemies.Add(new Vector3Int(r1 - maxX, r2 - maxY, 1));
            }
            else
            {
                i--;
            }
            r1 = new System.Random().Next(0, map.GetLength(0));
            r2 = new System.Random().Next(0, map.GetLength(1));
        }
        GetComponent<GameManager>().GenerarEnemigos(enemies);
    }
    public void SpawnearCofres()
    {
        cofres.Clear();
        int r1 = new System.Random().Next(0, map.GetLength(0));
        int r2 = new System.Random().Next(0, map.GetLength(1));
        for (int i = 0; i < numCofres; i++)
        {
            if (CheckMapPosition(r1, r2))
            {
                cofres.Add(new Vector3Int(r1 - maxX, r2 - maxY, 1));
            }
            else
            {
                i--;
            }
            r1 = new System.Random().Next(0, map.GetLength(0));
            r2 = new System.Random().Next(0, map.GetLength(1));
        }
        GetComponent<GameManager>().GenerarCofres(cofres);
    }
    bool CheckMapPosition(int i, int j)
    {
        int numRows = map.GetLength(0);
        int numCols = map.GetLength(1);

        for (int row = i - 1; row <= i + 1; row++)
        {
            for (int col = j - 1; col <= j + 1; col++)
            {
                if (row >= 0 && row < numRows && col >= 0 && col < numCols)
                {
                    if (map[row, col] != 1)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}
