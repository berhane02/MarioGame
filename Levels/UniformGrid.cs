using System;
using System.Collections.Generic;
using MarioGame.Entities;
using Microsoft.Xna.Framework;

namespace MarioGame.Levels
{
    public class UniformGrid
    {
        private readonly Cell[,] Grid;//Grid of X,Y axis
        private readonly int MaxX;
        private readonly int MaxY;
        private readonly int tileSize;

        public UniformGrid(int screenWidth, int screenHeight)
        {
            tileSize = 32;
            MaxX = screenWidth / tileSize;
            MaxY = screenHeight / tileSize;
            Grid = new Cell[MaxX, MaxY];
            Console.WriteLine(MaxX);
        }

        public Vector2 Position(int x, int y)//given a row and column we return the vector2 position
        {
            x = Math.Min(x, MaxX-1);
            y = Math.Min(y, MaxY-1);

            return new Vector2(x*tileSize, y*tileSize);
        }
        public void AddtoGrid(Entity entity)//add entity to all grid cells
        {
            List<Cell> cells = FutureGridCells(entity.BoundBox);
            foreach (Cell cell in cells)
            {
                cell.AddEntity(entity);
            }

        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Cell> FutureGridCells(Rectangle entity)
        {
            int Xmin = Math.Max(entity.X / tileSize, 0);//cap at lowest X
            int Ymin = Math.Max(entity.Y / tileSize, 0);//cap at lowest Y
            int Xmax = Math.Min(entity.Right / tileSize, MaxX - 1);//capped to max X
            int Ymax = Math.Min(entity.Bottom / tileSize, MaxY - 1);//capped to max Y
            Console.WriteLine(Xmin);
            Console.WriteLine(Ymin);
            Console.WriteLine(Xmax);
            Console.WriteLine(Ymax);

            List<Cell> future = new List<Cell>();//list of 
            for (int x = Xmin; x <= Xmax; x++)
            {
                for (int y = Ymin; y <= Ymax; y++)
                {
                    if (Grid[x, y] == null)
                    {
                        Grid[x, y] = new Cell();
                    }
                    future.Add(Grid[x,y]);
                }
            }
            return future;
        }

    }

    public class Cell
    {
        private List<Entity> InCell;

        public Cell()
        {
            InCell = new List<Entity>();
        }

        public void AddEntity(Entity tile)
        {
            InCell.Add(tile);
        }

    }

}
