param([Parameter(Mandatory)][string]$AssetPath)

$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot
$tool = Join-Path $projectRoot 'artifacts\MosaicAssetPatch\MosaicAssetPatch.dll'
$testRoot = Join-Path $projectRoot ('artifacts\test-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $testRoot | Out-Null
$before = (Get-FileHash -LiteralPath $AssetPath -Algorithm SHA256).Hash
$sample = Join-Path $testRoot 'sample.assets'
$output = Join-Path $testRoot 'patched.assets'
Copy-Item -LiteralPath $AssetPath -Destination $sample

function Assert-Command([int]$Expected, [string[]]$Arguments) {
    & dotnet $tool @Arguments
    if ($LASTEXITCODE -ne $Expected) {
        throw "Expected exit code $Expected, received $LASTEXITCODE for $Arguments"
    }
}

Assert-Command 2 @('patch')
Assert-Command 2 @('verify', $sample, 'extra')
Assert-Command 2 @('patch', $sample, $sample)
Assert-Command 3 @('verify', (Join-Path $testRoot 'missing.assets'))
Assert-Command 0 @('patch', $sample, $output)
Assert-Command 0 @('verify', $output)
Assert-Command 2 @('patch', $sample, $output)
if ((Get-FileHash -LiteralPath $AssetPath).Hash -ne $before -or
    (Get-FileHash -LiteralPath $sample).Hash -ne $before) {
    throw 'An input asset changed during the regression test.'
}
Write-Output "PASS: argument bounds, isolated patch/verify and unchanged input hashes ($before)"
