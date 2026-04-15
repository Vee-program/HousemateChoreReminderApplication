using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(Housemate housemate, string password);
        Task<string> Login(string username, string? password);
        Task<string> ForgotUsername(string phoneNumber);
    }
}
