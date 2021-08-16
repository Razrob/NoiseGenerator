using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextureSaver : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Vector2Int _textureSize = new Vector2Int(1024, 1024);
    [SerializeField] private string _fileName = "file_01";
    [SerializeField] private string _savePath = "Textures";
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private TextureType _textureType;
    [SerializeField] private ColorOverlayType _colorOverlayType;

    [Header("Texture color overlay")]
    [SerializeField] private Gradient _heightsColorsGradient;
    [SerializeField] private HeightsColors _heightsColors;

    [Header("Generation properties")]
    [SerializeField] private int _seed = 1;
    [SerializeField] private int _octaveNumber = 1;
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _frequencyMultiplier = 2;
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _amplitudeMultiplier = 0.5f;
    [SerializeField] private ClampingType _clampingType = ClampingType.Clamp;

    public enum TextureType { Raw, Color }
    public enum ColorOverlayType { WithGradient, WithHeightsColorArray }

    public void Generate()
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
        

        Texture2D _texture = new Texture2D(_textureSize.x, _textureSize.y);

        _texture.filterMode = _filterMode;

        if (_textureType == TextureType.Color)
        {
            if (_colorOverlayType == ColorOverlayType.WithGradient) _texture = _noiseGenerator.GetColorTexture(_textureSize.x, _textureSize.y, _seed, _heightsColorsGradient);
            else _texture = _noiseGenerator.GetColorTexture(_textureSize.x, _textureSize.y, _seed, _heightsColors);
        }
        else _texture = _noiseGenerator.GetRawTexture(_textureSize.x, _textureSize.y, _seed);

        byte[] _textureBytes = _texture.EncodeToPNG();
        File.WriteAllBytes($"{Application.dataPath}/{_savePath}/{_fileName}.png", _textureBytes);

    }
}
