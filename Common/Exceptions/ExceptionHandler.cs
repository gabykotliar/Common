using System;

namespace Common.Exceptions
{
    public class ExceptionHandler
    {
        public static void LogIfException(Action riskyAction)
        {
            try
            {
                riskyAction();
            }
            catch (Exception ex)
            {
                AppServices.Log.Exception(ex);

                throw;
            }
        }

        public static void HideException(Action riskyAction)
        {
            try
            {
                riskyAction();
            }
            catch (Exception ex) {
                AppServices.Log.Exception(ex);
            }
        }

        public static void IfException(Action riskyAction, Action<Exception> handleException)
        {
            try
            {
                riskyAction();
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }
    }
}
