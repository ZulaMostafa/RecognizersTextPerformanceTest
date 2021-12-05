using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public static class EnvironmentVariables
    {
        private const string CulturesConfiguration = "TR_CULTURES";
        private const string RecognizersConfiguration = "TR_RECOGNIZERS";
        private const string OperationName = "TR_OPERATION_NAME";
        private const string MainDirectory = "TR_MAIN_DIRECTORY";
        private const string CurrentReleaseDirectory = "TR_CURRENT_RELEASE_DIRECTORY";
        private const string NextReleaseDirectory = "TR_NEXT_RELEASE_DIRECTORY";

        public static string GetCulturesConfiguration() 
            => Environment.GetEnvironmentVariable(CulturesConfiguration);
        public static string GetRecognizersConfiguration()
            => Environment.GetEnvironmentVariable(RecognizersConfiguration);
        public static string GetOperationName()
            => Environment.GetEnvironmentVariable(OperationName);
        public static string GetMainDirectory()
            => Environment.GetEnvironmentVariable(MainDirectory);
        public static string GetCurrentReleaseDirectory()
            => Environment.GetEnvironmentVariable(CurrentReleaseDirectory);
        public static string GetNextReleaseDirectory()
            => Environment.GetEnvironmentVariable(NextReleaseDirectory);
    }
}
