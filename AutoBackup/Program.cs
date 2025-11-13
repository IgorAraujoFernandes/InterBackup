using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

public class Program()
{
    public static string path = "NULL";
    public static string mundo = "NULL";
    public static string CommitMessage = " ";
    public static DateTime date = new DateTime();
    public static void Main(string[] args)
    {
        
        string JsonToFile = File.ReadAllText("control.json");
        
        Controller JSON =  JsonSerializer.Deserialize<Controller>(JsonToFile);

        
        Dictionary<string, string> JsonMap =  new Dictionary<string, string>();
        JsonMap.Add("Game", (JSON.Game).ToUpper());
        JsonMap.Add("RepoExists", JSON.RepoExists);
        JsonMap.Add("Branch", JSON.Branch);
        JsonMap.Add("GitFolder", JSON.GitFolder);
        
        switch (JsonMap["Game"])
        {
            case "TERRARIA":
                mundo = Terraria();
                date = DateTime.Now;
                CommitMessage = $"Mundo: {mundo} salvo em: {date}";
                Console.WriteLine(CommitMessage);
                break;
            case "STARDEW":
                    mundo = StardewValley();
                    Console.WriteLine($"nome do mundo Stardew: {mundo}");
                    break;
            default:
                Console.WriteLine("Jogo inexistente");
                break;
        }
        
        
    }

    #region GitFunctions

    public static void GitFirstConfiguration()
    {
        Console.WriteLine("Cole a URL do repositorio git: ");
        string GitRepository = Console.ReadLine();
        
        
    }
    
    public static void GitCommit(string Message)
    {
        
        Process.Start("git", "init");
        Process.Start("git", "add .");
        Process.Start("git", $"commit -m '{Message}'");
        Process.Start("git", "push -U origin main");
        
    }
    

    #endregion
    
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
