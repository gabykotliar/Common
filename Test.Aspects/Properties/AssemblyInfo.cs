using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Test.Aspects")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Test.Aspects")]
[assembly: AssemblyCopyright("Copyright ©  2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: Guid("9c6ec2dc-cf05-42f1-af46-610f24e4d15c")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// this should work but for the moment is not. It's maybe a particularity of the test project
//[assembly: Common.Aspects.CacheAspectAttribute(SlidingExpiryWindow = "60",
//    AttributeTargetTypes = "Test.Aspects.CachedClass",    
//    AttributeTargetMembers = "Get*")]