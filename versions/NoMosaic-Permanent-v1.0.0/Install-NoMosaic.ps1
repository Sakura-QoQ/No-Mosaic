param(
    [string]$GameDirectory = ""
)

$ErrorActionPreference = "Stop"

function Find-GameDirectory {
    param([string]$RequestedPath)

    if ($RequestedPath) {
        return (Resolve-Path -LiteralPath $RequestedPath).Path
    }

    $candidates = @(
        $PSScriptRoot,
        (Split-Path -Parent $PSScriptRoot),
        (Get-Location).Path
    ) | Select-Object -Unique

    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath (Join-Path $candidate "FallenFlower.exe")) {
            return (Resolve-Path -LiteralPath $candidate).Path
        }
    }

    $entered = Read-Host "Enter the full FallenFlower installation directory"
    return (Resolve-Path -LiteralPath $entered).Path
}

$gameDirectory = Find-GameDirectory $GameDirectory
$gameExe = Join-Path $gameDirectory "FallenFlower.exe"
$assetFile = Join-Path $gameDirectory "FallenFlower_Data\sharedassets0.assets"
$patcher = Join-Path $PSScriptRoot "tools\MosaicAssetPatch.exe"

if (-not (Test-Path -LiteralPath $gameExe -PathType Leaf)) {
    throw "FallenFlower.exe was not found in: $gameDirectory"
}
if (-not (Test-Path -LiteralPath $assetFile -PathType Leaf)) {
    throw "sharedassets0.assets was not found in: $gameDirectory\FallenFlower_Data"
}
if (-not (Test-Path -LiteralPath $patcher -PathType Leaf)) {
    throw "The bundled patcher is missing: $patcher"
}
if (Get-Process -Name "FallenFlower" -ErrorAction SilentlyContinue) {
    throw "FallenFlower is currently running. Close the game and run the installer again."
}

Write-Host "Checking current material state..."
& $patcher verify-material-hidden $assetFile
if ($LASTEXITCODE -eq 0) {
    Write-Host "NoMosaic Permanent is already installed."
    exit 0
}

$temporaryFile = Join-Path ([IO.Path]::GetTempPath()) ("NoMosaic-" + [Guid]::NewGuid().ToString("N") + ".assets")

try {
    Write-Host "Creating the patched resource..."
    & $patcher patch-material-hidden $assetFile $temporaryFile
    if ($LASTEXITCODE -ne 0) {
        throw "The patcher rejected this game resource. The game version may be incompatible."
    }

    & $patcher verify-material-hidden $temporaryFile
    if ($LASTEXITCODE -ne 0) {
        throw "Patched-resource verification failed. The game file was not changed."
    }

    Copy-Item -LiteralPath $temporaryFile -Destination $assetFile -Force

    & $patcher verify-material-hidden $assetFile
    if ($LASTEXITCODE -ne 0) {
        throw "Installed-resource verification failed. Repair the game files before launching."
    }

    Write-Host "Installation completed successfully."
    Write-Host "Launch FallenFlower.exe normally. --enable-mods is not required."
}
finally {
    if (Test-Path -LiteralPath $temporaryFile) {
        Remove-Item -LiteralPath $temporaryFile -Force
    }
}
