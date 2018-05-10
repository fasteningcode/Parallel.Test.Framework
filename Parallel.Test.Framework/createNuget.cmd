
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe" Parallel.Test.Framework.csproj
nuget pack Parallel.Test.Framework.csproj -properties Configuration=Release -version 1.0.0.81

REM dotnet nuget push Parallel.Test.Framework.1.0.0.72  -k nugetKeyPls  -s https://www.nuget.org/

pause
Feature 
 - Create batch file for categories
Updates 
 - IEnumerator - IsNullorEmptyMethod

