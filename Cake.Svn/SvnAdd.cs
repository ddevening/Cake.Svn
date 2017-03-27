
using Cake.Core;

namespace MSBuild.Community.Tasks.Subversion
{
    /// <summary>
    /// Subversion Add command
    /// </summary>
    public class SvnAdd : SvnClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SvnAdd"/> class.
        /// </summary>
        public SvnAdd(ICakeContext context) : base(context)
        {
            base.Command = "add";
            base.NonInteractive = true;
            base.NoAuthCache = true;
        }

        /// <summary>
        /// Indicates whether all task parameters are valid.
        /// </summary>
        /// <returns>
        /// true if all task parameters are valid; otherwise, false.
        /// </returns>
        protected override bool ValidateParameters()
        {
            if (base.Targets == null || base.Targets.Length == 0)
            {
                CakeContext.Log.Write(Cake.Core.Diagnostics.Verbosity.Normal, Cake.Core.Diagnostics.LogLevel.Error, "The \"{0}\" task was not given a value for the required parameter \"{1}\"", "SvnAdd", "Targets");
                //Log.LogError("The \"{0}\" task was not given a value for the required parameter \"{1}\"", "SvnAdd", "Targets");
                return false;
            }
            return base.ValidateParameters();
        }
    }
}