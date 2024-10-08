# Cloud Provider Template Generator
This is an example of how to set up values to pass into the generator.  

The generator uses a small set of reference files to render a final template.

            List<Loader.LoaderFile> oList = new List<Loader.LoaderFile>();

            // pull the correct snippet files by type
            Loader l = new Loader(ConfigurationManager.AppSettings["FilePath"]);
            oList = l.FetchTemplateFilesContent("typeselected");

The UI would pass values to change a small number of elements in those reference files.
The first step is creating the appropriate Class for the exact operation intended.
In this case, adding a Kube Cluster (AKS) to an existing VNet and connecting it to an existing ACR.

This db call pulls all of the configuration properties needed by Azure to properly generate an AKS.

            AKSWithACR oAKSACR = new AKSWithACR(this.DBCnxnString, this.LogPath);
            // a list of all available types can be fetched with:
            //  Loader l = new Loader(ConfigurationManager.AppSettings["FilePath"]); // file path to the template reference files
            //  myList.DataSource = l.FetchResourceTypes();
       
            // this is where we fill all props of the oAKSACR object from the UI etc
            oAKSACR.AcrName = "DropACR";
            oAKSACR.AcrResourceGroup = "RGDrop";
            oAKSACR.EnableRBAC = true;
            // etc

            // EVERY property in the object should have a value so ValidateMe checks for any nulls
            if (oAKSACR.ValidateMe())
            {
                // The reference files have placeholders marked with ***
                // so here we set up replacement properties
                foreach (PropertyInfo p in oAKSACR.GetType().GetProperties())
                {
                    this.ReplacementValues.Add("***" + p.Name + " ***");
                }
            }
            else
            {
                Response.Write("Invalid AKSWithACR");
                Response.End();
            }


            // now we replace the placeholders with values from the UI
            foreach (Loader.LoaderFile lf in oList)
            {
                string sRpl = FileMaker.CreateAKSWithACRFile(oAKSACR, lf.JSON);
                if (sRpl.Length > 0)
                {
                    lf.JSON = sRpl;
                }
            }

            // save the modified files to a secondary location
            // each resource could have up to four separate files
            // required to make a single template
            
            string sTempFilePath = "c:/logs/";// some cloud path
            foreach (Loader.LoaderFile lf in oList)
            {
                switch (lf.FileType)
                {
                    case "P":
                        File.WriteAllText(sTempFilePath + "AKSClusterParameters.txt", lf.JSON);
                        break;
                    case "R":
                        File.WriteAllText(sTempFilePath + "AKSClusterResource.txt", lf.JSON);
                        break;
                    case "V":
                        File.WriteAllText(sTempFilePath + "AKSClusterVariables.txt", lf.JSON);
                        break;
                    case "O":
                        File.WriteAllText(sTempFilePath + "AKSClusterOutput.txt", lf.JSON);
                        break;

                }

            }

Here is an example of how to call the template generator.
Extremely simple, create some config if needed for whatever resource you are creating
and then call the template type by ID (which in this case comes from an Enum but 
the point is just know the ID for the template you want to generate)

              // set some config properties needed for NSGs
            ARMConfig Config = new ARMConfig();
            Config.SGRSSHOK = true;
            Config.SGRHttpsOK = true;

            // call the process specifying that new path you used above for the new template files
            ARMTemplateMaster m = new ARMTemplateMaster(Config, this.DBCnxnString, sTempFilePath, this.LogPath);
            int iTemplateType = (int)Global.Templates.AKSWithACR;
            
            // add the chosen template by type to the master
            ARMTemplate c = new ARMTemplate(iTemplateType, this.DBCnxnString, this.LogPath);
            m.Templates.Add(c);

            // call the RenderFinal process to get a usable ARM Template to run
            string sOut = m.RenderFinalTemplate("someNamespace");

            // write that final file
            File.WriteAllText("c:/logs/arm" + iTemplateType + ".txt", sOut);
