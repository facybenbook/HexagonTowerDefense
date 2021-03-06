﻿using UnityEngine;

public class UI_MapEditor_Utils : MonoBehaviour
{
    // The path within Assets/Resources/ that contains the TileMesh prefabs
    private static string RESOURCES_PATH_TILE_MESH = "Tile/TileMeshes";
    private static string RESOURCES_PATH_TILE_CONTENT = "Tile/TileContents";

    public static GameObject[] GetAllTileMeshesPrefabs() => Utils.GetResourcePrefabsSorted(RESOURCES_PATH_TILE_MESH);

    public static GameObject[] GetAllTileContentsPrefabs() => Utils.GetResourcePrefabsSorted(RESOURCES_PATH_TILE_CONTENT);
}
