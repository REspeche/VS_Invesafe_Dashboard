using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class DashboardController : BaseController
    {
        // Dashboard

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Home()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Home()
        {
            return PartialView("Home");
        }

        // Situacion Patrimonial

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult PatrimonialSituation()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _PatrimonialSituation()
        {
            return PartialView("PatrimonialSituation");
        }

        // Oportunidades

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Opportunities()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Opportunities()
        {
            return PartialView("Opportunities");
        }

        // Mis Inversiones

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult MyInvestments()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _MyInvestments()
        {
            return PartialView("MyInvestments");
        }

        // Mi Cuenta
        // Mi Cuenta/Datos Principales
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult PersonalData()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _PersonalData()
        {
            return PartialView("PersonalData");
        }

        // Mi Cuenta/Datos Adicionales
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult AditionalData()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _AditionalData()
        {
            return PartialView("AditionalData");
        }

        // Mi Documentación
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult PersonalDocuments()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _PersonalDocuments()
        {
            return PartialView("PersonalDocuments");
        }

        // Mi Cuenta/Acreditar Cuenta
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult TypeAccount()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _TypeAccount()
        {
            return PartialView("TypeAccount");
        }

        // Mi Cuenta/Cambiar Contrasena
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _ChangePassword()
        {
            return PartialView("ChangePassword");
        }

        // Método de Autentificación
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult AuthenticationMethod()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _AuthenticationMethod()
        {
            return PartialView("AuthenticationMethod");
        }

        // Actividad Bancaria
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult BankActivity()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _BankActivity()
        {
            return PartialView("BankActivity");
        }

        // Cuentas Bancarias
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult BankAccounts()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _BankAccounts()
        {
            return PartialView("BankAccounts");
        }

        // Informes Fiscales
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult FiscalReports()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _FiscalReports()
        {
            return PartialView("FiscalReports");
        }

        // Documentos legales
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult LegalDocuments()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _LegalDocuments()
        {
            return PartialView("LegalDocuments");
        }

        // Juntas generales de socios
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult GeneralMemberMeetings()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _GeneralMemberMeetings()
        {
            return PartialView("GeneralMemberMeetings");
        }

        // Soporte
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Support()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Support()
        {
            return PartialView("Support");
        }

        // Entry Funds
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult EntryFunds()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _EntryFunds()
        {
            return PartialView("EntryFunds");
        }

        // Basic Information
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult BasicInformation()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _BasicInformation()
        {
            return PartialView("BasicInformation");
        }

        // Use Conditions
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult UseConditions()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _UseConditions()
        {
            return PartialView("UseConditions");
        }

        // Privacy Policy
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _PrivacyPolicy()
        {
            return PartialView("PrivacyPolicy");
        }

        // Invesafe Reports
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult InvesafeReports()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _InvesafeReports()
        {
            return PartialView("InvesafeReports");
        }

        // Retire Funds
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult RetireFunds()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _RetireFunds()
        {
            return PartialView("RetireFunds");
        }

        // FAQ
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult FAQ()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _FAQ()
        {
            return PartialView("FAQ");
        }

        // About Us
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult AboutUs()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _AboutUs()
        {
            return PartialView("AboutUs");
        }

        // How Does It Work
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult HowWork()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _HowWork()
        {
            return PartialView("HowWork");
        }

        // Market Place
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult MarketPlace()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _MarketPlace()
        {
            return PartialView("MarketPlace");
        }

        // Notifications
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Notifications()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Notifications()
        {
            return PartialView("Notifications");
        }

        // Active Account
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult ActiveAccount()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _ActiveAccount()
        {
            return PartialView("ActiveAccount");
        }
    }
}