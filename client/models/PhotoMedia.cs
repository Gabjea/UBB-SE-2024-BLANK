using client.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace client.models
{
    class PhotoMedia : Media
    {
        public PhotoMedia(string filePath) :
            base(filePath, ".jpg")
        {

        }
    }
}
