REM ./packages/NUnit.ConsoleRunner.3.8.0/tools/nunit-console.exe ./Parallel.Test.FrameworkTests/bin/Release/Parallel.Test.FrameworkTests.dll
REM \packages\NUnit.ConsoleRunner.3.8.0\tools\nunit-console.exe Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll
REM for building the parallel.test.framework


path %loc%;"%cd%\packages\NUnit.ConsoleRunner.3.8.0\tools"
start nunit3-console.exe
CD "%CD%\Parallel.Test.FrameworkTests\bin\Release"
nunit3-console Parallel.Test.FrameworkTests.dll

nunit3-console.exe **\Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll