This Class project will is where we will implement the MEF methods.

Remember we are basing the repositories on their counterpart Interfaces from the JARS.Data.Core.Interfaces class (which only contains the interfaces)

We have to decorate every class with the appropriate attributes, for mef to be able to pick it up and match it with the interface counterpars.

In the below example _xxENTITYxx_ can be replaced with the name of the entity ie

* IxxENTITYxxRepository --becomes-- IYourEntityRepository
* xxENTITYxxRepository --becomes-- YourEntityRepository

and so on...

```c#
namespace JARS.Data.XXXXX.Repositories
{
    [Export(typeof(I_xxENTITYxx_Repository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class _xxENTITYxx_Repository : DataRepositoryBase<_xxENTITYxx_>, I_xxENTITYxx_Repository
    {
    
    }
}

```

The Repositories are added for completeness, even though most of the repositories only inherit from JARS.Core.Interfaces.Data.IDataRepository<T> with no additional methods added.
This allows us to break the repositories down into smaller bits in case we want to add specific functionality to a certain repository

```c#
[Export(typeof(I_x_Repository))]
[PartCreationPolicy(CreationPolicy.NonShared)] : DataRepositoryBase<x>, I_X_Repository

 ```
 There is a generic Repository that should cover most scenarios for managing data.

 The generic repository GenericEntityRepository<T> - which implements the IGenericEntityRepository<T> interface can be used for any CRUD methods.

 To Use the repository You have to instanciate the repository with the Entity type ie.
 ```c#
 IGenericEntityRepository<JarsJobBase> jobRepo = _RepoFactory.GetRepository<IGenericEntityRepository<JarsJobBase>>();
 ```

 the jobRepo will then be bound to manipulating JarsJobBase entities to the database.

