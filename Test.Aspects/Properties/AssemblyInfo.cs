using System.Reflection;
using System.Runtime.InteropServices;

using Common.Aspects;

[assembly: AssemblyTitle("Test.Aspects")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Test.Aspects")]
[assembly: AssemblyCopyright("Copyright ©  2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("9c6ec2dc-cf05-42f1-af46-610f24e4d15c")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: CacheAspect(SlidingExpiryWindow = "60",
    AttributeTargetTypes = "Test.Aspects.CachedClass",    
    AttributeTargetMembers = "Get")]