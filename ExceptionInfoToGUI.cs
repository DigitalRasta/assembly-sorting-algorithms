using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
