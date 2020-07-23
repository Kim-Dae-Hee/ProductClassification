using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClassification.Data
{
    public static class ExceptionHelper
    {
        public static string GetMessageRecursively(this Exception ex)
        {
            //string message = "";
            StringBuilder message = new StringBuilder();

            while (ex != null)
            {
                //message.Append(ex.Message);
                //message.Append(Environment.NewLine); //"\r\n"
                message.AppendLine(ex.Message);

                ex = ex.InnerException;
            }

            return message.ToString();
        }
    }
}
