


using VideoGame.Inventory;

namespace VideoGame {


    /// <summary>
    /// Representa a un jugador concreto
    /// </summary>
    public partial class Player {


        /// <summary>
        /// Inventario del jugador
        /// </summary>
        public PlayerInventory Inventory { get; init; }

        public Player()
        {
            Inventory = new PlayerInventory(this);
        }
    }
}