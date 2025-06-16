
namespace VideoGame.Inventory
{
    /// <summary>
    /// Gestionar los items que se encuentran en el escenario.
    /// </summary>
    public class ChestInventory : Inventory
    {
        public ChestInventory(int size = 10) : base(size) {}

        public override bool Store(IItem item)
        {
            return false; 
        }
        public override bool StoreAt(IItem item, int index)
        {
            return false; 
        }
    }
    /// <summary>
        /// Gestionar los items que llevan los personajes no jugables (NPC)
        /// </summary>
        public class NPCInventory : Inventory
        {public NPCInventory(int size = 10) : base(size) {}

        public override bool Drop(IItem item)
        {
            return false; 
        }
        public override bool Drop(int index)
        {
            return false; 
        }
    }
    /// <summary>
    /// Gestionar los items que se venden en una tienda.
    /// </summary>
        public interface ISellable
        {
            decimal? Price { get; }
        }
    public class ShopInventory : Inventory
    {
        public override bool Store(IItem item)
        {
            return item is ISellable sellable && sellable.Price != null;
        }
        public override bool StoreAt(IItem item, int index)
        {
            return item is ISellable sellable && sellable.Price != null && base.StoreAt(item, index);
        }
    }
}
    
