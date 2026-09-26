using AssetsTools.NET;
using AssetsTools.NET.Extra;

if (args.Length < 2 || args[0] is not ("scan" or "dump-go" or "patch" or "verify" or "patch-go" or "verify-go" or "patch-material-hidden" or "verify-material-hidden"))
{
    Console.Error.WriteLine("Usage: MosaicAssetPatch <scan|dump-go|patch|verify|patch-go|verify-go|patch-material-hidden|verify-material-hidden> <asset-file> [output-file|expected-count]");
    return 2;
}

string command = args[0];
string inputPath = Path.GetFullPath(args[1]);
string classPackagePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "classdata.tpk"));
if (!File.Exists(classPackagePath))
{
    classPackagePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "UABEA-v8", "classdata.tpk"));
}

var manager = new AssetsManager();
manager.LoadClassPackage(classPackagePath);
var instance = manager.LoadAssetsFile(inputPath, true);
manager.LoadClassDatabaseFromPackage(instance.file.Metadata.UnityVersion);

if (command is "patch-material-hidden" or "verify-material-hidden")
{
    var namedMaterials = new Dictionary<string, (AssetFileInfo Info, AssetTypeValueField Field)>(StringComparer.Ordinal);
    foreach (var info in instance.file.GetAssetsOfType(AssetClassID.Material))
    {
        var material = manager.GetBaseField(instance, info, AssetReadFlags.None);
        string name = material["m_Name"].AsString;
        if (name is "Mosaic" or "Hided")
            namedMaterials[name] = (info, material);
    }

    if (!namedMaterials.TryGetValue("Mosaic", out var mosaic) ||
        !namedMaterials.TryGetValue("Hided", out var hidden))
    {
        Console.Error.WriteLine($"Expected Mosaic and Hided materials; found: {string.Join(",", namedMaterials.Keys)}");
        return 6;
    }

    string mosaicShader = PPtrSignature(mosaic.Field["m_Shader"]);
    string hiddenShader = PPtrSignature(hidden.Field["m_Shader"]);

    if (command == "verify-material-hidden")
    {
        bool valid = mosaicShader == hiddenShader;
        Console.WriteLine(valid
            ? $"VERIFY_MATERIAL_HIDDEN_OK pathId={mosaic.Info.PathId} shader={mosaicShader}"
            : $"VERIFY_MATERIAL_HIDDEN_FAILED mosaicShader={mosaicShader} hiddenShader={hiddenShader}");
        return valid ? 0 : 4;
    }

    if (args.Length != 3)
    {
        Console.Error.WriteLine("patch-material-hidden requires an output file");
        return 2;
    }

    // Serialize the complete transparent Hided material at Mosaic's original
    // PathID. Preserve the Mosaic name so CheckMosaic keeps resolving the same
    // monitored object while its renderer receives transparent HDRP/Unlit
    // properties and render state.
    hidden.Field["m_Name"].AsString = "Mosaic";
    var materialReplacer = new AssetsReplacerFromMemory(instance.file, mosaic.Info, hidden.Field);
    string materialOutputPath = Path.GetFullPath(args[2]);
    Directory.CreateDirectory(Path.GetDirectoryName(materialOutputPath)!);
    using (var writer = new AssetsFileWriter(materialOutputPath))
    {
        instance.file.Write(writer, 0, new List<AssetsReplacer> { materialReplacer }, manager.ClassDatabase);
    }
    Console.WriteLine($"PATCH_MATERIAL_HIDDEN_OK output={materialOutputPath} pathId={mosaic.Info.PathId} shader={mosaicShader}->{hiddenShader}");
    return 0;
}

if (command is "patch-go" or "verify-go")
{
    var gameObjects = new List<(AssetFileInfo Info, AssetTypeValueField Field)>();
    foreach (var info in instance.file.GetAssetsOfType(AssetClassID.GameObject))
    {
        var gameObject = manager.GetBaseField(instance, info, AssetReadFlags.None);
        if (gameObject["m_Name"].AsString == "Mosaic")
            gameObjects.Add((info, gameObject));
    }

    if (command == "verify-go")
    {
        if (args.Length != 3 || !int.TryParse(args[2], out int expectedCount))
        {
            Console.Error.WriteLine("verify-go requires the expected Mosaic GameObject count");
            return 2;
        }
        int inactiveCount = gameObjects.Count(x => !x.Field["m_IsActive"].AsBool);
        bool valid = gameObjects.Count == expectedCount && inactiveCount == expectedCount;
        Console.WriteLine(valid
            ? $"VERIFY_GO_OK file={Path.GetFileName(inputPath)} count={expectedCount} inactive={inactiveCount}"
            : $"VERIFY_GO_FAILED file={Path.GetFileName(inputPath)} found={gameObjects.Count} inactive={inactiveCount} expected={expectedCount}");
        return valid ? 0 : 4;
    }

    if (args.Length != 3 || gameObjects.Count == 0)
    {
        Console.Error.WriteLine($"patch-go requires an output file and at least one Mosaic GameObject; found {gameObjects.Count}");
        return 5;
    }

    var goReplacers = new List<AssetsReplacer>();
    foreach (var item in gameObjects)
    {
        item.Field["m_IsActive"].AsBool = false;
        goReplacers.Add(new AssetsReplacerFromMemory(instance.file, item.Info, item.Field));
        Console.WriteLine($"PATCH_GO pathId={item.Info.PathId} m_IsActive=false");
    }
    string goOutputPath = Path.GetFullPath(args[2]);
    Directory.CreateDirectory(Path.GetDirectoryName(goOutputPath)!);
    using (var writer = new AssetsFileWriter(goOutputPath))
    {
        instance.file.Write(writer, 0, goReplacers, manager.ClassDatabase);
    }
    Console.WriteLine($"PATCH_GO_OK output={goOutputPath} count={gameObjects.Count}");
    return 0;
}

