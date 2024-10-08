using System;
using System.Collections.Generic;
using System.Text;


namespace TemplateGenerator.ARM
{
    public class TemplateRegionCommon : TemplateRegion
    {
        private string schema = "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#";
        private string contentVersion = "";
        public string Schema { get => schema; }
        public string ContentVersion { get => contentVersion; set => contentVersion = value; }

        public TemplateRegionCommon(string ContentVersion = "1.0")
        {
            this.ContentVersion = ContentVersion;
            this.RegionType = "Common";
        }

        public override string Render()
        {
            string sOutput = "";

            sOutput += F.CB + Environment.NewLine;
            sOutput += F.Quote + "$schema" + F.Quote + F.Colon + F.Quote + Schema + F.Quote + F.Comma + Environment.NewLine;

            sOutput += F.Quote + "contentVersion" + F.Quote + F.Colon + F.Quote + ContentVersion + F.Quote + F.Comma + Environment.NewLine;

            return (sOutput);
        }
    }
}
