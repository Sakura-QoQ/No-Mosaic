param(
    [switch]$Package
)

$ErrorActionPreference = 'Stop'
$projectRoot = $PSScriptRoot
$project = Join-Path $projectRoot 'src\MosaicAssetPatch\MosaicAssetPatch.csproj'
$publishDirectory = Join-Path $projectRoot 'artifacts\MosaicAssetPatch'

dotnet publish $project -c Release --self-contained false -o $publishDirectory
if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed with exit code $LASTEXITCODE" }
foreach ($unneededName in @('AssetsTools.NET.xml', 'MosaicAssetPatch.pdb')) {
    $unneededPath = Join-Path $publishDirectory $unneededName
    if (Test-Path -LiteralPath $unneededPath) { Remove-Item -LiteralPath $unneededPath -Force }
}

if ($Package) {
    [xml]$projectXml = Get-Content -LiteralPath $project -Raw
    $version = [string]$projectXml.Project.PropertyGroup.Version
    if ($version -notmatch '^\d+\.\d+\.\d+$') { throw 'Invalid patcher version' }
    $releaseDirectory = Join-Path $projectRoot "versions\NoMosaic-Permanent-v$version"
    $manifest = Get-Content -LiteralPath (Join-Path $releaseDirectory 'release.json') -Raw | ConvertFrom-Json
    if ($manifest.version -ne $version) { throw 'Release manifest and patcher versions differ' }
    $releaseTools = Join-Path $releaseDirectory 'tools'
    New-Item -ItemType Directory -Path $releaseTools -Force | Out-Null
    foreach ($name in @('MosaicAssetPatch.exe', 'MosaicAssetPatch.dll',
        'MosaicAssetPatch.deps.json', 'MosaicAssetPatch.runtimeconfig.json',
        'AssetsTools.NET.dll', 'classdata.tpk')) {
        Copy-Item -LiteralPath (Join-Path $publishDirectory $name) -Destination $releaseTools -Force
    }
    $zip = Join-Path $projectRoot "versions\NoMosaic-Permanent-v$version.zip"
    if (Test-Path -LiteralPath $zip) { Remove-Item -LiteralPath $zip -Force }
    Compress-Archive -LiteralPath $releaseDirectory -DestinationPath $zip -CompressionLevel Optimal
    Write-Host "Packaged: $zip"
}

Write-Host "Built: $publishDirectory"
