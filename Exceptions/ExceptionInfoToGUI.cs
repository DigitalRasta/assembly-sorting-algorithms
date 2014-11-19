using System;

/*
 * Exception which provides user-readable informations about Exception in application
 * Author: Jakub'Digitalrasta'Bujny
 * Version: 0.0.0
 * Created: 22.10.2014
 * Changelog:
 */
namespace sortingProject.Exceptions
{
    public class ExceptionInfoToGUI : System.Exception
    {

        String message;
        public ExceptionInfoToGUI(String message)
        {
            this.message = message;
        }

        public String getMessage()
        {
            return message;
        }
    }
}
