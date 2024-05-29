using System.Diagnostics.CodeAnalysis;
using CommandLine;

namespace MSFSAutostartManager
{
    internal class Program
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AddOptions))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RemoveOptions))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(IsEnabledOptions))]
        static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<AddOptions, RemoveOptions, IsEnabledOptions>(args)
                .MapResult(
                  (AddOptions opts) => RunAddAndReturnExitCode(opts),
                  (RemoveOptions opts) => RunRemoveAndReturnExitCode(opts),
                  (IsEnabledOptions opts) => RunIsEnabledAndReturnExitCode(opts),
                  errs => -1);
        }

        private static int RunAddAndReturnExitCode(AddOptions opts)
        {
            try
            {              
                ModifyAutostart modifier = new(opts.Name, opts.XmlPath);

                modifier.Add(opts);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }

        }

        private static int RunRemoveAndReturnExitCode(RemoveOptions opts)
        {
            try
            {
                ModifyAutostart modifier = new(opts.Name, opts.XmlPath);

                modifier.Remove();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        private static int RunIsEnabledAndReturnExitCode(IsEnabledOptions opts)
        {
            try
            {
                ModifyAutostart modifier = new(opts.Name, opts.XmlPath);

                Console.WriteLine(modifier.CheckEnabled().ToString());
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }




    }
}