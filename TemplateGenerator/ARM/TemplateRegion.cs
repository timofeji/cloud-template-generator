using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateGenerator.ARM
{
    public abstract class TemplateRegion
    {
        private string name;
        private string json;
        private string type;
        public string Name { get => name; set => name = value; }
        public string JSON { get => json; set => json = value; }
        public string RegionType { get => type; set => type = value; }

        public abstract string Render();     
 
    }
}
