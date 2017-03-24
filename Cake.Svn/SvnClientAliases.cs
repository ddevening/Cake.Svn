using Cake.Core;
using Cake.Core.Annotations;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MSBuild.Community.Tasks.Subversion;
using MSBuild.Community.Tasks.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Cake.Svn
{
    [CakeAliasCategory("SVN Tools")]
    public static class SvnClientAliases
    {
        [CakeMethodAlias]
        public static SvnResultsBase SvnUpdate(this ICakeContext context,
            string localPath,           
            string username, 
            string password)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnUpdate task = new SvnUpdate(context);
            task.Username = username;
            task.Password = password;
            task.LocalPath = localPath;            

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }
            results.RepositoryPath = task.RepositoryPath ?? "";
            results.Revision = task.Revision;
            results.StandardOutput = task.StandardOutput ?? "";
            results.StandardError = task.StandardError ?? "";
            results.ExitCode = task.ExitCode;

            return results;
        }

        [CakeMethodAlias]
        public static SvnResultsBase SvnCopy(this ICakeContext context,
            string message,
            string SourcePath,
            string DestinationPath,
            string taglabel,
            string username, string password)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnCopy task = new SvnCopy(context);
            task.Username = username;
            task.Password = password;
            task.SourcePath = SourcePath;
            task.DestinationPath = DestinationPath;
            task.Message = message;

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }
            results.RepositoryPath = task.RepositoryPath ?? "";
            results.Revision = task.Revision;
            results.StandardOutput = task.StandardOutput ?? "";
            results.StandardError = task.StandardError ?? "";
            results.ExitCode = task.ExitCode;

            return results;
        }

        [CakeMethodAlias]
        public static SvnResultsBase SvnExport(this ICakeContext context,
        string repositorypath,
        string localPath,
        bool force,
        string username, string password)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnExport task = new SvnExport(context);
            task.Username = username;
            task.Password = password;
            task.RepositoryPath = repositorypath;
            task.LocalPath = localPath;
            task.Force = force;

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }
            results.RepositoryPath = task.RepositoryPath ?? "";
            results.Revision = task.Revision;
            results.StandardOutput = task.StandardOutput ?? "";
            results.StandardError = task.StandardError ?? "";
            results.ExitCode = task.ExitCode;

            return results;
        }

        [CakeMethodAlias]
        public static SvnResultsBase SvnExport(this ICakeContext context,
            string repositorypath,
            string localPath,
            string username, string password)
        {
            return SvnExport(context,
                repositorypath,
                localPath,
                false,
                username, password);
        }

        [CakeMethodAlias]
        public static SvnResultsBase SvnCheckout(this ICakeContext context, string repositorypath, string localPath, string username, string password)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnCheckout task = new SvnCheckout(context);
            task.Username = username;
            task.Password = password;
            task.RepositoryPath = repositorypath;
            task.LocalPath = localPath;

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }
            results.RepositoryPath = task.RepositoryPath ?? "";
            results.Revision = task.Revision;
            results.StandardOutput = task.StandardOutput ?? "";
            results.StandardError = task.StandardError ?? "";
            results.ExitCode = task.ExitCode;

            return results;
        }

        [CakeMethodAlias]
        public static SvnInfoResults SvnInfo(this ICakeContext context, string repositorypath, string LocalPath, string username, string password)
        {
            SvnInfoResults results = new SvnInfoResults();
            SvnInfo task = new SvnInfo(context);
            task.Username = username;
            task.Password = password;
            task.RepositoryPath = repositorypath;
            task.LocalPath = LocalPath;
            //task.ToolPath = toolpath;// "\"" + toolpath + "\"";

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }
            results.RepositoryPath = task.RepositoryPath ?? "";
            results.Revision = task.Revision;
            results.StandardOutput = task.StandardOutput ?? "";
            results.StandardError = task.StandardError ?? "";
            results.ExitCode = task.ExitCode;

            results.LastChangedAuthor = task.LastChangedAuthor;
            results.LastChangedDate = task.LastChangedDate;
            results.LastChangedRevision = task.LastChangedRevision;
            results.NodeKind = task.NodeKind;
            results.RepositoryRoot = task.RepositoryRoot;
            results.RepositoryUuid = task.RepositoryUuid;
            results.Schedule = task.Schedule;

            return results;
        }

        /// <summary>
        /// SVNs the commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="message">The message.</param>
        /// <param name="localPath">The local path.</param>
        /// <param name="targetsinclude">The targetsinclude.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static SvnResultsBase SvnCommit(this ICakeContext context, string message, string localPath,
            List<string> targetsinclude,
            //List<string> targetsexclude,
            string username, string password)
        {
            SvnResultsBase results = new SvnResultsBase();

            SvnCommit task = new SvnCommit(context);
            task.LocalPath = localPath;
            task.Message = message;
            task.Username = username;
            task.Password = password;

            var itemsTaskItems = new List<ITaskItem>();

            foreach (var target in targetsinclude)
            {
                var targetPath = Path.Combine(localPath, target);
                itemsTaskItems.Add(new XmlNodeTaskItem("Include", targetPath, "ToCommit"));
            }
            //foreach (var target in targetsexclude)
            //{
            //    itemsTaskItems.Add(new XmlNodeTaskItem("Exclude", target, "ToCommit"));
            //}

            task.Targets = itemsTaskItems.ToArray();

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }

            results.RepositoryPath = task.RepositoryPath ?? "";
            results.Revision = task.Revision;
            results.StandardOutput = task.StandardOutput ?? "";
            results.StandardError = task.StandardError ?? "";
            results.ExitCode = task.ExitCode;

            return results;
        }

        [CakeMethodAlias]
        public static SvnResultsVersion SvnVersion(this ICakeContext context, string localPath)
        {
            SvnVersion task = new SvnVersion(context);
            task.LocalPath = localPath;
            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }

            string expectedCommand = String.Format("status \"{0}\" --xml --non-interactive --no-auth-cache", localPath);
            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            return new SvnResultsVersion()
            {
                Exported = task.Exported,
                LowRevision = task.LowRevision,
                Modifications = task.Modifications,
                Revision = task.Revision,
                Switched = task.Switched,
            };
        }

        [CakeMethodAlias]
        public static string SvnStatus(this ICakeContext context, string localPath)
        {
            List<SvnResultsStatus> results = new List<SvnResultsStatus>();
            SvnStatus task = new SvnStatus(context);

            task.LocalPath = localPath;
            var bOk = task.Execute();
            if (task.StandardError.Length > 0)
            {
                //-- fail
            }

            string expectedCommand = String.Format("status \"{0}\" --xml --non-interactive --no-auth-cache", localPath);

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            return task.StandardOutput;
        }

        //[CakeMethodAlias]
        //public static List<SvnSvnStatusResults> SvnStatus(this ICakeContext context, string localPath)
        //{
        //    List<SvnSvnStatusResults> results = new List<SvnSvnStatusResults>();
        //    SvnStatus task = new SvnStatus(context);

        //    task.LocalPath = localPath;
        //    var bOk = task.Execute();
        //    if (task.StandardError.Length > 0)
        //    {
        //        //-- fail
        //    }

        //    string expectedCommand = String.Format("status \"{0}\" --xml --non-interactive --no-auth-cache", localPath);

        //    string actualCommand = GetToolTaskCommand(task);
        //    string actualCommand2 = GetToolTaskToolPath(task);

        //    foreach (var item in task.Entries)
        //    {
        //        var newItem = new SvnSvnStatusResults();
        //        newItem.ItemSpec = item.ItemSpec;
        //        var c = item.CloneCustomMetadata();
        //        var c2 = item.GetMetadata("Revision");
        //        var e = item.MetadataNames.GetEnumerator();
        //        while (e.MoveNext())
        //        {
        //            newItem.MetadataNames.Add(e.Current.ToString());
        //        }

        //        //newItem.MetadataNames = item.MetadataNames.ToArray();
        //        results.Add(newItem);
        //    }

        //    return results;
        //}

        /// <summary>
        /// Returns the command line with arguments that a ToolTask will execute
        /// </summary>
        /// <param name="task">The ToolTask</param>
        /// <returns></returns>
        public static string GetToolTaskCommand(ToolTask task)
        {
            MethodInfo method = task.GetType().GetMethod("GenerateCommandLineCommands", BindingFlags.Instance | BindingFlags.NonPublic);
            return (string)method.Invoke(task, null);
        }

        /// <summary>
        /// Returns the full path to the command that will be executed by a ToolTask
        /// </summary>
        /// <param name="task">The ToolTask</param>
        /// <returns></returns>
        public static string GetToolTaskToolPath(ToolTask task)
        {
            MethodInfo method = task.GetType().GetMethod("GenerateFullPathToTool", BindingFlags.Instance | BindingFlags.NonPublic);
            return (string)method.Invoke(task, null);
        }
    }
}