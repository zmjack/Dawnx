dotnet restore
dotnet build Dawnx.sln

::==== ~Root ====
dotnet test Dawnx.Test/Dawnx.Test.csproj
dotnet test Dawnx.Tools.Test/Dawnx.Tools.Test.csproj
::==== ~AspNetCore ====
dotnet test "~Library/~AspNetCore/Dawnx.AspNetCore/Dawnx.AspNetCore.csproj"
dotnet test "~Library/~AspNetCore/Dawnx.AspNetCore.IdentityUtility/Dawnx.AspNetCore.IdentityUtility.csproj"
dotnet test "~Library/~AspNetCore/Dawnx.AspNetCore.LiveAccountUtility/Dawnx.AspNetCore.LiveAccountUtility.csproj"
::==== ~Net ====
dotnet test "~Library/~AspNetCore/Dawnx.Net/Dawnx.Net.csproj"
::==== ~NPOI ====
dotnet test "~Library/~AspNetCore/Dawnx.NPOI/Dawnx.NPOI.csproj"
::==== ~Security ====
dotnet test "~Library/~AspNetCore/Dawnx.Security/Dawnx.Security.csproj"
::==== ~Xml ====
dotnet test "~Library/~AspNetCore/Dawnx.Xml/Dawnx.Xml.csproj"
