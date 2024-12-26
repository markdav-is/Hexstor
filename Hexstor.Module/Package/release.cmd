REN  Hexstor.Module.Template.nuspec.REMOVE Hexstor.Module.Template.nuspec 
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Hexstor.Module.Template.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y

