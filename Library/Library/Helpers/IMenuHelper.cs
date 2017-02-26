using Library.Menu;

namespace Library.Helpers
{
    public interface IMenuHelper
    {
        void NavigateToMainMenu();
        MainMenu DoMainMenuSelection();
        SearchFor DoSearchMenuSelection();
    }
}
