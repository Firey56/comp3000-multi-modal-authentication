using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace FirstGUIAttempt
{
    public static class Common
    {
        public static string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";

        public static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // save the image to the memory stream in the desired format (e.g., jpeg)
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                // return the byte array
                return ms.ToArray();
            }
        }
    }

}
