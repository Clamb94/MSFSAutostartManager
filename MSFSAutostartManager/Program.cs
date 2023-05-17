using CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace MSFSAutostartManager
{
    internal class Program
    {

        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AddOptions))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RemoveOptions))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(IsEnabledOptions))]
        static int Main(string[] args)
        {
            //string[] modifiedArgs = ModifyArguments(args);
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
                FindExeXml finder = new FindExeXml();
                string xmlPath = finder.ExeXmlPath;
                ModifyAutostart modifier = new(opts.Name, xmlPath);

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
                FindExeXml finder = new FindExeXml();
                string xmlPath = finder.ExeXmlPath;
                ModifyAutostart modifier = new(opts.Name, xmlPath);

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
                FindExeXml finder = new FindExeXml();
                string xmlPath = finder.ExeXmlPath;
                ModifyAutostart modifier = new(opts.Name, xmlPath);

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