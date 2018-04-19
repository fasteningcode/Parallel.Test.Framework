// ReSharper disable InconsistentNaming

namespace Parallel.Test.Framework.Constants {
    public static class TestSettingsConst {
        public const string ENVIRONMENT = "ENVIRONMENT";
        public const string RUN_TEST = "RUN_TEST";
        public const string BROWSER = "BROWSER";
        public const string VERSION = "VERSION";
        public const string OS = "OS";
        public const string DEVICENAME = "DEVICENAME";
        public const string DEVICEORIENTATION = "DEVICEORIENTATION";
        public const string USERNAME_SAUCE = "USERNAME_SAUCE";
        public const string ACCESS_KEY_SAUCE = "ACCESS_KEY_SAUCE";
        public const string WAIT_SEC = "WAIT";
        public const string TIMEOUT_MIN = "TIMEOUT";
        public const string BATCH_FILE_DIRECTORY = "BATCH_FILE_DIRECTORY";
        public const string TEMP_FOLDER = "TEMP_FOLDER";
        public const string MS_BUILD_PATH = "MS_BUILD_PATH";
        public const string SOLUTION_NAME = "SOLUTION_NAME";
        public const string NUNIT_PATH = "NUNIT_PATH";
    }

    public enum RunTest {
        Local,
        Sauce
    }
}