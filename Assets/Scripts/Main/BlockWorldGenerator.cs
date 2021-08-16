using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockWorldGenerator : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Vector2Int _worldSize;
    [SerializeField] private HeightsBlocks _heightsBlocks;
    [SerializeField] private float _verticalScale;
    [SerializeField] private float _waterLevel;

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

        TerrainHeights _heights = _noiseGenerator.GetTerrainHeights(_worldSize.x, _worldSize.y, _seed);

        for (int x = 0; x < _heights.Width; x++)
        {
            for (int z = 0; z < _heights.Height; z++)
            {
                Vector3 _position = new Vector3(x, Convert.ToInt32(_heights[x, z] * _verticalScale), z);

                if (_position.y < _waterLevel * _verticalScale) _position.y = Convert.ToInt32(_waterLevel * _verticalScale);
                if (_heights[x, z] > .95f) Debug.Log(_heights[x, z]);

                Instantiate(GetBlockFromHeight(_heights[x, z]), _position, Quaternion.identity);
            }
        }


    }
    private GameObject GetBlockFromHeight(float _height)
    {
        GameObject _block = _heightsBlocks.Blocks[0].Block;
        for (int i = 0; i < _heightsBlocks.Blocks.Length; i++)
        {
            if (_height <= _heightsBlocks.Blocks[i].Height) break;
            else if (_heightsBlocks.Blocks.Length > i + 1) _block = _heightsBlocks.Blocks[i + 1].Block;

        }
        return _block;
    }

}
