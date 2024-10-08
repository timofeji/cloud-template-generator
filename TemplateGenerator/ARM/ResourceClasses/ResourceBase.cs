using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public abstract class ResourceBase
    {
        public virtual bool ValidateMe()
        {
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p == null)
                    return (false);
            }
            return (true);
        }
        public bool LoadDefaults(int ResourceID, object ResourceClass, string DBCnxnString, string LogPath)
        {
            AzureResourceProviderPropertyCollection r = new AzureResourceProviderPropertyCollection(ResourceID, DBCnxnString, LogPath);
            foreach (AzureResourceProviderProperty z in r.Values)
            {
                foreach (PropertyInfo p in ResourceClass.GetType().GetProperties())
                {
                    if (z.PropertyName.ToUpper() == p.Name.ToUpper())
                        p.SetValue(ResourceClass, z.DefaultValue);
                }
            }
            return (true);
        }
    }
}
