

using MSBuild.Community.Tasks.Subversion;
using NUnit.Framework;

namespace MSBuild.Community.Tasks.Tests.Subversion
{
    [TestFixture]
    public class SvnCopyTest
    {

        [Test]
        public void SvnCopyExecute()
        {
            var context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext();
            SvnCopy task = new SvnCopy(context.CakeContext);
            task.Message = "Tagging";
            task.SourcePath = "file:///d:/svn/trunk/path";
            task.DestinationPath = "file:///d:/svn/tags/release/path";
            string expectedCommand = "copy \"file:///d:/svn/trunk/path\" \"file:///d:/svn/tags/release/path\" --message \"Tagging\" --non-interactive --no-auth-cache";
            string actualCommand = TaskUtility.GetToolTaskCommand(task);
            Assert.AreEqual(expectedCommand, actualCommand);
        }

        [Test]
        public void SvnCopyBuildTreeExecute()
        {
            var context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext().CakeContext;
            SvnCopy task = new SvnCopy(context);
            task.Message = "Tagging";
            task.SourcePath = "file:///d:/svn/trunk/path";
            task.DestinationPath = "file:///d:/svn/tags/release/path";
            task.BuildTree = true;
            string expectedCommand = "copy \"file:///d:/svn/trunk/path\" \"file:///d:/svn/tags/release/path\" --parents --message \"Tagging\" --non-interactive --no-auth-cache";
            string actualCommand = TaskUtility.GetToolTaskCommand(task);
            Assert.AreEqual(expectedCommand, actualCommand);
        }
    }
}
