using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGestionCancion: IServiceGestionCancion
    {
        public string FormatURL(string url)
        {
            string UrlFormat = "";
            try
            {
                if (url != null)
                {
                    if (url.Contains("youtu.be"))
                        UrlFormat = url.Replace("youtu.be", "youtube.com/embed");
                    else
                        return url;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return UrlFormat;
        }
    }
}
