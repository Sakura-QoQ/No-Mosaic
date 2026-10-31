using NoMosaic.MosaicAssetPatch;

try
{
    var command = PatchCommand.Parse(args);
    using var assets = AssetFileContext.Open(command.InputPath);
    var patcher = new MosaicMaterialPatcher(assets);

    if (command.VerifyOnly)
    {
        var verifyResult = patcher.Verify();
        Console.WriteLine(verifyResult.IsPatched
            ? $"VERIFY_MATERIAL_HIDDEN_OK pathId={verifyResult.PathId} shader={verifyResult.MosaicShader}"
            : $"VERIFY_MATERIAL_HIDDEN_FAILED mosaicShader={verifyResult.MosaicShader} hiddenShader={verifyResult.HiddenShader}");
        return verifyResult.IsPatched ? ExitCodes.Success : ExitCodes.VerificationFailed;
    }

    var outputPath = command.OutputPath!;
    var patchResult = patcher.Patch(outputPath);
    Console.WriteLine($"PATCH_MATERIAL_HIDDEN_OK output={outputPath} pathId={patchResult.PathId} shader={patchResult.MosaicShader}->{patchResult.HiddenShader}");
    return ExitCodes.Success;
}
catch (ArgumentException exception)
{
    Console.Error.WriteLine(exception.Message);
    return ExitCodes.InvalidArguments;
}
catch (FileNotFoundException exception)
{
    Console.Error.WriteLine(exception.Message);
    return ExitCodes.MissingFile;
}
catch (InvalidDataException exception)
{
    Console.Error.WriteLine(exception.Message);
    return ExitCodes.IncompatibleAsset;
}
catch (Exception exception)
{
    Console.Error.WriteLine(exception.Message);
    return ExitCodes.UnexpectedFailure;
}
