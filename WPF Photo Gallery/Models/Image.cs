using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Photo_Gallery.Models
{
    internal class Image
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public int Mark { get; set; }
        public string Path { get; set; }
    }
}
