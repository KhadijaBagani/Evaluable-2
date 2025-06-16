using System.Diagnostics;
using VideoGame;
using VideoGame.Inventory;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        var inventario = new PlayerInventory(new Player());
        var item = new Sword();
        var item2 = new Armor();

        //Store
        Debug.Assert(inventario.Store(item), "El item no se guarda");
        inventario.Store(item);
        inventario.Store(new Armor());
        Debug.Assert(inventario.Contains(item), "No ha guardado el item");

        //Clear
        inventario.Clear();

        //Store Drop
        inventario.Store(item);
        inventario.Drop(6);
        inventario.Drop(0);
        inventario.Drop(item);

        //StoreAt

        Debug.Assert(inventario.StoreAt(item, 2), "El item no se guarda");
        inventario.StoreAt(item, 2);

        // Find
        var resultado = inventario.Find(item => item.Name.StartsWith("S"));
        Console.WriteLine(resultado?.Name ?? "No encontrado");

        //Find <T>
        var resultado2 = inventario.Find<Sword>(item => true);

        //Transfer
        var inventario2 = new PlayerInventory(new Player());
        inventario.Transfer(item, inventario2);
        inventario.Transfer(item, inventario2);


        //GetItemAT
        inventario.GetItemAt(3);

      
    }
}
