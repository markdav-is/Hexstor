XCOPY "..\Client\bin\Debug\net9.0\Hexstor.Module.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Client\bin\Debug\net9.0\Hexstor.Module.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Server\bin\Debug\net9.0\Hexstor.Module.Server.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Server\bin\Debug\net9.0\Hexstor.Module.Server.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Shared\bin\Debug\net9.0\Hexstor.Module.Shared.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Shared\bin\Debug\net9.0\Hexstor.Module.Shared.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Server\wwwroot\*" "..\..\oqtane.framework\Oqtane.Server\wwwroot\" /Y /S /I
XCOPY "..\Client\bin\Debug\net9.0\MudBlazor.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y

