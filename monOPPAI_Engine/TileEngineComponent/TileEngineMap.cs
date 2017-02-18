using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace monOPPAI_Engine.TileEngineComponent
{
    public class TileEngineMap
    {
        #region Fields & Properties Region

        public Boolean ReadyToLoad = false; // Flag, if Map operations should proceed

        String MapName; // Map Name

        static int TileWidth; // The Width of a single Tile
        static int TileHeight; // The Height of a single Tile
        public static int TILEWIDTH
        {
            get { return TileWidth; }
            set { TileWidth = (int)MathHelper.Clamp(value, 16, 256); }
        }
        public static int TILEHEIGHT
        {
            get { return TileHeight; }
            set { TileHeight = (int)MathHelper.Clamp(value, 16, 256); }
        }

        int TotalMapWidth; // The Width of the entire Map
        int TotalMapHeight; // The Height of the entire Map
        public int MAPWIDTH
        {
            get { return TotalMapWidth; }
        }
        public int MAPHEIGHT
        {
            get { return TotalMapHeight; }
        }

        int NumOfTilesX; // The Number of Tiles in the x-axis
        int NumOfTilesY; // The Number of Tiles in the y-axis
        public int TILE_X
        {
            get { return NumOfTilesX; }
        }
        public int TILE_Y
        {
            get { return NumOfTilesY; }
        }

        List<List<int>> MapData; //A list of lists of integers representing the Map itself
        List<int> UnpassableTiles; //Stores the TileID of tiles which are unpassable by the player.
        List<Rectangle> CollisionTiles; //A list of rectangles, where collision detection is calculated/processed
        Dictionary<int, Texture2D> TileTextures; // Will store the textures used by the Map.  Is identified by an integer ID
        List<Rectangle> TreasureLocationRects; // A list of rectangles where the treasures will be located on the map
        List<Rectangle> EnemyLocationRects; // A list of rectangles where the enemies will be located on the map

        Vector2 StartPosition, EndPosition; // Coordinates of the Map's Start and End Positions

        #endregion

        #region Constructor
        public TileEngineMap()
        {
            //Setting the variables to empty
            //Instantiating objects
            MapName = String.Empty;

            MapData = new List<List<int>>();
            CollisionTiles = new List<Rectangle>();
            TileTextures = new Dictionary<int, Texture2D>();
            UnpassableTiles = new List<int>();

            TreasureLocationRects = new List<Rectangle>();
            EnemyLocationRects = new List<Rectangle>();

            TotalMapWidth = TotalMapHeight = TileWidth = TileHeight = -1;

            StartPosition = EndPosition = Vector2.Zero;

            ReadyToLoad = true;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content">ContentManager object for loading Content</param>
        /// <param name="TileMapPaths">A KeyValuePair containg the paths of the Map and its xml Settings file</param>
        public void LoadMapResources(ContentManager Content, KeyValuePair<String, String> TileMapPaths)
        {
            //Throw exception if the files do not exist
            if (!File.Exists(TileMapPaths.Key) || !File.Exists(TileMapPaths.Value))
                throw new FileNotFoundException();

            LoadMapResources(Content, TileMapPaths.Key); // Loading the MapResources(Tile Definitions etc...)
            LoadMapData(TileMapPaths.Value); //

            GetUnpassableTiles(TileMapPaths.Key);
            StartPosition = GetMapStartPosition(TileMapPaths.Key);
            EndPosition = GetMapEndPosition(TileMapPaths.Key);

            return;
        }
        private void LoadMapResources(ContentManager Content, String TileMapTextureXMLPath)
        {
            #region Test Error Catching
            if (!ReadyToLoad)
            {
                try
                {
                    throw new Exception("Textures already loaded...");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return;
                }
            }
            #endregion


            foreach (KeyValuePair<int, String> TileTexture in GetTileData(TileMapTextureXMLPath))
            {
                TileTextures.Add(TileTexture.Key,
                                 Content.Load<Texture2D>(@TileTexture.Value));
            }
        }
        /// <summary>
        /// Parses data from a specified *.map file.
        /// </summary>
        /// <param name="MapDataFilePath">Path to a .map file</param>
        private void LoadMapData(String MapDataFilePath)
        {
            if (!File.Exists(MapDataFilePath))
                throw new FileNotFoundException();


            using (StreamReader sRead = new StreamReader(MapDataFilePath))
            {
                Boolean readMapName = false;
                Boolean readTMW = false;
                Boolean readTMH = false;
                Boolean readMapData = false;

                #region Parsing File
                while (!sRead.EndOfStream)
                {
                    String CurrentReadLine = sRead.ReadLine();

                    if (CurrentReadLine.Trim().Length < 1 ||
                        CurrentReadLine.Trim()[0] == '!')
                        continue;

                    switch (CurrentReadLine)
                    {
                        case "#MAPNAME":

                            readMapName = true;

                            continue;

                        case "#MAPTILEWIDTH":

                            readTMW = true;

                            continue;

                        case "#MAPTILEHEIGHT":

                            readTMH = true;

                            continue;

                        case "#MAPDATA_START":

                            readMapData = true;

                            continue;

                        case "#MAPDATA_END":

                            NumOfTilesX = MapData[0].Count;
                            NumOfTilesY = MapData.Count;

                            TotalMapWidth = TILEWIDTH * TILE_X;
                            TotalMapHeight = TILEHEIGHT * TILE_Y;

                            //Console.WriteLine("MapWidth = {0}\nMapHeight = {1}", MAPWIDTH, MAPHEIGHT);

                            readMapData = false;

                            continue;
                    }

                    if (readMapName)
                    {
                        MapName = CurrentReadLine;
                        readMapName = false;
                        continue;
                    }
                    else if (readTMW)
                    {
                        //Console.WriteLine("TMW {0}", CurrentReadLine);
                        TileWidth = Int16.Parse(CurrentReadLine);
                        readTMW = false;
                        continue;
                    }
                    else if (readTMH)
                    {
                        //Console.WriteLine("TMH {0}", CurrentReadLine);
                        TileHeight = Int16.Parse(CurrentReadLine);
                        readTMH = false;
                    }
                    else if (readMapData)
                    {
                        String MapRowLine = CurrentReadLine;

                        //Console.WriteLine(MapRowLine);

                        List<int> SingleRowData = new List<int>();

                        foreach (char ch in MapRowLine)
                        {

                            if (Char.IsDigit(ch))
                                SingleRowData.Add(Int16.Parse(ch.ToString()));
                            else
                            {
                                //Console.WriteLine("{0}", ch);
                                SingleRowData.Add(-1);
                            }
                        }
                        MapData.Add(SingleRowData);

                        continue;
                    }

                }

                #endregion
            }
        }

        /// <summary>
        /// This will effectively clamp the camera within the map
        /// area only.
        /// </summary>
        /// <param name="ScreenWidth">The x resolution of the game</param>
        /// <param name="ScreenHeight">The y resolution of the game</param>
        public void Update(int ScreenWidth, int ScreenHeight)
        {
            Camera2D.CameraPosition.X = MathHelper.Clamp(Camera2D.CameraPosition.X, 0, MAPWIDTH - ScreenWidth);
            Camera2D.CameraPosition.Y = MathHelper.Clamp(Camera2D.CameraPosition.Y, 0, MAPHEIGHT - ScreenHeight);
        }

        /// <summary>
        /// Draws the map unto the screen
        /// </summary>
        /// <param name="spriteBatch">spritebatch object</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < TILE_X; x++)
                for (int y = 0; y < TILE_Y; y++)
                {
                    Vector2 TilePositionRelativeToCamera2D =
                        Camera2D.AdjustForCamera(new Vector2(x * TILEWIDTH, y * TILEHEIGHT));

                    spriteBatch.Draw(TileTextures[MapData[y][x]], // WHY DO WE HAVE TO SWAP THE X AND Y FOR THIS!?!?!
                                        new Rectangle((int)TilePositionRelativeToCamera2D.X,
                                                        (int)TilePositionRelativeToCamera2D.Y,
                                                        TILEWIDTH, TILEHEIGHT),
                                       Color.White);
                }

        }

        //For clarification; the 'TileTextureDefinitionsPath' is the xml file
        //where more concise parameters about the map is stored (i.e. Map01.map -> Map01TitleTextures.xml).
        const String TileTextureXMLRootNode = "MapTileTextures";

        public List<KeyValuePair<int, String>> GetTileData(String TileTextureDefinitionsPath)
        {
            if (!File.Exists(TileTextureDefinitionsPath))
                throw new FileNotFoundException();

            List<KeyValuePair<int, String>> ListOfTileKVP = new List<KeyValuePair<int, string>>();

            
            const String KeyAttribute = "KEY";


            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(TileTextureDefinitionsPath);

            XmlNode RootNode = xDoc.SelectSingleNode(TileTextureXMLRootNode);

            foreach (XmlNode node in RootNode)
            {
                if (node.Name == "Tile")
                    ListOfTileKVP.Add(new KeyValuePair<int, string>(
                                                Int16.Parse(node.Attributes[KeyAttribute].InnerText),
                                                    node.InnerText)
                                     );
            }


            return ListOfTileKVP;
        }
        public Vector2 GetMapStartPosition(String TileTextureDefinitionsPath)
        {
            if (!File.Exists(TileTextureDefinitionsPath))
                throw new FileNotFoundException();

            const String TileTextureStartNode = "MapTileTextures/MapStart";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(TileTextureDefinitionsPath);

            XmlNode StartPos = xDoc.SelectSingleNode(TileTextureStartNode);


            return new Vector2(Int16.Parse(StartPos.Attributes["X"].InnerText) * TILEWIDTH,
                                Int16.Parse(StartPos.Attributes["Y"].InnerText) * TILEHEIGHT);
        }
        public Vector2 GetMapEndPosition(String TileTextureDefinitionsPath)
        {
            if (!File.Exists(TileTextureDefinitionsPath))
                throw new FileNotFoundException();

            const String TileTextureStartNode = "MapTileTextures/MapEnd";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(TileTextureDefinitionsPath);

            XmlNode EndPos = xDoc.SelectSingleNode(TileTextureStartNode);


            return new Vector2(Int16.Parse(EndPos.Attributes["X"].InnerText) * TILEWIDTH,
                                Int16.Parse(EndPos.Attributes["Y"].InnerText) * TILEHEIGHT);
        }
        public void GetUnpassableTiles(String TileTextureDefinitionsPath)
        {
            if (!File.Exists(TileTextureDefinitionsPath))
                throw new FileNotFoundException();

            //const String TileTextureXMLRootNode = "MapTileTextures";
            const String KeyAttribute = "KEY";
            const String KeyPassable = "PASSABLE";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(TileTextureDefinitionsPath);

            XmlNode RootNode = xDoc.SelectSingleNode(TileTextureXMLRootNode);

            foreach (XmlNode node in RootNode)
            {
                if (node.Name == "Tile")
                {
                    if (!Boolean.Parse(node.Attributes[KeyPassable].InnerText))
                    {
                        UnpassableTiles.Add(Int16.Parse(node.Attributes[KeyAttribute].InnerText));
                        continue;
                    }
                    else
                        continue;
                }
            }
        }

        public void GetTreasureLocations(String TileTextureDefinitionsPath)
        {
            //Put code to put the location rectangles of all treasures in the map'
            if (!File.Exists(TileTextureDefinitionsPath))
                throw new FileNotFoundException();
            String TileTextureXMLTreasureLocations = TileTextureXMLRootNode + "/MapTreasures";
            //const String TreasureVectorX = "X";
            //const String TreasureVectorY = "Y";
            //const String TreasureItem = "ITEM";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(TileTextureDefinitionsPath);

            XmlNode TreasureLocationsNode = xDoc.SelectSingleNode(TileTextureXMLTreasureLocations);

            foreach (XmlNode node in TreasureLocationsNode)
            {
                if (node.Name == "Treasure")
                {
                    //TreasureLocationRects
                }
            }
        }

        public void GetEnemySpawnLocations(String TileTextureDefinitionsPath)
        {
            //Put code to put the location rectangles of all Enemies in the map'

            if (!File.Exists(TileTextureDefinitionsPath))
                throw new FileNotFoundException();
            
            //const String TileTextureXMLRootNode = "MapTileTextures";


        }

    }

    //static class Camera2D
    //{
    //    public static Vector2 CameraPosition;

    //    public static Vector2 AdjustForCamera(Vector2 WorldPosition)
    //    {
    //        return new Vector2(WorldPosition.X - CameraPosition.X,
    //                            WorldPosition.Y - CameraPosition.Y);
    //    }
    //}
}
