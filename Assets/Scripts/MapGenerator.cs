using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class MapGenerator : MonoBehaviour
{
    // Enemigos
    List<Vector3Int> enemies;


    // Grounds
    public Tilemap groundTilemap;
    public TileBase groundTile;
    public TileBase groundTile2;
    public TileBase groundTile3;
    public TileBase groundTile4;

    public Tilemap addonsTilemap;
    public TileBase addonsTile1;
    public TileBase addonsTile2;

    public GameObject chest;

    // Walls
    public Tilemap wallTilemap;
    public TileBase wallTile;

    public int maxX;
    public int maxY;

    int cont = 1500;
    int[,] map;

    

    private void Start()
    {
        enemies = new();
        map = new int[maxX * 2, maxY * 2];
        GenerateLevel();
        GetComponent<GameManager>().GenerarEnemigos(enemies);
    }

    void GenerateLevel()
    {
        GenerateGround();
        PaintTerrain();
        EnableShadows();
    }
    void EnableShadows()
    {
        wallTilemap.GetComponent<ShadowCaster2DCreator>().Create();
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

            TileBase tile;
            int num = new System.Random().Next(0, 300);
            switch (num)
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
                    Instantiate(chest, new Vector3Int(pos[0], pos[1], 1), Quaternion.identity);
                    break;
                case 9:
                    tile = groundTile;
                    addonsTilemap.SetTile(new Vector3Int(pos[0], pos[1], 1), addonsTile1);
                    break;
                case 10:
                    tile = groundTile;
                    addonsTilemap.SetTile(new Vector3Int(pos[0], pos[1], 1), addonsTile2);
                    break;
                case 11:
                    tile = groundTile;
                    enemies.Add(new Vector3Int(pos[0], pos[1], 1));
                    break;
                default:
                    tile = groundTile;
                    break;
            }
            groundTilemap.SetTile(new Vector3Int(pos[0], pos[1], 2), tile);

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
