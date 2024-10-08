
The Template Generator dll has a class for data management for each of the tables in the DB that are template necessities.  
The classes include Regions for the templates and properties of the Resources.  They contain self-saving and also self-rendering capabilities in ARM template format.

The Resource Classes namespace contains POCOs for carrying and validating required properties vis-à-vis the text-based template files 
in the Snippets folder that hold the specific ARM template snippets needed to construct a template piece-by-piece on demand.

The snippets are organized by Resource Type and hold replaceable values (typically in the xParameters.txt file).  
Inserting values into those placeholders results in the final template components that are then rendered into the final ARM Template used for actual provisioning.

Loader is the class called to assemble the proper snippets.

The deployment implementations will be a combination of building and running ARM Templates to create the infrastructure and then excecuting PowerShell scripts for
the mechanics involved in employing that infratructure (adding logins, creating databases, uploading containers, etc)

Just drop this dll next to any UI and provide a live db connection when implementing.
