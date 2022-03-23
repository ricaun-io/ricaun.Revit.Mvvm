using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;
using ricaun.Nuke.Components;

[CheckBuildProjectConfigurations]
class Build : NukeBuild, IPublishPack
{
    //string IHazRevitPackageBuilder.Application => "Revit.App";
    //public static int Main() => Execute<Build>(x => x.From<IPublishPack>().Build);
    public static int Main() => 1;//Execute<Build>(x => x.From<IPublishPack>().Build);
}