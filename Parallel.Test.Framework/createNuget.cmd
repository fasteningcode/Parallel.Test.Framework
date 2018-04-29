nuget pack Parallel.Test.Framework.csproj -properties Configuration=Release -version 1.0.0.73

nuget push Parallel.Test.Framework.1.0.0.73.nupkg oy2bvbq5msk4gzgoqk3q3lrmddoqoizepg6bkcfi8ov0av5ly5  -Source https://api.nuget.org/v3/index.json

pause
