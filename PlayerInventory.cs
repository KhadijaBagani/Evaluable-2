
namespace VideoGame.Inventory
{

    public partial class PlayerInventory
    {

        /// <summary>
        /// Jugador que contiene al inventario
        /// </summary>
        public Player parent { get; }

        /// <summary>
        /// Capacidad máxima del inventario
        /// </summary>
        public int Size { get; }

        // TODO: Implementar cómo se almacenan los items 

        private List<IItem?> items;

        /// <summary>
        /// Crea el inventario asociado a un jugador
        /// </summary>
        /// <param name="player">Dueño del inventario</param>
        /// <param name="size">Capacidad máxima del inventario</param>
        public PlayerInventory(Player player, int size = 10)
        {
            this.parent = player;
            this.Size = size;
            this.items = new List<IItem?>(size);
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Store(IItem item)
        //Poder almacenar items y retorna false si no puede guardarlo
        {
            if (Contains(item))
                return true;

            int index = items.IndexOf(null);
            if (index != -1)
            {
                items[index] = item;
                return true;
            }
            items.Add(item);
            return true;

        }

        /// <summary>
        /// TODO: Implementar
        /// Similar a Store pero guarda el item en una posición concreta
        /// </summary>
        public bool StoreAt(IItem item, int index)
        {

            if (index < 0 || index >= Size)
                return false;
            // * Esto es para ayudarme
            // Si el item ya está en esa posición, retornamos true
            if (index < items.Count && items[index] == item)
                return true;
            // Si el item está en otra posición, no permitimos duplicados
            if (Contains(item))
                return false;
            // Aseguramos que la lista tenga al menos 'Size' elementos (rellenamos con nulls si hace falta)
            while (items.Count <= index)
            {
                items.Add(null);
            }

            // Si la posición está ocupada por otro item, no se puede insertar
            if (items[index] != null)
                return false;
            // Guardamos el item en la posición indicada
            items[index] = item;
            return true;
        }

        /// <summary>
        /// TODO: Implementar 
        /// Poder ver el item almacenado en una posición
        /// </summary>
        public IItem? GetItemAt(int index)

        {
            if (index < 0 || index >= items.Count)
            {
                return null;
            }
            return items[index];
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Drop(IItem item)
        {
            int index = items.IndexOf(item);
            if (index == -1) return false;

            items[index] = null;
            return true;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Drop(int index)
        {
            if (index < 0 || index >= items.Count) return false;
            if (items[index] == null) return false;

            items[index] = null;
            return true;
        }


        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public ICollection<IItem?> ListItems()
        {

            return items.Where(item => item != null).ToList();
        }

        /// <summary>
        /// TODO: Implementar
        /// Devuelve true si el item especificado está almacenado en el inventario.
        /// </summary>
        public bool Contains(IItem item)
        {
            return items.Contains(item);
        }

        /// <summary>
        /// TODO: Implementar
        /// Encuentra el primer Item que cumpla la condición especificada en el delegado.
        /// </summary>
        public IItem? Find(Func<IItem, bool> condition)
        {
            return items.FirstOrDefault(item => item != null && condition(item));
        }

        /// <summary>
        /// TODO: Implementar
        /// Filtra por subtipo de item que busca.
        /// </summary>
        public T? Find<T>(Func<T, bool> condition) where T : class, IItem
        {
            return items.OfType<T>().FirstOrDefault(condition);
        }

        /// <summary>
        /// TODO: Implementar
        /// Borra el contenido del inventario.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i] = null;
            }
        }

        /// <summary>
        /// TODO: Implementar
        /// Transfiere un item desde el inventario actual a otro inventario objetivo. 
        /// </summary>
        public bool Transfer(IItem item, PlayerInventory target)
        {
            // * Esto es para ayudarme
            // Si el item no está en este inventario, no se puede transferir
            if (!Contains(item)) return false;
            // Intentamos almacenar el item en el inventario objetivo
            if (!target.Store(item)) return false;
            // Si se almacenó correctamente, lo eliminamos de este inventario
            return Drop(item);
        }
    }
}