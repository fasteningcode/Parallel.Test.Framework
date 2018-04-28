REM ./packages/NUnit.ConsoleRunner.3.8.0/tools/nunit-console.exe ./Parallel.Test.FrameworkTests/bin/Release/Parallel.Test.FrameworkTests.dll
REM \packages\NUnit.ConsoleRunner.3.8.0\tools\nunit-console.exe Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll
REM for building the parallel.test.framework

REM nunit3-console.exe c:\Projects\Parallel.Test.Framework\Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll


path %loc%;"%cd%\packages\NUnit.ConsoleRunner.3.8.0\tools"
start nunit3-console.exe
CD "%CD%\Parallel.Test.FrameworkTests\bin\Release"
nunit3-console Parallel.Test.FrameworkTests.dll

nunit3-console.exe **\Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll

REM for running the open cover
packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:aadhithB -target:"packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe" -targetargs:"YourTestDLL -noshadow" -output:".\coverage.xml" -filter:"+[YourSUTNamepace*]* -[YourTestNamespace*]*"


packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:"packages\xunit.runner.console.2.2.0\tools\xunit.console.x86.exe" -targetargs:"YourTestDLL -noshadow" -output:".\coverage.xml" -filter:"+[YourSUTNamepace*]* -[YourTestNamespace*]*"

REM Build Solution
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\Msbuild.exe" "./Parallel.Test.Framework/Parallel.Test.Framework.csproj" /p:Configuration=Release

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\Msbuild.exe" "./Parallel.Test.FrameworkTests/Parallel.Test.FrameworkTests.csproj" /p:Configuration=Release

