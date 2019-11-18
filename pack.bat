@echo off

set PackageSource=d:\nuget_repo

del src\WebsiteBase\bin\*.nupkg /s

set /p Build=<ver.txt
set /a Build=%Build%+1
echo %Build%>ver.txt
set SemVer=1.0.%Build%-pre
echo Packaging using version: %SemVer%

REM setlocal EnableDelayedExpansion
REM set n=0
REM for %%p in (%projects%) do (
REM    set vector[!n!]=%%p
REM    set /A n+=1
REM    echo %%p
REM )

msbuild -t:pack src\WebsiteBase/WebsiteBase.csproj -verbosity:quiet -p:PackageVersion=%SemVer%


REM For /R . %%G IN (src\*.nupkg) do (nuget push %%G -source %PackageSource%


For /R . %%G IN (*.nupkg) do (call :pack "%%G")
GOTO :eof

:pack
 echo nuget push package: %1
 nuget push %1 -source %PackageSource%
 REM nuget update src\Website1\Website1.csproj -Id WebsiteBase -source %PackageSource%
 REM dotnet add src\Website1\Website1.csproj package WebsiteBase -v %SemVer% -s %PackageSource%
 cd src\Website1\
 dotnet add Website1.csproj package -s %PackageSource% WebsiteBase
 cd ..
 cd ..
 REM nuget restore src\Website1\Website1.csproj
 REM nuget update src\Website1\Website1.csproj -ID WebsiteBase -source %PackageSource%
GOTO :eof
