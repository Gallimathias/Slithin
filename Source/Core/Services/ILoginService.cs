﻿namespace Slithin.Core.Services
{
    public interface ILoginService
    {
        LoginInfo GetLoginCredentials();

        void RememberLoginCredencials(LoginInfo viewModel);
    }
}
