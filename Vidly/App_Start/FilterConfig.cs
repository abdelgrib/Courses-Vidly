using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); //filter executed after an action and if there is an unhandled excep in that action it will catch it and render custom error view
            //is applyed to all ctrls and all actions, can be exec before and/or after action
            //custom error page (Views/Shared/CustomError) used for exception errors; response is returned from ASP.NET not ISS (ISS for 404, HttpNotFound())
            //appliquer sur une action ou un controller (toutes les actions) [Authorisation] ou ici globalement
            //appliquer globalement la restriction, on peut override localement avec [AllowAnonymous], [Authorisation(Roles=",,")] plus strict
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute()); //pour n'autoriser que la con avec https qui sera usée pour OAuth (Facebook)

            //So
            //exception errors => ASP.NET, HandleErrorAttribute(), Views/Shared/CustomError when On in web.config <customErrors mode="on"
            
            //.../Home/About2 (non existing action): 404 => Erreur du serveur dans l'application '/', La ressource est introuvable.
            // - <error statusCode="404" redirect="~/404.html"/> inside <customErrors mode="On"> et create the file

            //.../imge.jpeg : 404 => page 404 (celle modifiée tout au début) car : served by IIS directly
            //.../Customers/Edit/999 : 404 => page 404
            // - need to change IIS config <system.webServer>
        }
    }
}
