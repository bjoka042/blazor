@echo off

set PackageSource=d:\nuget_repo

del src\Components\bin\*.nupkg /s
del src\Ramverk\bin\*.nupkg /s

set /p Build=<ver.txt
set /a Build=%Build%+1
echo %Build%>ver.txt

>ver.txt echo %Build%
set SemVer=1.0.%Build%-pre
echo Packaging using version: %SemVer%

dotnet pack src/Components/Components.csproj -p:verbosity=quiet -p:PackageVersion=%SemVer%
dotnet pack src/Ramverk/Ramverk.csproj -p:verbosity=quiet -p:PackageVersion=%SemVer%

For /R . %%G IN (*.nupkg) do (dotnet nuget push %%G -s %PackageSource%)

REM For /R . %%G IN (*.nupkg) do (call :pack "%%G")
REM GOTO :eof

REM :pack
REM echo dotnet nuget push package: %1
 
REM dotnet nuget push %1 -s %PackageSource%
 
REM dotnet remove src\Website1\Website1.csproj package WebsiteBase
 
REM GOTO :eof

 dotnet add src\Website1\Website1.csproj package Components -s %PackageSource% -v %SemVer%
 dotnet add src\Website1\Website1.csproj package Ramverk -s %PackageSource% -v %SemVer%
