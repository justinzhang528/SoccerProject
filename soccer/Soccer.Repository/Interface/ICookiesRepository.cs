using Soccer.Repository.Models;

namespace Soccer.Repository.Interface
{
    public interface ICookiesRepository
    {
        CookiesModel GetCookiesByCondition(string website, string name);
        void UpdateCookies(List<CookiesModel> cookies);
    }
}