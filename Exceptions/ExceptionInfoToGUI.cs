using System;

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
