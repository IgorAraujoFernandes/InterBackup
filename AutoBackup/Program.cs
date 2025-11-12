using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

public class Program()
{
    public static string path = "NULL";
    public static void Main(string[] args)
    {
        
        string JsonToFile = File.ReadAllText("control.json");
        
        Controller JSON =  JsonSerializer.Deserialize<Controller>(JsonToFile);

        string mundo = "NULL";
        
        Dictionary<string, string> JsonMap =  new Dictionary<string, string>();
        JsonMap.Add("Game", JSON.Game);
        JsonMap.Add("RepoExists", JSON.RepoExists);
        JsonMap.Add("Branch", JSON.Branch);
        JsonMap.Add("GitFolder", JSON.GitFolder);

        if (JsonMap["Game"] == "Terraria")
        {
            mundo = Terraria();
            Console.WriteLine($"nome do mundo Terraria: {mundo}");
        }

        if (JsonMap["Game"] == "Stardew")
        {
            mundo = StardewValley();
            Console.WriteLine($"nome do mundo Stardew: {mundo}");
        }
        
    }

    #region AuxiliaryFunctions

    public static string CheckAndReturnDirectory(string path, string game)
    {
        
        string mundo;
        
        string[] folders = Directory.GetDirectories(path);
        List<string>? files =  new List<string>();
        
        if (game == "TERRARIA")
        {
            Console.WriteLine("Lista de mundos: ");
            folders = Directory.GetFiles(path);
            foreach (string folder in folders)
            {
                files.Add(Path.GetFileName(folder.ToUpper()));
                Console.WriteLine(Path.GetFileName(folder));
            }
            
        }
        else
        {
            
        Console.WriteLine("Lista de pastas:");
        foreach (string folder in folders)
        {
            files.Add(Path.GetFileName(folder.ToUpper()));
            Console.WriteLine(Path.GetFileName(folder));
        }
        }

        
        int counter = 0;
        do
        {
            string mensagem = (counter<1)? "Escolha o mundo a ser feito backup(Com a extensao): " : "Mundo inexistente, insira novamente:";
            Console.WriteLine(mensagem);
            mundo = Console.ReadLine();
            counter++;
        } while(!files.Contains(mundo.ToUpper()));

        return mundo;
    }

    #endregion
    
    #region FunctionsByGame

    public static string Terraria()
    {
        
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\My Games\\Terraria\\Worlds"; 
        return CheckAndReturnDirectory(path, "TERRARIA");
        
        
    }

    public static string StardewValley()
    {
        
        path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ "\\StardewValley\\Saves";
        return CheckAndReturnDirectory(path, "STARDEW");

    }
    
    #endregion
    
    #region Records
    public record Controller(string Game, String RepoExists, String Branch, String GitFolder);
    
    #endregion
 
}
