

using System;
using Cake.Core;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MSBuild.Community.Tasks.Subversion;
using NUnit.Framework;

namespace MSBuild.Community.Tasks.Tests.Subversion
{
    [TestFixture]
    public class SvnClientTest
    {
        private Cake.VersionReader.Tests.Fakes.FakeCakeContext _context;
        [Test]
        public void SvnClientExecute()
        {
            _context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext();
            string targetFile = "myfile.txt";
            SvnClient client = new SvnClient(_context.CakeContext);
            client.Command = "revert";
            client.Targets = new ITaskItem[] {new TaskItem(targetFile)};

            string expectedCommand = String.Format("revert \"{0}\"", targetFile);
            string actualCommand = TaskUtility.GetToolTaskCommand(client);
            Assert.AreEqual(expectedCommand, actualCommand);

        }

        [Test]
        public void SvnClientExecuteWithArguments()
        {
            _context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext();
            SvnClient client = new SvnClient(_context.CakeContext);
            client.Command = "diff";
            client.Arguments = "--non-interactive --no-auth-cache";

            string expectedCommand = "diff --non-interactive --no-auth-cache";
            string actualCommand = TaskUtility.GetToolTaskCommand(client);
            Assert.AreEqual(expectedCommand, actualCommand);

        }

        [Test]
        public void ParsesStandardUpdateRevisionOutput()
        {
            _context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext();
            SvnClientStub task = new SvnClientStub(_context.CakeContext);
            task.BuildEngine = new MockBuild();

            int actualRevision = task.GetRevisionFromLogEventsFromTextOutput("At revision 222", MessageImportance.Low);

            const int expectedRevision = 222;

            Assert.AreEqual(expectedRevision, actualRevision);
        }

        [Test]
        public void IgnoresSkippedPathsUpdateOutput()
        {
            _context = new Cake.VersionReader.Tests.Fakes.FakeCakeContext();
            SvnClientStub task = new SvnClientStub(_context.CakeContext);
            task.BuildEngine = new MockBuild();

            //  "Skipped paths..." is a potential output of svn.exe update and follows the actual revision output
            const string updateMsg = "Skipped paths: 1";

            int actualRevision = task.GetRevisionFromLogEventsFromTextOutput(updateMsg, MessageImportance.Low);

            const int expectedRevision = -1;

            Assert.AreEqual(expectedRevision, actualRevision);
        }

        private class SvnClientStub : SvnClient
        {

            private int _revisionResult;

            public SvnClientStub(ICakeContext context) : base(context)
            {
            }

            public int GetRevisionFromLogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
            {
                LogEventsFromTextOutput(singleLine, messageImportance);

                return _revisionResult;
            }

            protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
            {
                base.LogEventsFromTextOutput(singleLine, messageImportance);

                _revisionResult = Revision;
            }
        }
    }

}