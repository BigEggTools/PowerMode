$docsFolderName = "..\docs";
$angularOutputFolderName = "dist";

If (Test-Path $docsFolderName) {
    Write-Output "Cleanup the GitHub pages folder.";
    Remove-Item $docsFolderName -Recurse;
}

Write-Output "Start build the Angular SAP.";
ng build --prod --base-href "https://bigeggtools.github.io/PowerMode/";

Write-Output "Copy the Angular SAP to GitHub pages folder.";
Copy-Item $angularOutputFolderName -Destination $docsFolderName -Recurse;

Write-Output "Done.";
