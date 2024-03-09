namespace GeradorExemplo;
public class CreateFolders
{
    public string SourcePath { get; init; } = @"C:\dev\";
    public string EntityName { get; private set; }
    public DirectoryInfo EntityPath { get; private set; }
    public DirectoryInfo InterfacePath { get; private set; }
    public DirectoryInfo RepositoryPath { get; private set; }
    public DirectoryInfo DtoPath { get; private set; }
    public DirectoryInfo ControllerPath { get; private set; }

    public CreateFolders(string entityName)
    {
        SourcePath += entityName;
        EntityName = entityName;
        if (Directory.Exists(SourcePath))
        {
            Console.WriteLine("Esse diretorio já existe");
            throw new Exception("Esse diretorio já existe");
        }

        CreateInitialFolders();
    }

    private void CreateInitialFolders()
    {
        if (!Directory.Exists(@"C:\dev\"))
        {
            Directory.CreateDirectory(@"C:\dev\");
        }

        var sourcePath = Directory.CreateDirectory(SourcePath);
        InterfacePath = sourcePath.CreateSubdirectory("Interfaces");
        RepositoryPath = sourcePath.CreateSubdirectory("Repositories");
        DtoPath = sourcePath.CreateSubdirectory("Dtos");
        ControllerPath = sourcePath.CreateSubdirectory("Controllers");
        EntityPath = sourcePath.CreateSubdirectory("Entities");
    }
}
