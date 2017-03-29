#addin "Cake.Incubator"

//#r "F:\dd-cakeaddin\Cake.Svn\Cake.Svn\bin\Debug\Cake.Svn.dll"
#r "..\Cake.Svn\bin\Debug\Cake.Svn.dll"

using Cake.Svn;
using Cake.Incubator;

var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  //Information("Hello World!");

  var svnVersion = SvnVersion(@"C:\Westport\DOTNET2008\trunk");

  if(svnVersion.ExitCode != 0)
  {
    throw new CakeException("svn.exe exited with error: " + svnVersion.ExitCode);
  }

  Information("SVN Version " + svnVersion.Revision);
  Information("SVN Version Dump" + svnVersion.Dump());

  List<string> Includelist = new List<string>();
  Includelist.Add(@"build.cake");

  var resultCommit = SvnCommit("UPDATE: Test Commit - Cake Build",
                @"C:\Westport\DOTNET2008\trunk\Setup\Cake Build",
                Includelist,                
                "ddbuild",
                "p");
 
  Information("SVN Commit Dump" + resultCommit.Dump());                

});

RunTarget(target);