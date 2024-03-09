
using GeradorExemplo;

try
{
    Console.Write("Digite o nome da entidade: ");
    var entityName = Console.ReadLine();
    var folders = new CreateFolders(entityName!);
    var properties = Set.Properties();
    var dto = Set.Dto(properties);

    CreateFile.CreateEntity(folders.EntityPath, properties, entityName, dto);
    CreateFile.CreateInterface(folders.InterfacePath, entityName);
    CreateFile.CreateDto(folders.DtoPath, dto, entityName);
    CreateFile.CreateRepository(folders.RepositoryPath, properties, entityName, dto);
    CreateFile.CreateController(folders.ControllerPath, entityName);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

