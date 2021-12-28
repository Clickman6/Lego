using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoad {
    [Serializable]
    public class Playground {
        #region Singleton

        private static Playground _current;
        public static Playground Current => _current ??= new Playground();

        #endregion
        
        public List<Block> Items = new List<Block>();
        public Player Player;

        public Playground() {
            _current = this;
        }

        public static void Save(global::Player.Block[] blocks, global::Player.Player player) {
            Current.Player = new Player(player.transform);
            
            Current.Items.Clear();

            for (var i = 0; i < blocks.Length; i++) {
                Current.Items.Add(new Block(blocks[i].Id, blocks[i].transform, blocks[i].GetMaterial()));
            }
        }
    }
}
