using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Tile Atlas")]
    public TileAtlas tileAtlas;

    [Header("Trees")]
    public int treeChance = 10;
    public int minTreeHeight = 3;
    public int maxTreeHeight = 6;

    [Header("Generation Settings")]
    public int chunkSize = 16;
    public int worldSize = 100;
    public int DirtLayerHeight = 5;
    public bool generateCaves = true;
    public float surfaceValue = 0.25f;
    public float heightMultiplier = 15f;
    public int heightAddition = 25;

    [Header("Noise Settings")]
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.5f;
    public float seed;
    public Texture2D caveNoiseTexture;

    [Header("Ore Settings")]
    public OreClass[] ores;
 /* public float coalRarity;
    public float coalSize;
    public float ironRarity;
    public float ironSize;
    public float goldRarity;
    public float goldSize;
    public float diamondRarity;
    public float diamondSize;
    public Texture2D coalSpread;
    public Texture2D ironSpread;
    public Texture2D goldSpread;
    public Texture2D diamondSpread; */



    private GameObject[] worldChunks;
    private List<Vector2> worldTiles = new List<Vector2>();

    private void OnValidate()
    {
            caveNoiseTexture = new Texture2D(worldSize, worldSize);
            ores[0].spreadTexture = new Texture2D(worldSize, worldSize);
            ores[1].spreadTexture = new Texture2D(worldSize, worldSize);
            ores[2].spreadTexture = new Texture2D(worldSize, worldSize);
            ores[3].spreadTexture = new Texture2D(worldSize, worldSize);
        
        GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

        GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
        GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
        GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
        GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);
    }


    private void Start()
    {
        seed = Random.Range(-10000, 10000);
        if (caveNoiseTexture == null)
        {
            caveNoiseTexture = new Texture2D(worldSize, worldSize);
            ores[0].spreadTexture = new Texture2D(worldSize, worldSize);
            ores[1].spreadTexture = new Texture2D(worldSize, worldSize);
            ores[2].spreadTexture = new Texture2D(worldSize, worldSize);
            ores[3].spreadTexture = new Texture2D(worldSize, worldSize);

        }
        GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

        GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
        GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
        GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
        GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);

        CreateChunks();
        GenerateTerrain();
    }

    public void CreateChunks()
    {
        int numChunks = worldSize / chunkSize;
        worldChunks = new GameObject[numChunks];
        for (int i =0; i < numChunks; i++)
        {
            GameObject newChunk = new GameObject();
            newChunk.name = i.ToString();
            newChunk.transform.parent = this.transform;
            worldChunks[i] = newChunk;
        }
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

            for (int y = 0; y < height; y++)
            {
                Sprite tileSprite;
                if (y < height - DirtLayerHeight)
                {
                    tileSprite = tileAtlas.Stone.tileSprite;

                    if (ores[0].spreadTexture.GetPixel(x, y).r > 0.5f)
                        tileSprite = tileAtlas.coal.tileSprite;
                    if (ores[1].spreadTexture.GetPixel(x, y).r > 0.5f)
                        tileSprite = tileAtlas.iron.tileSprite;
                    if (ores[2].spreadTexture.GetPixel(x, y).r > 0.5f)
                        tileSprite = tileAtlas.gold.tileSprite;
                    if (ores[3].spreadTexture.GetPixel(x, y).r > 0.5f)
                        tileSprite = tileAtlas.diamond.tileSprite;
                }
                else if (y < height - 1)
                {
                    tileSprite = tileAtlas.Dirt.tileSprite;
                }
                else
                {
                    // top layer of the terrain
                    tileSprite = tileAtlas.Grass.tileSprite;
                }
                if (generateCaves)
                {
                    if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                    {
                        PlaceTile(tileSprite, x, y,false);
                    }
                }
                else
                {
                    PlaceTile(tileSprite, x, y, false);
                }
                if (y >= height - 1)
                {
                    int t = Random.Range(0, treeChance);
                    if (t == 1)
                    {
                        //generate a tree
                        if (worldTiles.Contains(new Vector2(x, y)))
                        {
                            GenerateTree(x, y + 1);
                        }    
                    }

                }
            }
        }
    }

    public void GenerateNoiseTexture(float frequency,float limit, Texture2D noiseTexture)
    {
        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency);
                if (v > limit)
                noiseTexture.SetPixel(x, y, Color.white);
                else
                    noiseTexture.SetPixel(x, y, Color.black);
            }
        }
        noiseTexture.Apply();
    }

    void GenerateTree(int x, int y)
    {
        //generate Log
        int treeHeight = Random.Range(minTreeHeight, maxTreeHeight);
        for (int i =0; i < treeHeight; i++)
        {
            PlaceTile(tileAtlas.Log.tileSprite, x, y + i, true);
        }

        //generate leaves
        PlaceTile(tileAtlas.Leaf.tileSprite, x, y + treeHeight, true);
        PlaceTile(tileAtlas.Leaf.tileSprite, x, y + treeHeight + 1, true);
        PlaceTile(tileAtlas.Leaf.tileSprite, x, y + treeHeight + 2, true);

        PlaceTile(tileAtlas.Leaf.tileSprite, x - 1, y + treeHeight, true);
        PlaceTile(tileAtlas.Leaf.tileSprite, x - 1, y + treeHeight + 1, true);

        PlaceTile(tileAtlas.Leaf.tileSprite, x + 1, y + treeHeight, true);
        PlaceTile(tileAtlas.Leaf.tileSprite, x + 1, y + treeHeight + 1, true);

    }
    public void PlaceTile(Sprite tileSprite, int x, int y, bool backgroundElement)
    {
        GameObject newTile = new GameObject();

        int chunkCoord = (Mathf.RoundToInt(x / chunkSize) * chunkSize);
        chunkCoord /= chunkSize;
        newTile.transform.parent = worldChunks[(int)chunkCoord].transform;


        newTile.AddComponent<SpriteRenderer>();
        if (!backgroundElement)
        {
            newTile.AddComponent<BoxCollider2D>();
            newTile.tag = "Ground";
            newTile.GetComponent<BoxCollider2D>().size = Vector2.one;
        }
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
        newTile.name = tileSprite.name;
        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

        worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
    }
}
