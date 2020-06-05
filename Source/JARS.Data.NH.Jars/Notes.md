How to set up a context.
 Create a Project and name it anything you like, but make sure it has a .DataContext. in the assembly name.
 Add references to:
 
 -Nuget packages (if using nhibernate):
	FluentNhibernate
-dlls
 System.ComponentModel.Composition (for working with MEF)
 System.Configuration
 JARS.Entities.Maps (or the name of the dll where your Nhibernate mappings are)
 JARS.Data.DataContext.NH (the NH indicates that you are using the Nhibernate base)


 Inherit your class from DataContextNhBase eg.
 public class DataContextNhJars : DataContextNhBase, IDataContextNhJars {}

 Create an Interface that inherits from  IDataContextNhBase eg.
 public interface IDataContextNhJars : IDataContextNhBase { }
 
 You could use the GenericRepository<T,ContextInterface> for a lot of data access but if you require a special repository create you can create another project (or folder).
 if creating a project, make sure the assembly builds with .Repositories. in the assembly name.
 Folder*
 Add more references
-dlls
System.Runtime.Serialisation
JARS.Data.NH.Interfaces;
Jars.Entities
Jars.External.Entities


