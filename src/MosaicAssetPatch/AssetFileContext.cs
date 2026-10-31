using AssetsTools.NET.Extra;

namespace NoMosaic.MosaicAssetPatch;

internal sealed class AssetFileContext : IDisposable
{
    private AssetFileContext(AssetsManager manager, AssetsFileInstance instance)
    {
        Manager = manager;
        Instance = instance;
    }

    internal AssetsManager Manager { get; }
    internal AssetsFileInstance Instance { get; }

    internal static AssetFileContext Open(string inputPath)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Asset file was not found: {inputPath}", inputPath);

        var classPackagePath = ResolveClassPackagePath();
        var manager = new AssetsManager();
        try
        {
            manager.LoadClassPackage(classPackagePath);
            var instance = manager.LoadAssetsFile(inputPath, true);
            manager.LoadClassDatabaseFromPackage(instance.file.Metadata.UnityVersion);
            return new AssetFileContext(manager, instance);
        }
        catch
        {
            manager.UnloadAll();
            throw;
        }
    }

    public void Dispose() => Manager.UnloadAll();

    private static string ResolveClassPackagePath()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "classdata.tpk");
        if (!File.Exists(path))
            throw new FileNotFoundException("classdata.tpk was not found beside the patcher.", path);
        return path;
    }
}
