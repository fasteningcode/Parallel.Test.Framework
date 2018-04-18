namespace Parallel.Test.FrameworkTests {
    public static class TestSettingsConst {
        public const string BROWSER = "BROWSER";
        public const string ENVIRONMENT = "ENVIRONMENT";
        public const string DEFAULTWAITINSECONDS = "DEFAULTWAITINSECONDS ";
        public const string WAITFORFIRSTELEMTENTINMINUTES = "WAITFORFIRSTELEMTENTINMINUTES ";
        public const string RUNTEST = "RUNTEST";
        public const string VERSION = "VERSION";
        public const string OS = "OS";
        public const string DEVICENAME = "DEVICENAME";
        public const string DEVICEORIENTATION = "DEVICEORIENTATION";
        public const string USERNAME = "USERNAME";
        public const string ACCESSKEY = "ACCESSKEY";
    }

    public enum RunTest {
        Local,
        BrowserStack
    }
}