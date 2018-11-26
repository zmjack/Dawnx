::==== ~Root ====
nuget push "Dawnx/bin/Release/Dawnx.1.6.1.nupkg" -source nuget.org
nuget push "Dawnx.Tools/bin/Release/Dawnx.Tools.1.6.1.nupkg" -source nuget.org
nuget push "Dawnx.Diagnostics/bin/Release/Dawnx.Diagnostics.1.6.1.nupkg" -source nuget.org
nuget push "DawnxLite/bin/Release/DawnxLite.1.6.1.nupkg" -source nuget.org
::==== ~AspNetCore ====
nuget push "~Library/~AspNetCore/Dawnx.AspNetCore/bin/Release/Dawnx.AspNetCore.1.6.1.nupkg" -source nuget.org
nuget push "~Library/~AspNetCore/Dawnx.AspNetCore.IdentityUtility/bin/Release/Dawnx.AspNetCore.IdentityUtility.1.6.1.nupkg" -source nuget.org
nuget push "~Library/~AspNetCore/Dawnx.AspNetCore.LiveAccountUtility/bin/Release/Dawnx.AspNetCore.LiveAccountUtility.1.6.1.nupkg" -source nuget.org
::==== ~Net ====
nuget push "~Library/~Net/Dawnx.Net/bin/Release/Dawnx.Net.1.6.1.nupkg" -source nuget.org
::==== ~NPOI ====
nuget push "~Library/~NPOI/Dawnx.NPOI/bin/Release/Dawnx.NPOI.1.6.1.nupkg" -source nuget.org
::==== ~Security ====
nuget push "~Library/~Security/Dawnx.Security/bin/Release/Dawnx.Security.1.6.1.nupkg" -source nuget.org
::==== ~Xml ====
nuget push "~Library/~Xml/Dawnx.Xml/bin/Release/Dawnx.Xml.1.6.1.nupkg" -source nuget.org
pause
