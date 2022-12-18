using RestaurantWeb.Contract;

namespace PV179_RestaurantWeb.Services;

public class CookieLanguageContext : ILanguageContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CookieLanguageKey = "lang";
    private const string DefaultIsoLanguageCode = "en";
    public CookieLanguageContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentLanguage()
    {

        if (!_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieLanguageKey, out string? langValue))
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLanguageKey, DefaultIsoLanguageCode);
            return DefaultIsoLanguageCode;
        };
        return langValue ?? DefaultIsoLanguageCode;
    }

    public void SetCurrentLanguage(string isoCode)
    {
        if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieLanguageKey, out string? langValue) &&
            langValue == isoCode)
        {
            return;
        }
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLanguageKey, isoCode);
    }
}