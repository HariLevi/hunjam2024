﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Tiles
{
    public class Tile : MonoBehaviour
    {
        public Vector Position { get; set; }

        /**********
         * GETTERS
         **********/

        /*
         * Check whether this tile touches with the other tile
         */
        public bool IsNextTo(Tile other)
        {
            return Position.DistanceFrom(other.Position).Length == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return Position.Z == other.Position.Z;
        }

        public bool IsOnNeighboringLevel(Tile other)
        {
            return Math.Abs(Position.Z - other.Position.Z) == 1;
        }

        /*
         * Returns how much you would have to step from one tile to another if you could not move diagonally
         */
        public Vector DistanceFrom(Tile other)
        {
            return Position.DistanceFrom(other.Position);
        }

        /*
         * Checks whether the tile is able to accept a player from another tile.
         * Acceptance means the player could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public virtual bool AcceptsCharacterFrom(Tile other)
        {
            // TODO: remove
            return false;
        }

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public virtual bool AcceptsCharacter(Character character)
        {
            return false;
        }

        public virtual bool CanBeMovedOn()
        {
            var tile = MapManager.Instance.GetTileAt(Position + new Vector(0, 0, 1));
            return tile == null || tile.AcceptsCharacterFrom(this);
        }

        /*
         * Returns existing tiles that have a distance of 1 without counting the Z dimension
         * The returned tiles' Z coordinate is the same as the `z` given here as parameter 
         */
        public List<Tile> GetNeighboursInLevel(int z)
        {
            List<Tile> result = new();

            var zOffset = z - Position.Z;

            var neighbour = MapManager.Instance.GetTileAt(Position + new Vector(1, 0, zOffset));
            if (neighbour != null) result.Add(neighbour);
            neighbour = MapManager.Instance.GetTileAt(Position + new Vector(0, 1, zOffset));
            if (neighbour != null) result.Add(neighbour);
            neighbour = MapManager.Instance.GetTileAt(Position + new Vector(-1, 0, zOffset));
            if (neighbour != null) result.Add(neighbour);
            neighbour = MapManager.Instance.GetTileAt(Position + new Vector(0, -1, zOffset));
            if (neighbour != null) result.Add(neighbour);

            return result;
        }

        /// <summary>
        /// Returns the list of valid neighbor tiles that can be stepped upon
        /// </summary>
        /// <returns>
        ///     List of valid tiles
        /// </returns>
        public List<Tile> GetValidNeighbors()
        {
            List<Tile> tiles = new();
            // Same Level
            var tile = MapManager.Instance.GetTileAt(Position + new Vector(1, 0, 0));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            Debug.Log("tile added? " + tiles.Count);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(-1, 0, 0));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, 1, 0));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, -1, 0));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            // One Above
            tile = MapManager.Instance.GetTileAt(Position + new Vector(1, 0, 1));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(-1, 0, 1));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, 1, 1));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, -1, 1));
            if (tile != null && tile.CanBeMovedOn()) tiles.Add(tile);
            tiles.Add(this);
            return tiles;
        }

        /**********
         * ACTIONS
         **********/

        public bool AcceptTile(Tile tile)
        {
            return false;
        }

        public bool AcceptCharacter(Character character)
        {
            return false;
        }

        /*
         * Tries to change the position of the tile to `destination`
         * Returns true on success
         */
        public virtual bool MoveTo(Vector destinationPosition)
        {
            return false;
        }


        // Unity STUFF
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}