if (command == "dump-go")
{
    int count = 0;
    foreach (var info in instance.file.GetAssetsOfType(AssetClassID.GameObject))
    {
        var gameObject = manager.GetBaseField(instance, info, AssetReadFlags.None);
        if (gameObject["m_Name"].AsString != "Mosaic")
            continue;
        count++;
        Console.WriteLine($"GAMEOBJECT file={Path.GetFileName(inputPath)} pathId={info.PathId}");
        DumpTree(gameObject, 0);
    }
    return count > 0 ? 0 : 3;
}

var matches = new List<(AssetFileInfo Info, AssetTypeValueField Field, AssetTypeValueField Size)>();
foreach (var info in instance.file.GetAssetsOfType(AssetClassID.Material))
{
    var field = manager.GetBaseField(instance, info, AssetReadFlags.None);
    if (field["m_Name"].AsString != "Mosaic")
        continue;

    var sizeField = FindFloatProperty(field, "_Size");
    if (sizeField is not null)
        matches.Add((info, field, sizeField));

    Console.WriteLine($"MATCH file={Path.GetFileName(inputPath)} pathId={info.PathId} name={field["m_Name"].AsString} size={(sizeField is null ? "<missing>" : sizeField.AsFloat.ToString("R"))}");
}

if (command == "scan")
    return matches.Count > 0 ? 0 : 3;

if (command == "verify")
{
    bool valid = matches.Count == 1 && Math.Abs(matches[0].Size.AsFloat) < 0.00001f;
    Console.WriteLine(valid ? "VERIFY_OK" : $"VERIFY_FAILED matches={matches.Count}");
    return valid ? 0 : 4;
}

if (args.Length != 3)
{
    Console.Error.WriteLine("patch requires an output file");
    return 2;
}
if (matches.Count != 1)
{
    Console.Error.WriteLine($"Refusing to patch: expected exactly one Mosaic material with _Size, found {matches.Count}.");
    return 5;
}

matches[0].Size.AsFloat = 0f;
var replacers = new List<AssetsReplacer>
{
    new AssetsReplacerFromMemory(instance.file, matches[0].Info, matches[0].Field)
};
string outputPath = Path.GetFullPath(args[2]);
Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
using (var writer = new AssetsFileWriter(outputPath))
{
    instance.file.Write(writer, 0, replacers, manager.ClassDatabase);
}
Console.WriteLine($"PATCH_OK output={outputPath} pathId={matches[0].Info.PathId} _Size=0");
return 0;

static AssetTypeValueField? FindFloatProperty(AssetTypeValueField root, string propertyName)
{
    var floats = FindField(root, "m_Floats");
    if (floats is null)
        return null;

    var array = floats["Array"];
    foreach (var entry in array.Children)
    {
        if (entry.Children.Count >= 2 && entry.Children[0].AsString == propertyName)
            return entry.Children[1];
    }
    return null;
}

static string PPtrSignature(AssetTypeValueField pptr)
{
    return $"{pptr["m_FileID"].AsInt}:{pptr["m_PathID"].AsLong}";
}

static AssetTypeValueField? FindField(AssetTypeValueField node, string fieldName)
{
    if (node.FieldName == fieldName)
        return node;
    foreach (var child in node.Children)
    {
        var result = FindField(child, fieldName);
        if (result is not null)
            return result;
    }
    return null;
}

static void DumpTree(AssetTypeValueField node, int depth)
{
    string value = node.Children.Count == 0 ? node.Value?.ToString() ?? "<null>" : "";
    Console.WriteLine($"{new string(' ', depth * 2)}{node.FieldName} [{node.TypeName}] {value}");
    foreach (var child in node.Children)
        DumpTree(child, depth + 1);
}
