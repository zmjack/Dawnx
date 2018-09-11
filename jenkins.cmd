dotnet restore Dawnx.sln
dotnet build Dawnx.sln

dotnet test Dawnx.Test/Dawnx.Test.csproj
dotnet test Dawnx.Tools.Test/Dawnx.Tools.Test.csproj
dotnet test "~Library/~AspNetCore/Dawnx.AspNetCore.Test/Dawnx.AspNetCore.Test.csproj"
dotnet test "~Library/~Net/Dawnx.Net.Test/Dawnx.Net.Test.csproj"
dotnet test "~Library/~NPOI/Dawnx.NPOI.Test/Dawnx.NPOI.Test.csproj"
dotnet test "~Library/~Security/Dawnx.Security.Test/Dawnx.Security.Test.csproj"
dotnet test "~Library/~Xml/Dawnx.Xml.Test/Dawnx.Xml.Test.csproj"
pause
