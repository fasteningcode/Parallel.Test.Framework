packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:aadhithB -target:"packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe" -targetargs:"c:\Projects\Parallel.Test.Framework\Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll" -output:".\coverage.xml" -filter:"+[Parallel.Test.Framework*]* -[Parallel.Test.FrameworkTests*]*"

REM codecov.sh -f "coverage.xml" -t f9537881-dd22-4e98-878f-47d7643499bf
REM nunit3-console.exe c:\Projects\Parallel.Test.Framework\Parallel.Test.FrameworkTests\bin\Release\Parallel.Test.FrameworkTests.dll