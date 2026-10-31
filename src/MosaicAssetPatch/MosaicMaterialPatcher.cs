using AssetsTools.NET;
using AssetsTools.NET.Extra;

namespace NoMosaic.MosaicAssetPatch;

internal sealed class MosaicMaterialPatcher
{
    private readonly AssetFileContext _assets;

    internal MosaicMaterialPatcher(AssetFileContext assets) => _assets = assets;

    internal MaterialPatchResult Verify()
    {
        var (mosaic, hidden) = FindMaterials();
        var mosaicShader = PointerSignature(mosaic.Field["m_Shader"]);
        var hiddenShader = PointerSignature(hidden.Field["m_Shader"]);
        return new MaterialPatchResult(mosaic.Info.PathId, mosaicShader, hiddenShader, mosaicShader == hiddenShader);
    }

    internal MaterialPatchResult Patch(string outputPath)
    {
        var (mosaic, hidden) = FindMaterials();
        var mosaicShader = PointerSignature(mosaic.Field["m_Shader"]);
        var hiddenShader = PointerSignature(hidden.Field["m_Shader"]);

        // Copy the complete transparent material while preserving the identity
        // referenced by CheckMosaic: the original name and PathID.
        hidden.Field["m_Name"].AsString = "Mosaic";
        var replacer = new AssetsReplacerFromMemory(_assets.Instance.file, mosaic.Info, hidden.Field);
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath)
            ?? throw new InvalidDataException("The output path has no parent directory."));
        using var output = new FileStream(outputPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        using var writer = new AssetsFileWriter(output);
        _assets.Instance.file.Write(writer, 0, new List<AssetsReplacer> { replacer }, _assets.Manager.ClassDatabase);

        return new MaterialPatchResult(mosaic.Info.PathId, mosaicShader, hiddenShader, true);
    }

    private (NamedMaterial Mosaic, NamedMaterial Hidden) FindMaterials()
    {
        NamedMaterial? mosaic = null;
        NamedMaterial? hidden = null;
        foreach (var info in _assets.Instance.file.GetAssetsOfType(AssetClassID.Material))
        {
            var field = _assets.Manager.GetBaseField(_assets.Instance, info, AssetReadFlags.None);
            switch (field["m_Name"].AsString)
            {
                case "Mosaic":
                    if (mosaic is not null) throw new InvalidDataException("Duplicate Mosaic material.");
                    mosaic = new NamedMaterial(info, field); break;
                case "Hided":
                    if (hidden is not null) throw new InvalidDataException("Duplicate Hided material.");
                    hidden = new NamedMaterial(info, field); break;
            }
        }

        if (mosaic is null || hidden is null)
            throw new InvalidDataException("The asset must contain exactly the expected Mosaic and Hided materials.");
        if (hidden.Field["m_Shader"]["m_PathID"].AsLong == 0)
            throw new InvalidDataException("The Hided material has a null Shader pointer.");
        return (mosaic, hidden);
    }

    private static string PointerSignature(AssetTypeValueField pointer) =>
        $"{pointer["m_FileID"].AsInt}:{pointer["m_PathID"].AsLong}";

    private sealed record NamedMaterial(AssetFileInfo Info, AssetTypeValueField Field);
}

internal sealed record MaterialPatchResult(long PathId, string MosaicShader, string HiddenShader, bool IsPatched);
