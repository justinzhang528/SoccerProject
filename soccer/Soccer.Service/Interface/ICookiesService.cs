using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface ICookiesService
    {
        CookiesModel GetCookesByCondition(string website, string name);
        void UpdateSBOCookies();
    }
}