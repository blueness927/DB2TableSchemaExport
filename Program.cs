namespace DB2SchemaExport
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the ap    plication.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}