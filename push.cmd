::==== ~Root ====
nuget push "Dawnx/bin/Release/Dawnx.1.24.0.nupkg" -source nuget.org
nuget push "Dawnx.Tools/bin/Release/Dawnx.Tools.1.24.0.nupkg" -source nuget.org
nuget push "Dawnx.Diagnostics/bin/Release/Dawnx.Diagnostics.1.24.0.nupkg" -source nuget.org
nuget push "DawnxLite/bin/Release/DawnxLite.1.24.0.nupkg" -source nuget.org

::==== ~Dawnx.Library ====
nuget push "~Library/Dawnx.AspNetCore/bin/Release/Dawnx.AspNetCore.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.AspNetCore.IdentityUtility/bin/Release/Dawnx.AspNetCore.IdentityUtility.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.AspNetCore.LiveAccountUtility/bin/Release/Dawnx.AspNetCore.LiveAccountUtility.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.LuaEngine/bin/Release/Dawnx.LuaEngine.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.Net/bin/Release/Dawnx.Net.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.NPOI/bin/Release/Dawnx.NPOI.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.Security/bin/Release/Dawnx.Security.1.24.0.nupkg" -source nuget.org
nuget push "~Library/Dawnx.Xml/bin/Release/Dawnx.Xml.1.24.0.nupkg" -source nuget.org

::==== Dawnx.Chinese ====
nuget push "~Localization/Dawnx.Chinese/bin/Release/Dawnx.Chinese.1.24.0.nupkg" -source nuget.org

::==== Others ====
nuget push "Def/bin/Release/Def.1.0.0.nupkg" -source nuget.org
nuget push "NLinq/bin/Release/NLinq.0.1.7.nupkg" -source nuget.org
nuget push "TypeSharp/bin/Release/TypeSharp.0.1.6.nupkg" -source nuget.org

pause
