using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;

namespace FirstChoiceApp.Manager
{
    public class LoginManager
    {
        LoginGateway loginGateway = new LoginGateway();

        internal Login GetAuthentication(Login login)
        {
            return loginGateway.GetAuthentication(login);
        }
    }
}