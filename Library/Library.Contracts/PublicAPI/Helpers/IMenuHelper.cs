using Library.Contracts.Models.Menu;

namespace Library.Contracts.PublicAPI.Helpers
{
    public interface IMenuHelper
    {
        void NavigateToMainMenu();
        MainMenu DoMainMenuSelection();
        SearchFor DoSearchMenuSelection();
    }
}
