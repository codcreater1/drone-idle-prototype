using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Transform scout;
    public Material fogMaterial;
    public int gridSize = 10;
    public float revealRadius = 2f;

    private GameObject[,] tiles;

    void Start()
    {
        tiles = new GameObject[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = "FogTile_" + x + "_" + z;
                tile.transform.parent = transform;

                float worldX = x - gridSize / 2f + 0.5f;
                float worldZ = z - gridSize / 2f + 0.5f;
                tile.transform.position = new Vector3(worldX, 0.05f, worldZ);
                tile.transform.localScale = new Vector3(1f, 0.1f, 1f);

                if (fogMaterial != null)
                {
                    tile.GetComponent<MeshRenderer>().material = fogMaterial;
                }

                Destroy(tile.GetComponent<Collider>());

                tiles[x, z] = tile;
            }
        }
    }

    void Update()
    {
        if (scout == null) return;

        Vector3 scoutFlat = new Vector3(scout.position.x, 0f, scout.position.z);

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                if (tiles[x, z] == null) continue;

                Vector3 tileFlat = new Vector3(
                    tiles[x, z].transform.position.x,
                    0f,
                    tiles[x, z].transform.position.z
                );

                if (Vector3.Distance(tileFlat, scoutFlat) < revealRadius)
                {
                    Destroy(tiles[x, z]);
                    tiles[x, z] = null;
                }
            }
        }
    }
}
