#!/bin/bash

TargetFramework=$1
ProjectName=$2

cp -f "../Client/bin/Debug/$TargetFramework/$ProjectName$.Client.Oqtane.dll" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "../Client/bin/Debug/$TargetFramework/$ProjectName$.Client.Oqtane.pdb" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "../Server/bin/Debug/$TargetFramework/$ProjectName$.Server.Oqtane.dll" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "../Server/bin/Debug/$TargetFramework/$ProjectName$.Server.Oqtane.pdb" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "../Shared/bin/Debug/$TargetFramework/$ProjectName$.Shared.Oqtane.dll" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "../Shared/bin/Debug/$TargetFramework/$ProjectName$.Shared.Oqtane.pdb" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -rf "../Server/wwwroot/"* "../../oqtane.framework/Oqtane.Server/wwwroot/"