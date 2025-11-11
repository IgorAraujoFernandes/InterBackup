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
        
        Dictionary<string, string> JsonMap =  new Dictionary<string, string>();
        JsonMap.Add("Game", JSON.Game);
        JsonMap.Add("RepoExists", JSON.RepoExists);
        JsonMap.Add("Branch", JSON.Branch);
        JsonMap.Add("GitFolder", JSON.GitFolder);

        if (JsonMap["Game"] == "Terraria")
        {
            Terraria();
        }

        if (JsonMap["Game"] == "Stardew")
        {
            string mundo = StardewValley();
            Console.WriteLine(mundo);
        }
        
    }


    #region FunctionsByGame

    public static void Terraria()
    {
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Console.WriteLine(path);
    }

    public static string StardewValley()
    {
        
        path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        path += "\\StardewValley\\Saves";

        string mundo = "none";
        
        string[] folders = Directory.GetDirectories(path);
        List<string> foldersList = folders.ToList();

        Console.WriteLine("Lista de pastas:");
        foreach (string folder in folders)
        {
            Console.WriteLine(Path.GetFileName(folder));
        }

        
            int counter = 0;
            do
            {
                string mensagem = (counter<1)? "Escolha o mundo a ser feito backup:" : "Mundo inexistente, insira novamente:";
                Console.WriteLine(mensagem);
                mundo = Console.ReadLine();
                counter++;
            } while(!foldersList.Contains(mundo));
        
            return mundo;

    }
    
    #endregion
    
    #region Records
    public record Controller(string Game, String RepoExists, String Branch, String GitFolder);
    
    #endregion
 
}
