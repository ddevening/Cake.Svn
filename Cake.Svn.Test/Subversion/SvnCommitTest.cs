

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MSBuild.Community.Tasks.Subversion;
using NUnit.Framework;

namespace MSBuild.Community.Tasks.Tests.Subversion
{
    [TestFixture]
    public class SvnCommitTest
    {
        [Test]
        public void SvnCommitExecute()
        {
            var context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext().CakeContext;
            SvnCommit commit = new SvnCommit(context);
            commit.Targets = new ITaskItem[] {new TaskItem("a.txt"), new TaskItem("b.txt")};
            commit.Message = "Test";
            string expectedCommand = "commit \"a.txt\" \"b.txt\" --message \"Test\" --non-interactive --no-auth-cache";
            string actualCommand = TaskUtility.GetToolTaskCommand(commit);
            string actualCommand2 = TaskUtility.GetToolTaskToolPath(commit);

            Assert.AreEqual(expectedCommand, actualCommand);
        }
    }
}