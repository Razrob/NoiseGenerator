using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;

    [Header("Main")]
    [SerializeField] private Vector3Int _worldSize;

    [Header("Generation properties")]
    [SerializeField] private int _seed = 1;
    [SerializeField] private int _octaveNumber = 1;
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _frequencyMultiplier = 2;
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _amplitudeMultiplier = 0.5f;
    [SerializeField] private ClampingType _clampingType = ClampingType.Clamp;

    private void Start()
    {

        NoiseGenerator _noiseGenerator = new NoiseGenerator
        {
            OctaveNumber = _octaveNumber,
            Frequency = _frequency,
            FrequencyMultiplier = _frequencyMultiplier,
            Amplitude = _amplitude,
            AmplitudeMultiplier = _amplitudeMultiplier,
            ClampingType = _clampingType
        };

        TerrainHeights _heights = _noiseGenerator.GetTerrainHeights(_worldSize.x, _worldSize.z, _seed);

        float[,] _terrainHeights = new float[_worldSize.x, _worldSize.z];

        for (int x = 0; x < _heights.Width; x++)
        {
            for (int z = 0; z < _heights.Height; z++)
            {
                _terrainHeights[x, z] = _heights[x, z] ;
            }
        }
        _terrain.terrainData.size = new Vector3(_worldSize.x, _worldSize.y, _worldSize.z);
        _terrain.terrainData.heightmapResolution = _worldSize.x;
        _terrain.terrainData.SetHeights(0, 0, _terrainHeights);
    }
}
