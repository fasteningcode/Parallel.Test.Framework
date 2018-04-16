namespace Parallel.Test.Framework.Constants {
    public static class TestSettingsConst {
        public const string Browser = "Browser";
        public const string Environment = "Environment";
        public const string DefaultWaitinSeconds = "DefaultWaitinSeconds ";
        public const string WaitForFirstElemtentinMinutes = "WaitForFirstElemtentinMinutes ";
        public const string RunTest = "RunTest";
        public const string Version = "Version";
        public const string Os = "OS";
        public const string DeviceName = "DeviceName";
        public const string DeviceOrientation = "DeviceOrientation";
        public const string Username = "username";
        public const string AccessKey = "accessKey";
    }

    public enum RunTest {
        Local,
        BrowserStack
    }
}