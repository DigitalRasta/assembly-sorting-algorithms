using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Exception which provides user-readable informations about Exception in application
 * Author: Jakub'Digitalrasta'Bujny
 * Version: 0.0.0
 * Created: 22.10.2014
 * Changelog:
 */
namespace sortingProject
{
    class ExceptionInfoToGUI : Exception
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
