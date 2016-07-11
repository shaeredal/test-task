using System.Collections.Generic;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces
{
    public interface INotifiableUsersProvider
    {
        List<User> GetNotifiableUsers();
    }
}
