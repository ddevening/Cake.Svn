#addin "Cake.FileHelpers"

var Project = Directory("./Cake.Svn/");
var TestProject = Directory("./Cake.SvnTests/");
var CakeSvnProj = Project + File("Cake.Svn.csproj");
var CakeTestSvnProj = TestProject + File("Cake.Svn.Test.csproj");
var CakeTestSvnAssembly = TestProject + Directory("bin/Release") + File("Cake.Svn.Tests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var CakeSvnSln = File("./Cake.Svn.sln");
var CakeSvnNuspec = File("./Cake.Svn.nuspec");
var Nupkg = Directory("./nupkg");

var target = Argument("target", "Default");
var version = "";

Task("Default")
	.Does (() =>
	{
		Information("Run Default");
		NuGetRestore (CakeSvnSln);
		DotNetBuild (CakeSvnSln, c => {
			c.Configuration = "Release";
			c.Verbosity = Verbosity.Minimal;
		});
});

Task("UnitTest")
	.IsDependentOn("Default")
	.Does(() =>
	{
		Information("Run UnitTest");
		NUnit3(CakeTestSvnAssembly);
	});

Task("NuGetPack")
	.IsDependentOn("GetVersion")
	.IsDependentOn("Default")
	.IsDependentOn("UnitTest")
	.Does (() =>
{
	Information("Run NuGetPack");
	CreateDirectory(Nupkg);
	NuGetPack (CakeSvnNuspec, new NuGetPackSettings { 
		Version = version,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = Nupkg,
		BasePath = "./",
	});	
});

Task("GetVersion")
	.Does(() => {
		Information("Run GetVersion");
		var assemblyInfo = ParseAssemblyInfo(AssemblyInfo);
		var semVersion = string.Join(".", assemblyInfo.AssemblyVersion.Split('.').Take(3));
		Information("Version {0}", semVersion);
		version = semVersion;
	});

RunTarget (target);
