﻿using Logic.Tiles;

namespace Logic
{
    public class Character
    {
        private MapManager _mapManager;
        private CloneManager _cloneManager;
        private Tile _tile;

        /***********
         * GETTERS
         ***********/
        private Vector Position => _tile.Position + new Vector(0, 0, 1);

        public bool ShouldBeDead => _cloneManager.Get(Position) != null;

        /**********
         * ACTIONS
         **********/

        /*
         * Character will try to move ONTO the tile at `destination`
         * The characters moves if it can and returns `true`, otherwise `false`.
         */
        public bool MoveOnto(Tile destination)
        {
            if (destination.IsOnSameLevel(_tile) && !_tile.IsNextTo(destination))
                return false;

            if (destination.IsOnNeighboringLevel(_tile) && destination.DistanceFrom(_tile).Length != 2)
                return false;

            var tileOnNextTile =
                _mapManager.GetTileAt(destination.Position + new Vector(0, 0, 1));
            if (tileOnNextTile == null || !tileOnNextTile.AcceptsPlayerFrom(_tile))
            {
                return false;
            }

            _tile = destination;

            return true;
        }

        /*
         * Character will try to push the given `movableTile` ONTO the tile at `destination`
         * The `movableTile` is pushed when the push is valid and returns `true`, otherwise `false`.
         * THE CHARACTER DOES NOT MOVE !!!
         */
        public bool PushOnto(MovableTile movableTile, Tile destination)
        {
            var destinationPosition = destination.Position + new Vector(0, 0, 1);
            var positionToDestinationDistance = Position.DistanceFrom(destinationPosition);
            if (!(
                    Position.DistanceFrom(movableTile.Position).Length == 1 &&
                    positionToDestinationDistance.Length == 2 &&
                    positionToDestinationDistance.IsPureDirectional()
                ))
            {
                return false;
            }

            return movableTile.MoveTo(destinationPosition);
        }
    }
}