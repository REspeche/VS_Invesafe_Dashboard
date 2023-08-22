namespace BusinessLayer.Enum
{
    public class Rules
    {
        public enum actionMessage
        {
            None = 0,
            RedirectToLogin = 1,
            RedirectToDashboard = 2,
            RedirectToSignup = 3,
            RedirectToForgot = 4,
            RedirectToSite = 5,
            RedirectToActiveSuccess = 6
        }

        public enum imageSize
        {
            Photo = 5,
            PhotoFull = 50
        }

        public enum containerName
        {
            Images = 1,
            Documents = 2,
            Projects = 3,
            InvesafeDocs = 4,
            InvesafeReports = 5
        }
    }
}
