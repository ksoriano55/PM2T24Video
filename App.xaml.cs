namespace PM2T24Video
{
    public partial class App : Application
    {
        static Controllers.VideoController? dbVideos;
        public static Controllers.VideoController DataBase
        {
            get
            {
                if (dbVideos == null)
                {
                    dbVideos = new Controllers.VideoController();
                }
                return dbVideos;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
