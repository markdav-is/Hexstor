del "*.nupkg"
REN Hexstor.Theme.Space.nuspec.REMOVE Hexstor.Theme.Space.nuspec
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Hexstor.Theme.Space.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\wwwroot\Themes\" /Y
