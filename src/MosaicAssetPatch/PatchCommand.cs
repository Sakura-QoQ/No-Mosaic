namespace NoMosaic.MosaicAssetPatch;

internal sealed record PatchCommand(bool VerifyOnly, string InputPath, string? OutputPath)
{
    internal static PatchCommand Parse(string[] args)
    {
        if (args.Length == 0 || args[0] is not ("patch" or "patch-material-hidden" or "verify" or "verify-material-hidden"))
            throw new ArgumentException("Usage: MosaicAssetPatch <patch-material-hidden|verify-material-hidden> <asset-file> [output-file]");
        var verify = args[0] is "verify" or "verify-material-hidden";
        if (args.Length != (verify ? 2 : 3))
            throw new ArgumentException(verify ? "verify requires exactly one input file" : "patch requires an input and an output file");
        var input = Path.GetFullPath(args[1]);
        var output = verify ? null : Path.GetFullPath(args[2]);
        if (output is not null && string.Equals(input, output, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("The output must be a separate, new file; in-place patching is not supported.");
        if (output is not null && File.Exists(output))
            throw new ArgumentException("The output already exists. Choose a new output file.");
        return new PatchCommand(verify, input, output);
    }
}
