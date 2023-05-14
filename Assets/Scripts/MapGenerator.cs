using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class MapGenerator : MonoBehaviour
{
    // Grounds
    public Tilemap groundTilemap;
    public TileBase groundTile;

    // Walls
    public Tilemap wallTilemap;
    public TileBase wallTile;


    public int maxX;
    public int maxY;

    int cont = 1500;
    int[,] map;

    private void Start()
    {
        map = new int[maxX * 2, maxY * 2];
        GenerateLevel();
    }

    void GenerateLevel()
    {
        GenerateGround();
        PaintTerrain();
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
            cont -= 10;
            map[pos[0] + maxX, pos[1] + maxY] = 1;
            groundTilemap.SetTile(new Vector3Int(pos[0], pos[1], 2), groundTile);

            if (pos[0] + 1 < maxX && RandomDice())
            {
                queue.Enqueue(new int[] { pos[0] + 1, pos[1]});
            }
            if (pos[0] - 1 > -maxX && RandomDice())
            {
                queue.Enqueue(new int[] { pos[0] - 1, pos[1]});
            }
            if (pos[1] + 1 < maxY && RandomDice())
            {
                queue.Enqueue(new int[] { pos[0], pos[1] + 1 });
            }
            if (pos[1] - 1 > -maxY && RandomDice())
            {
                queue.Enqueue(new int[] { pos[0], pos[1] - 1 });
            }
        }
    }
    
    bool RandomDice()
    {
        int num = new System.Random().Next(0, Mathf.Max(cont, 2));

        int num2 = new System.Random().Next(0, Mathf.Max(cont, 2));

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
}
