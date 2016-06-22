using OnlinerNotifier.BLL.Models.OnlinerDataModels;

namespace OnlinerNotifier.BLL.Services
{
    public interface IOnlinerSearchService
    {
        SearchResultOnliner Search(string productName);
    }
}
