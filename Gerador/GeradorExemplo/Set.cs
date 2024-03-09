namespace GeradorExemplo;
public class Set
{
    public static List<Property> Properties()
    {
        var properties = new List<Property>();
        var flag = true;
        int count = 0;
        while (flag)
        {
            count++;
            Console.Write($"Digite o nome da propriedade {count}: ");
            var name = Console.ReadLine();
            Console.Write($"Digite o tipo da propriedade {count}: ");
            var type = Console.ReadLine();
            properties.Add(new Property(name, type));
            Console.Write("Deseja adicionar mais uma propriedade? (s/n) ");
            var answer = Console.ReadLine();
            if (answer == "n")
            {
                flag = false;
            }
        }

        return properties;
    }

    public static List<Property> Dto(List<Property> properties)
    {
        var dto = new List<Property>();
        Console.WriteLine("\n\n");
        foreach (var property in properties)
        {
            Console.Write($"A propriedade {property.Name} estará no DTO? (s/n) ");
            var answer = Console.ReadLine();
            if (answer == "s")
            {
                dto.Add(property);
            }
        }
        return dto;
    }
}
