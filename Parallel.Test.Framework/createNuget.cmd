nuget pack Parallel.Test.Framework.csproj -properties Configuration=Release -version 0.0.48

REM dotnet nuget push Parallel.Test.Framework.1.0.0.nupkg  -k nugetKeyPls  -s https://www.nuget.org/

pause