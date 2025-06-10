
namespace VideoGame.Inventory {

    public partial class PlayerInventory {

        /// <summary>
        /// Jugador que contiene al inventario
        /// </summary>
        public Player parent { get; }

        /// <summary>
        /// Capacidad máxima del inventario
        /// </summary>
        public int Size { get; }

        // TODO: Implementar cómo se almacenan los items 

        private IItem?[] items;

        /// <summary>
        /// Crea el inventario asociado a un jugador
        /// </summary>
        /// <param name="player">Dueño del inventario</param>
        /// <param name="size">Capacidad máxima del inventario</param>
        public PlayerInventory(Player player, int size = 10)
        {
            this.parent = player;
            this.Size = size;
            this.items = new IItem?[size];
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Store(IItem item)
        //Poder almacenar items y retorna false si no puede guardarlo
        {
            // Si ya está almacenado, devolvemos true (sin duplicarlo)
            if (Contains(item))
            {
                return true;
            }

            // Buscamos espacio libre para almacenarlo
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// TODO: Implementar
        /// Similar a Store pero guarda el item en una posición concreta
        /// </summary>
        public bool StoreAt(IItem item, int index) {

            if (index < 0 || index >= Size) return false;

            // Si el item ya está en esa posición, retornamos true 
            if (items[index] == item) return true;
            // Si el item está en otra posición, no permitimos duplicados
            if (Contains(item)) return false;
            // Si la posición está ocupada por otro item, no se puede insertar
            if (items[index] != null) return false;
            // Guardamos el item en la posición indicada
            items[index] = item;
            return true;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public IItem? GetItemAt(int index)
        //Poder ver el item almacenado en una posición
        {
            if (index < 0 || index >= Size)
            {
                return null;
            }
             return items[index];
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Drop(IItem item) {
            // Busca el item en todo el inventario y si lo encuentra lo elimina
            for (int i = 0; i < Size; i++) {
            if (items[i] == item) {
                // Reutilizamos el Drop por índice para no duplicar código
                return Drop(i);
            }
            }

            return false;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Drop(int index)
        {
            //Elimina el item de la posición dada.
            if (index < 0 || index >= Size) return false;
            if (items[index] == null) return false;

            items[index] = null;
            return true;
        }
        

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public ICollection<IItem> ListItems() {
            //Visualizar los items que tiene en orden de inserción.
            List<IItem> list = new List<IItem>();

            foreach (var item in items) {
                if (item != null) {
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// TODO: Implementar
        /// Devuelve true si el item especificado está almacenado en el inventario.
        /// </summary>
        public bool Contains(IItem item) {
            for (int i = 0; i < Size; i++)
            {
                if (items[i] == item)
                { 
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// TODO: Implementar
        /// Encuentra el primer Item que cumpla la condición especificada en el delegado.
        /// </summary>
        public IItem? Find(Func<IItem, bool> condition) {
            foreach (var item in items) {
            if (item != null && condition(item)) {
                return item;
                }
            }
            return null;
        }

        /// <summary>
        /// TODO: Implementar
        /// Filtra por subtipo de item que busca.
        /// </summary>
        public T? Find<T>(Func<T, bool> condition) where T:class,IItem {
            foreach (var item in items) {
            if (item is T typedItem && condition(typedItem)) {
            return typedItem;
            }
        }
        return null;
        }

        /// <summary>
        /// TODO: Implementar
        /// Borra el contenido del inventario.
        /// </summary>
        public void Clear() {
            for (int i = 0; i < Size; i++) {
            items[i] = null;
            }
        }
        
        /// <summary>
        /// TODO: Implementar
        /// Transfiere un item desde el inventario actual a otro inventario objetivo. 
        /// </summary>
        public bool Transfer(IItem item, PlayerInventory target){
            // Si el item no está en este inventario, no se puede transferir
            if (!Contains(item)) return false;
            // Intentamos almacenar el item en el inventario objetivo
            if (!target.Store(item)) return false;
            // Si se almacenó correctamente, lo eliminamos de este inventario
            return Drop(item);
        }
    }
}