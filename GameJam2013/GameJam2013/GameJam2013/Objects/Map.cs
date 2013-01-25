using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DigestionDuel.Objects
{
    class Map
    {
        int WIDTH = 20;
        int HEIGHT = 14;

        static Dictionary<Point, int[]> tileMapper = null;

        int xOff;
        int yOff;

        public Texture2D wall;
        public Texture2D rockWall;
        public Texture2D player;

        Collideable[][] grid;

        public static void SetTileMapper()
        {
            tileMapper = new Dictionary<Point, int[]>();
            //Error
            tileMapper.Add(new Point(3, 1),
                new int[] { -1 });
            //TopLeft
            tileMapper.Add(new Point(0, 0),
                new int[] { 56, 57, 60, 61, 120, 121, 124, 125 });
            //TopMid
            tileMapper.Add(new Point(1, 0),
                new int[] { 248, 249, 252, 253 });
            //TopRight
            tileMapper.Add(new Point(2, 0),
                new int[] { 224, 225, 228, 229, 240, 241, 244, 245 });
            //MidLeft
            tileMapper.Add(new Point(0, 1),
                new int[] { 62, 63, 126, 127 });
            //Center
            tileMapper.Add(new Point(1, 1),
                new int[] { 255 });
            //MidRight
            tileMapper.Add(new Point(2, 1),
                new int[] { 227, 231, 243, 247 });
            //BotLeft
            tileMapper.Add(new Point(0, 2),
                new int[] { 14, 15, 30, 31, 78, 79, 94, 95 });
            //BotMid
            tileMapper.Add(new Point(1, 2),
                new int[] { 143, 159, 207, 223 });
            //BotRight
            tileMapper.Add(new Point(2, 2),
                new int[] { 131, 135, 147, 151, 195, 199, 211, 215 });
            //Horizontal
            tileMapper.Add(new Point(3, 0),
                new int[] { 136, 137, 140, 141, 152, 153, 156, 157, 200, 201, 204, 216, 217, 220, 221 });
            //Vertical
            tileMapper.Add(new Point(4, 0),
                new int[] { 34, 35, 38, 39, 50, 51, 54, 55, 98, 99, 102, 103, 114, 115, 118, 119 });
            //Solo
            tileMapper.Add(new Point(5, 0),
                new int[] { 0, 1, 4, 5, 16, 17, 20, 21, 64, 65, 68, 69, 80, 81, 84, 85 });
            //CornerTopLeft
            tileMapper.Add(new Point(4, 1),
                new int[] { 40, 41, 44, 45, 104, 105, 108, 109 });
            //CornerTopRight
            tileMapper.Add(new Point(5, 1),
                new int[] { 160, 161, 164, 165, 176, 177, 180, 181 });
            //CornerBotLeft
            tileMapper.Add(new Point(4, 2),
                new int[] { 10, 11, 26, 27, 74, 75, 90, 91 });
            //CornerBotRight
            tileMapper.Add(new Point(5, 2),
                new int[] { 130, 134, 146, 150, 194, 198, 210, 214 });
            //PointLeft
            tileMapper.Add(new Point(0, 4),
                new int[] { 8, 9, 12, 13, 24, 25, 28, 29, 72, 73, 76, 77, 88, 89, 92, 93 });
            //PointRight
            tileMapper.Add(new Point(1, 3),
                new int[] { 128, 129, 132, 133, 144, 145, 148, 149, 192, 193, 196, 197, 208, 209, 212, 213});
            //PointTop
            tileMapper.Add(new Point(0, 3),
                new int[] { 32, 33, 36, 37, 48, 49, 52, 53, 96, 97, 100, 101, 112, 113, 116, 117});
            //PointBottom
            tileMapper.Add(new Point(1, 4),
                new int[] { 2, 3, 6, 7, 18, 19, 22, 23, 66, 67, 70, 71, 82, 83, 86, 87 });
            //FlatLeft
            tileMapper.Add(new Point(2, 4),
                new int[] { 42, 43, 106, 107 });
            //FlatRight
            tileMapper.Add(new Point(3, 3),
                new int[] { 162, 166, 178, 182 });
            //FlatTop
            tileMapper.Add(new Point(2, 3),
                new int[] { 168, 169, 172, 173 });
            //FlatBottom
            tileMapper.Add(new Point(3, 4),
                new int[] { 138, 154, 202, 218 });
            //FlatLeft1
            tileMapper.Add(new Point(2, 5),
                new int[] { 46, 47, 110, 111 });
            //FlatRight1
            tileMapper.Add(new Point(1, 5),
                new int[] { 226, 230, 242, 246 });
            //FlatTop1
            tileMapper.Add(new Point(0, 5),
                new int[] { 184, 185, 188, 189 });
            //FlatBottom1
            tileMapper.Add(new Point(3, 5),
                new int[] { 139, 155, 203, 219 });
            //FlatLeft2
            tileMapper.Add(new Point(6, 5),
                new int[] { 58, 59, 122, 123 });
            //FlatRight2
            tileMapper.Add(new Point(7, 4),
                new int[] { 163, 167, 179, 183 });
            //FlatTop2
            tileMapper.Add(new Point(6, 4),
                new int[] { 232, 233, 236, 237 });
            //FlatBottom2
            tileMapper.Add(new Point(7, 5),
                new int[] { 142, 158, 206, 222 });
            //CenterOnlyTopLeft
            tileMapper.Add(new Point(6, 2),
                new int[] { 171 });
            //CenterOnlyTopRight
            tileMapper.Add(new Point(7, 2),
                new int[] { 174 });
            //CenterOnlyBottomLeft
            tileMapper.Add(new Point(7, 3),
                new int[] { 234 });
            //CenterOnlyBottomRight
            tileMapper.Add(new Point(6, 3),
                new int[] { 186 });
            //CenterMissingTopLeft
            tileMapper.Add(new Point(5, 4),
                new int[] { 254 });
            //CenterMissingTopRight
            tileMapper.Add(new Point(4, 4),
                new int[] { 251 });
            //CenterMissingBottomLeft
            tileMapper.Add(new Point(4, 3),
                new int[] { 239 });
            //CenterMissingBottomRight
            tileMapper.Add(new Point(5, 3),
                new int[] { 191 });
            //CenterLeft
            tileMapper.Add(new Point(7, 1),
                new int[] { 235 });
            //CenterRight
            tileMapper.Add(new Point(6, 0),
                new int[] { 190 });
            //CenterTop
            tileMapper.Add(new Point(7, 0),
                new int[] { 175 });
            //CenterBottom
            tileMapper.Add(new Point(6, 1),
                new int[] { 250 });
            //DiagonalBotLeft
            tileMapper.Add(new Point(5, 5),
                new int[] { 238 });
            //DiagonalBotRight
            tileMapper.Add(new Point(4, 5),
                new int[] { 187 });
            //InverseCenter
            tileMapper.Add(new Point(3, 2),
                new int[] { 170 });
        }


        public Map(int xOff, int yOff)
        {
            this.xOff = xOff;
            this.yOff = yOff;
            grid = new Collideable[HEIGHT][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Collideable[WIDTH];
            }
        }

        public Map(int xOff, int yOff, int width, int height)
        {
            this.xOff = xOff;
            this.yOff = yOff;
            this.HEIGHT = height;
            this.WIDTH = width;
            grid = new Collideable[HEIGHT][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Collideable[WIDTH];
            }
        }

        public void SetMap(string[] data, CollideableManager cols)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    AssignMap(j,i, data[i][j]);
                    if (grid[i][j] is Player)
                    {
                        cols.Add(grid[i][j], true);
                        grid[i][j] = null;
                    }
                    else if (grid[i][j] != null)
                    {
                        cols.Add(grid[i][j], false);
                    }
                }
            }
            TileMap();
        }

        void TileMap()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (CheckWallOnly(grid[i][j]))
                    {
                        Wall w = (Wall)grid[i][j];
                        w.SetStaticPos(CheckTile(j, i));
                    }
                }
            }
        }

        bool CheckWallOnly(Collideable w)
        {
            return (w is Wall && !(w is RockWall));
        }

        Point CheckTile(int x, int y)
        {
            int val = 0;
            Point retPoint = new Point(0,0);


            if (y == 0)
            {
                val += 1;
                val += 2;
                val += 4;
                if (x == 0)
                {
                    val += 128;
                    val += 64;
                    //MidRight
                    if (CheckWallOnly(grid[y][x + 1]))
                    {
                        val += 8;
                    }
                    //BotMid
                    if (CheckWallOnly(grid[y+1][x]))
                    {
                        val += 32;
                    }
                    //BotRight
                    if (CheckWallOnly(grid[y + 1][x+1]))
                    {
                        val += 16;
                    }
                }
                else if (x == WIDTH - 1)
                {
                    val += 8;
                    val += 16;
                    //MidLeft
                    if (CheckWallOnly(grid[y][x-1]))
                    {
                        val += 128;
                    }
                    //BotLeft
                    if (CheckWallOnly(grid[y + 1][x-1]))
                    {
                        val += 64;
                    }
                    //BotMid
                    if (CheckWallOnly(grid[y + 1][x]))
                    {
                        val += 32;
                    }
                }
                else
                {
                    //MidLeft
                    if (CheckWallOnly(grid[y][x-1]))
                    {
                        val += 128;
                    }
                    //MidRight
                    if (CheckWallOnly(grid[y][x+1]))
                    {
                        val += 8;
                    }
                    //BotLeft
                    if (CheckWallOnly(grid[y + 1][x-1]))
                    {
                        val += 64;
                    }
                    //BotMid
                    if (CheckWallOnly(grid[y + 1][x]))
                    {
                        val += 32;
                    }
                    //BotRight
                    if (CheckWallOnly(grid[y + 1][x+1]))
                    {
                        val += 16;
                    }
                }
            }
            else if (y == HEIGHT - 1)
            {
                val += 16;
                val += 32;
                val += 64;
                if (x == 0)
                {
                    val += 128;
                    val += 1;
                    //TopMid
                    if (CheckWallOnly(grid[y - 1][x]))
                    {
                        val += 2;
                    }
                    //TopRight
                    if (CheckWallOnly(grid[y - 1][x +1]))
                    {
                        val += 4;
                    }
                    //MidRight
                    if (CheckWallOnly(grid[y][x + 1]))
                    {
                        val += 8;
                    }
                }
                else if (x == WIDTH - 1)
                {
                    val += 8;
                    val += 4;
                    //TopLeft
                    if (CheckWallOnly(grid[y - 1][x - 1]))
                    {
                        val += 1;
                    }
                    //TopMid
                    if (CheckWallOnly(grid[y - 1][x]))
                    {
                        val += 2;
                    }
                    //MidLeft
                    if (CheckWallOnly(grid[y][x- 1]))
                    {
                        val += 128;
                    }
                }
                else
                {
                    //TopLeft
                    if (CheckWallOnly(grid[y - 1][x - 1]))
                    {
                        val += 1;
                    }
                    //TopMid
                    if (CheckWallOnly(grid[y - 1][x]))
                    {
                        val += 2;
                    }
                    //TopRight
                    if (CheckWallOnly(grid[y - 1][x+1]))
                    {
                        val += 4;
                    }
                    //MidLeft
                    if (CheckWallOnly(grid[y][x-1]))
                    {
                        val += 128;
                    }
                    //MidRight
                    if (CheckWallOnly(grid[y][x+1]))
                    {
                        val += 8;
                    }
                }
            }
            else
            {
                if (x == 0)
                {
                    val += 1;
                    val += 64;
                    val += 128;
                    //TopMid
                    if (CheckWallOnly(grid[y - 1][x]))
                    {
                        val += 2;
                    }
                    //TopRight
                    if (CheckWallOnly(grid[y - 1][x+1]))
                    {
                        val += 4;
                    }
                    //MidRight
                    if (CheckWallOnly(grid[y][x+1]))
                    {
                        val += 8;
                    }
                    //BotMid
                    if (CheckWallOnly(grid[y + 1][x]))
                    {
                        val += 32;
                    }
                    //BotRight
                    if (CheckWallOnly(grid[y + 1][x+1]))
                    {
                        val += 16;
                    }
                }
                else if (x == WIDTH - 1)
                {
                    val += 4;
                    val += 8;
                    val += 16;
                    //TopLeft
                    if (CheckWallOnly(grid[y -1][x-1]))
                    {
                        val += 1;
                    }
                    //TopMid
                    if (CheckWallOnly(grid[y - 1][x]))
                    {
                        val += 2;
                    }
                    //MidLeft
                    if (CheckWallOnly(grid[y][x -1 ]))
                    {
                        val += 128;
                    }
                    //BotLeft
                    if (CheckWallOnly(grid[y + 1][x - 1]))
                    {
                        val += 64;
                    }
                    //BotMid
                    if (CheckWallOnly(grid[y + 1][x]))
                    {
                        val += 32;
                    }
                }
                else
                {
                    //TopLeft
                    if (CheckWallOnly(grid[y - 1][x - 1]))
                    {
                        val += 1;
                    }
                    //TopMid
                    if (CheckWallOnly(grid[y - 1][x]))
                    {
                        val += 2;
                    }
                    //TopRight
                    if (CheckWallOnly(grid[y - 1][x+1]))
                    {
                        val += 4;
                    }
                    //MidLeft
                    if (CheckWallOnly(grid[y][x - 1]))
                    {
                        val += 128;
                    }
                    //MidRight
                    if (CheckWallOnly(grid[y][x + 1]))
                    {
                        val += 8;
                    }
                    //BotLeft
                    if (CheckWallOnly(grid[y + 1][x-1]))
                    {
                        val += 64;
                    }
                    //BotMid
                    if (CheckWallOnly(grid[y + 1][x]))
                    {
                        val += 32;
                    }
                    //BotRight
                    if (CheckWallOnly(grid[y + 1][x+ 1]))
                    {
                        val += 16;
                    }
                }
            }
            

            foreach (KeyValuePair<Point, int[]> pair in tileMapper)
            {
                if (pair.Value.Contains(val))
                {
                    retPoint = pair.Key;
                }
            }
            return retPoint;
        }

        void AssignMap(int x, int y, char c)
        {
            Vector2 pos = new Vector2(x * 32 + xOff, y * 32 + yOff);
            switch (c)
            {
                case 'w':
                    grid[y][x] = new Wall(wall, pos, 32, 32);
                    break;
                case 'r':
                    grid[y][x] = new RockWall(rockWall, pos, 32, 32);
                    break;
                case 'p':
                    grid[y][x] = new Player(player, pos, 32, 32);
                    break;
                default:
                    grid[y][x] = null;
                    break;
            }
        }

        public List<Collideable> ClosestTo(Vector2 pos)
        {
            List<Collideable> closest = new List<Collideable>();
            int x = (int)((pos.X-xOff) / 32);
            int y = (int)((pos.Y-yOff) / 32);

            if (x >= 0 && x <= WIDTH-1 &&
                y >= 0 && y <= HEIGHT-1)
            {
                closest.Add(grid[y][x]);
                if (x < WIDTH - 1)
                {
                    closest.Add(grid[y][x + 1]);
                }
                if (y < HEIGHT - 1)
                {
                    closest.Add(grid[y + 1][x]);
                    if (x < WIDTH - 1)
                    {
                        closest.Add(grid[y + 1][x + 1]);
                    }
                }
            }

            for (int i = 0; i < closest.Count; i++)
            {
                while (i < closest.Count && closest[i] == null)
                {
                    closest.RemoveAt(i);
                }
            }

            return closest;
        }

        public void Update()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != null && !grid[i][j].Alive)
                    {
                        grid[i][j] = null;
                    }
                }
            }
        }
    }
}
