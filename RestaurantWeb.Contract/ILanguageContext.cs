namespace RestaurantWeb.Contract;

public interface ILanguageContext
{
    string GetCurrentLanguage();
    void SetCurrentLanguage(string isoCode);
}