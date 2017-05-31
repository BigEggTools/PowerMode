$docsFolderName = "..\docs";
$angularOutputFolderName = "dist";

If (Test-Path $docsFolderName) {
    Remove-Item $docsFolderName -Recurse;
}

ng build --prod --base-href "https://bigeggtools.github.io/PowerMode/";

Copy-Item $angularOutputFolderName -Destination $docsFolderName -Recurse;
