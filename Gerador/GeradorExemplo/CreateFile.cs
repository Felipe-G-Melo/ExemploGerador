namespace GeradorExemplo;
public class CreateFile
{
    public static void CreateEntity(DirectoryInfo  path, List<Property> properties, string entityName, List<Property> dtos)
    {
        string className = entityName + "Entity";
        string classPath = Path.Combine(path.FullName, className + ".cs");
        using (StreamWriter sw = File.CreateText(classPath))
        {
            sw.WriteLine("namespace ApiExemplo.Entities;");
            sw.WriteLine();
            sw.WriteLine($"public class {className}");
            sw.WriteLine("{");
            sw.WriteLine("    public Guid Id { get; private set; }");
            foreach (var property in properties)
            {
                sw.WriteLine($"    public {property.Type} {property.Name} {{ get; private set; }}");
            }
            sw.WriteLine();
            sw.Write($"    public {className}(");
            for (int i = 0; i < dtos.Count; i++)
            {
                sw.Write($"{dtos[i].Type} {dtos[i].Name.ToLower()}");
                if (i < dtos.Count - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.WriteLine(")");
            sw.WriteLine("    {");
            sw.WriteLine("        Id = Guid.NewGuid();");
            foreach (var dto in dtos)
            {
                sw.WriteLine($"        {dto.Name} = {dto.Name.ToLower()};");
            }
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.Write("    public void Update(");
            for (int i = 0; i < dtos.Count; i++)
            {
                sw.Write($"{dtos[i].Type} {dtos[i].Name.ToLower()}");
                if (i < dtos.Count - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.WriteLine(")");
            sw.WriteLine("    {");
            foreach (var dto in dtos)
            {
                sw.WriteLine($"        {dto.Name} = {dto.Name.ToLower()};");
            }
            sw.WriteLine("    }");
            sw.WriteLine("}");
        }
    }

    public static void CreateInterface(DirectoryInfo path, string entityName)
    {
        string interfaceName = "I" + entityName + "Repository";
        string interfacePath = Path.Combine(path.FullName, interfaceName + ".cs");
        using (StreamWriter sw = File.CreateText(interfacePath))
        {
            sw.WriteLine("using ApiExemplo.Dto;");
            sw.WriteLine("using ApiExemplo.Entities;");
            sw.WriteLine();
            sw.WriteLine("namespace ApiExemplo.Interfaces;");
            sw.WriteLine();
            sw.WriteLine($"public interface {interfaceName}");
            sw.WriteLine("{");
            sw.WriteLine($"    Task<{entityName}Dto> Add({entityName}Dto {entityName.ToLower()});");
            sw.WriteLine($"    Task<{entityName}Dto> Update({entityName}Dto {entityName.ToLower()}, Guid id);");
            sw.WriteLine($"    Task<bool> Delete(Guid id);");
            sw.WriteLine($"    Task<List<{entityName}Entity>> GetAll();");
            sw.WriteLine("}");  
        }
    }

    public static void CreateDto(DirectoryInfo path, List<Property> properties, string entityName)
    {
        string dtoName = entityName + "Dto";
        string dtoPath = Path.Combine(path.FullName, dtoName + ".cs");
        using (StreamWriter sw = File.CreateText(dtoPath))
        {
            sw.WriteLine("namespace ApiExemplo.Dto;");
            sw.WriteLine();
            sw.WriteLine($"public class {dtoName}");
            sw.WriteLine("{");
            foreach (var property in properties)
            {
                sw.WriteLine($"    public {property.Type} {property.Name} {{ get; set; }}");
            }
            sw.WriteLine("}");
        }
    }

    public static void CreateRepository(DirectoryInfo path, List<Property> properties, string entityName, List<Property> dtos)
    {
        string repositoryName = entityName + "Repository";
        string repositoryPath = Path.Combine(path.FullName, repositoryName + ".cs");
        using (StreamWriter sw = File.CreateText(repositoryPath))
        {
            sw.WriteLine("using ApiExemplo.Dto;");
            sw.WriteLine("using ApiExemplo.Entities;");
            sw.WriteLine("using ApiExemplo.Interfaces;");
            sw.WriteLine();
            sw.WriteLine("namespace ApiExemplo.Repositories;");
            sw.WriteLine();
            sw.WriteLine($"public class {repositoryName} : I{entityName}Repository");
            sw.WriteLine("{");
            sw.WriteLine($"    public List<{entityName}Entity> {entityName}s {{ get; private set; }} = new List<{entityName}Entity>();");
            sw.WriteLine();
            sw.WriteLine($"    public Task<{entityName}Dto> Add({entityName}Dto {entityName.ToLower()})");
            sw.WriteLine("    {");
            sw.Write($"        var entity = new {entityName}Entity(");
            for (int i = 0; i < dtos.Count; i++)
            {
                sw.Write($"{entityName.ToLower()}.{dtos[i].Name}");
                if (i < dtos.Count - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.WriteLine($");");
            sw.WriteLine($"        {entityName}s.Add(entity);");
            sw.WriteLine($"        return Task.FromResult({entityName.ToLower()});");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine($"    public Task<bool> Delete(Guid id)");
            sw.WriteLine("    {");
            sw.WriteLine($"        var entity = {entityName}s.FirstOrDefault(u => u.Id == id);");
            sw.WriteLine($"        {entityName}s.Remove(entity!);");
            sw.WriteLine($"        return Task.FromResult(true);");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine($"    public Task<{entityName}Dto> Update({entityName}Dto {entityName.ToLower()}, Guid id)");
            sw.WriteLine("    {");
            sw.WriteLine($"        var entity = {entityName}s.FirstOrDefault(u => u.Id == id);");
            sw.Write($"        entity.Update(");
            for (int i = 0; i < dtos.Count; i++)
            {
                sw.Write($"{entityName.ToLower()}.{dtos[i].Name}");
                if (i < dtos.Count - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.WriteLine(");");
            sw.WriteLine($"        return Task.FromResult({entityName.ToLower()});");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine($"    public Task<List<{entityName}Entity>> GetAll()");
            sw.WriteLine("    {");
            sw.WriteLine($"        return Task.FromResult({entityName}s);");
            sw.WriteLine("    }");   
            sw.WriteLine("}");   
        }
    }

    public static void CreateController(DirectoryInfo path, string entityName)
    {
        string serviceName = entityName + "Controller";
        string servicePath = Path.Combine(path.FullName, serviceName + ".cs");
        using (StreamWriter sw = File.CreateText(servicePath))
        {
            sw.WriteLine("using ApiExemplo.Dto;");
            sw.WriteLine("using ApiExemplo.Interfaces;");
            sw.WriteLine("using Microsoft.AspNetCore.Mvc;");
            sw.WriteLine();
            sw.WriteLine("namespace ApiExemplo.Controllers;");
            sw.WriteLine("[Route(\"api/[controller]\")]");
            sw.WriteLine("[ApiController]");
            sw.WriteLine($"public class {serviceName} : ControllerBase");
            sw.WriteLine("{");
            sw.WriteLine($"    private readonly I{entityName}Repository _{entityName.ToLower()}Repository;");
            sw.WriteLine();
            sw.WriteLine($"    public {serviceName}(I{entityName}Repository {entityName.ToLower()}Repository)");
            sw.WriteLine("    {");
            sw.WriteLine($"        _{entityName.ToLower()}Repository = {entityName.ToLower()}Repository;");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine("    [HttpPost]");
            sw.WriteLine($"    public async Task<IActionResult> Add{entityName}({entityName}Dto {entityName.ToLower()})");
            sw.WriteLine("    {");
            sw.WriteLine($"        var {entityName.ToLower()}Added = await _{entityName.ToLower()}Repository.Add({entityName.ToLower()});");
            sw.WriteLine($"        return Ok({entityName.ToLower()}Added);");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine("    [HttpPut(\"{id}\")]");
            sw.WriteLine($"    public async Task<IActionResult> Update{entityName}([FromRoute] Guid id, {entityName}Dto {entityName.ToLower()})");
            sw.WriteLine("    {");
            sw.WriteLine($"        var {entityName.ToLower()}Updated = await _{entityName.ToLower()}Repository.Update({entityName.ToLower()}, id);");
            sw.WriteLine($"        return Ok({entityName.ToLower()}Updated);");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine("    [HttpDelete(\"{id}\")]");
            sw.WriteLine($"    public async Task<IActionResult> Delete{entityName}(Guid id)");
            sw.WriteLine("    {");
            sw.WriteLine($"        var {entityName.ToLower()}Deleted = await _{entityName.ToLower()}Repository.Delete(id);");
            sw.WriteLine($"        return Ok({entityName.ToLower()}Deleted);");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine("    [HttpGet]");
            sw.WriteLine($"    public async Task<IActionResult> GetAll{entityName}()");
            sw.WriteLine("    {");
            sw.WriteLine($"        var {entityName.ToLower()}s = await _{entityName.ToLower()}Repository.GetAll();");
            sw.WriteLine($"        return Ok({entityName.ToLower()}s);");
            sw.WriteLine("    }");
            sw.WriteLine("}");
        }
    }
}