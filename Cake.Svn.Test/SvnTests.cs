using Cake.Svn;
using Cake.VersionReader.Tests.Fakes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cake.SvnStatusTest.Tests
{
    [TestFixture]
    public class SvnTests
    {
        private FakeCakeContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new FakeCakeContext();
        }

        [Test]
        public void GetSvnCommit()
        {
            List<string> Includelist = new List<string>();
            Includelist.Add(@"build.cake");
            List<string> ExcludeList = new List<string>();

            var result = _context.CakeContext.SvnCommit("Test Commit - Cake Build",
                @"C:\Westport\DOTNET2008\trunk\Setup\Cake Build",
                Includelist,
                //ExcludeList,
                "ddbuild",
                "p");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnAdd()
        {
            List<string> Includelist = new List<string>();
            Includelist.Add(@"junk.txt");

            var result = _context.CakeContext.SvnAdd(
                @"C:\Westport\DOTNET2008\trunk\Setup\Cake Build",
                Includelist);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GetSvnAddFolder()
        {
            var result = _context.CakeContext.SvnAdd(
                @"C:\Westport\DOTNET2008\trunk\Setup\Cake Build\bak");
            Assert.That(result, Is.Not.Null);
        }

        

        [Test]
        public void GetSvnVersion()
        {
            var result = _context.CakeContext.SvnVersion(@"C:\Westport\DOTNET2008\trunk");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnStatus()
        {
            var result = _context.CakeContext.SvnStatus(@"C:\Westport\DOTNET2008\trunk");
            Assert.That(result, Is.EqualTo("1.1.2"));
        }

        [Test]
        public void GetSvnInfoFile()
        {
            var result = _context.CakeContext.SvnInfo(@"C:\Westport\DOTNET2008\trunk\Setup\Cake Build\build.cake");
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GetSvnInfo()
        {
            var result = _context.CakeContext.SvnInfo(@"C:\Westport\DOTNET2008\trunk\Setup\Cake Build");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnCheckout()
        {
            var result =
                _context.CakeContext.SvnCheckout(@"svn://10.10.1.103/westport/DOTNET2008/trunk/Setup/Cake Build",
                    @"F:\dd-cakeaddin\Cake.Svn\Westport");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnExport()
        {
            var result = _context.CakeContext.SvnExport(@"svn://10.10.1.103/westport/DOTNET2008/trunk/Setup/Cake Build",
                @"F:\dd-cakeaddin\Cake.Svn\Westport",
                "ddbuild",
                "p");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnExportForce()
        {
            var result = _context.CakeContext.SvnExport(@"svn://10.10.1.103/westport/DOTNET2008/trunk/Setup/Cake Build",
                @"F:\dd-cakeaddin\Cake.Svn\Westport",
                true,
                "ddbuild",
                "p");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnCopy()
        {
            var result = _context.CakeContext.SvnCopy(
                "tag Cake Build",
                @"svn://10.10.1.103/westport/DOTNET2008/trunk/Setup/Cake Build",
                @"svn://10.10.1.103/westport/DOTNET2008/tags/Setup/Cake Build",
                "mytagV1.0",
                "ddbuild",
                "p");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSvnUpdate()
        {
            //var result1 =_context.CakeContext.SvnCheckout(@"svn://10.10.1.103/westport/DOTNET2008/trunk/Setup/Cake Build",
            //    @"F:\dd-cakeaddin\Cake.Svn\Westport");
            //var result = _context.CakeContext.SvnUpdate(
            //    @"C:\Westport\DOTNET2008\trunk\Setup\Cake Build");
            var result = _context.CakeContext.SvnUpdate(
                    @"F:\dd-cakeaddin\Cake.Svn\Westport");
            Assert.That(result, Is.Not.Null);
        }
    }
}