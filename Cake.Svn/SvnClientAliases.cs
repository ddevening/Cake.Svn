using Cake.Core;
using Cake.Core.Annotations;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MSBuild.Community.Tasks.Subversion;
using MSBuild.Community.Tasks.Xml;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Cake.Svn
{
    [CakeAliasCategory("SVN Tools")]
    public static class SvnClientAliases
    {
        [CakeMethodAlias]
        public static SvnResultsBase SvnUpdate(this ICakeContext context,
            string localPath)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnUpdate task = new SvnUpdate(context)
            {
                LocalPath = localPath
            };

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
            string sourcePath,
            string destinationPath,
            string taglabel,
            string username,
            string password)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnCopy task = new SvnCopy(context)
            {
                Username = username,
                Password = password,
                SourcePath = sourcePath,
                DestinationPath = destinationPath,
                Message = message
            };

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
        string username,
        string password)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnExport task = new SvnExport(context)
            {
                Username = username,
                Password = password,
                RepositoryPath = repositorypath,
                LocalPath = localPath,
                Force = force
            };

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
            string username,
            string password)
        {
            return SvnExport(context,
                repositorypath,
                localPath,
                false,
                username, password);
        }

        [CakeMethodAlias]
        public static SvnResultsBase SvnCheckout(this ICakeContext context,
            string repositorypath,
            string localPath)
        {
            SvnResultsBase results = new SvnResultsBase();
            SvnCheckout task = new SvnCheckout(context)
            {
                RepositoryPath = repositorypath,
                LocalPath = localPath
            };

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
        public static SvnInfoResults SvnInfo(this ICakeContext context,
            string localPath)
        {
            SvnInfoResults results = new SvnInfoResults();
            SvnInfo task = new SvnInfo(context)
            {
                LocalPath = localPath
            };
            //task.ToolPath = toolpath;// "\"" + toolpath + "\"";

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
        public static SvnResultsBase SvnCommit(this ICakeContext context,
            string message,
            string localPath,
            List<string> targetsinclude,
            //List<string> targetsexclude,
            string username,
            string password)
        {
            SvnResultsBase results = new SvnResultsBase();

            SvnCommit task = new SvnCommit(context)
            {
                LocalPath = localPath,
                Message = message,
                Username = username,
                Password = password
            };

            var itemsTaskItems = new List<ITaskItem>();

            foreach (var target in targetsinclude)
            {
                var targetPath = Path.Combine(localPath, target);
                if (File.Exists(targetPath))
                {
                    itemsTaskItems.Add(new XmlNodeTaskItem("Include", targetPath, "ToCommit"));
                }
                else if (File.Exists(target))
                {
                    itemsTaskItems.Add(new XmlNodeTaskItem("Include", target, "ToCommit"));
                }
                //itemsTaskItems.Add(new XmlNodeTaskItem("Include", targetPath, "ToCommit"));
            }

            task.Targets = itemsTaskItems.ToArray();

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
        public static SvnResultsBase SvnCommit(this ICakeContext context,
            string message,
            string localPath,
            List<string> targetsinclude)
        {
            SvnResultsBase results = new SvnResultsBase();

            SvnCommit task = new SvnCommit(context)
            {
                LocalPath = localPath,
                Message = message,
            };

            var itemsTaskItems = new List<ITaskItem>();

            foreach (var target in targetsinclude)
            {
                var targetPath = Path.Combine(localPath, target);
                if (File.Exists(targetPath))
                {
                    itemsTaskItems.Add(new XmlNodeTaskItem("Include", targetPath, "ToCommit"));
                } 
                else if (File.Exists(target))
                {
                    itemsTaskItems.Add(new XmlNodeTaskItem("Include", target, "ToCommit"));
                }
                //itemsTaskItems.Add(new XmlNodeTaskItem("Include", targetPath, "ToCommit"));
            }

            task.Targets = itemsTaskItems.ToArray();

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
        public static SvnResultsBase SvnAdd(this ICakeContext context,
        string localPath)

        {
            SvnResultsBase results = new SvnResultsBase();

            SvnAdd task = new SvnAdd(context)
            {
                //LocalPath = localPath,
            };

            var itemsTaskItems = new List<ITaskItem>();
            itemsTaskItems.Add(new XmlNodeTaskItem("Include", localPath, "ToCommit"));
            task.Targets = itemsTaskItems.ToArray();

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
        public static SvnResultsBase SvnAdd(this ICakeContext context,
            string localPath,
            List<string> targetsinclude)
    
        {
            SvnResultsBase results = new SvnResultsBase();

            SvnAdd task = new SvnAdd(context)
            {
                //LocalPath = localPath,
            };

            var itemsTaskItems = new List<ITaskItem>();

            foreach (var target in targetsinclude)
            {
                var targetPath = Path.Combine(localPath, target);
                itemsTaskItems.Add(new XmlNodeTaskItem("Include", targetPath, "ToCommit"));
            }

            task.Targets = itemsTaskItems.ToArray();

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
        public static SvnResultsVersion SvnVersion(this ICakeContext context,
            string localPath)
        {
            SvnVersion task = new SvnVersion(context)
            {
                LocalPath = localPath
            };

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            var bOk = task.Execute();
            if (task.ExitCode != 0)
            {
                //-- fail
            }

            return new SvnResultsVersion()
            {
                Exported = task.Exported,
                LowRevision = task.LowRevision,
                Modifications = task.Modifications,
                Revision = task.Revision,
                Switched = task.Switched,
                ExitCode = task.ExitCode,
            };
        }

        [CakeMethodAlias]
        public static string SvnStatus(this ICakeContext context,
            string localPath)
        {
            List<SvnResultsStatus> results = new List<SvnResultsStatus>();
            SvnStatus task = new SvnStatus(context)
            {
                LocalPath = localPath
            };

            string actualCommand = GetToolTaskCommand(task);
            string actualCommand2 = GetToolTaskToolPath(task);

            var bOk = task.Execute();
            if (task.StandardError.Length > 0)
            {
                //-- fail
            }

            return task.StandardOutput;
        }

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