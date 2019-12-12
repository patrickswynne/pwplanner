using WGUPlanner.Views;
using Xamarin.Forms;

namespace WGUPlanner
{
    public partial class App : Application
    {
        public static PlannerRepository PlannerRepository { get; private set; }
        public static LoginRepository LoginRepository { get; private set; }
        private bool userExists = false;
        private string dbPath;

        public App(string dbPath)
        {
            InitializeComponent();
            this.dbPath = dbPath;
        }

        protected override void OnStart()
        {
            PlannerRepository = new PlannerRepository(this.dbPath);
            LoginRepository = new LoginRepository(this.dbPath);

            // loads fake student data
            FakeData.CreateFakeStudent.CreateStudentData();

            // if user exists go to login, else go to registration
            if (LoginRepository.UserExistsState)
            {
                MainPage = new Login();
            }
            else
            {
                MainPage = new Registration();
            }
        }

        protected override void OnResume()
        {
            if (LoginRepository.UserExistsState)
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new Login();
            }
        }

        protected override void OnSleep()
        {
            LoginRepository = new LoginRepository(this.dbPath);
            LoginRepository.UserExistsState = false;
        }
    }
}
