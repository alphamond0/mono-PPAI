#region Namespace Scope Region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using monOPPAI_Engine.UserInput;
using monOPPAI_Engine.Content;
#endregion


namespace monOPPAI_Engine.UserInput
{
    public class MenuManager
    {
        #region Fields and Properties Region

        String _menuName;
        List<MenuItem> _menuItemList; //List containg all menuItems
               
        Vector2 _menuManagerStartPosition; //Position on screen, where the menu will be drawn.
        SpriteFont _menuFont; //Font used to display menu
        SpriteFont _menuDescriptionFont; // Font used to display menu description
        Boolean _MenuManagerReady; //Boolean flag if MenuManager instance is ready for action

        RunningText _menuDescription; // RunningText Object for MenuItem Description
        Vector2 _menuIncrementValue; // A Vector2 Object specifying the amount of x & y to increment/decrement from item menus
        Vector2 _menuDescriptionDisplayPosition; // Vector2 Object specifying where the _menuDescription will be displayed on screen
        String _SelectedItemDescription; //Stores the currently selected menuitem's description

        Color _MenuItemsColor; // Color of the displayed Menu, selected item will be the inverse of this color

        int _currentlySelectedMenuItem; // index value relative to List _menuItemList, the currently selected menu Item
        const float zeroValue = 0f; // Zero Constant in float
        float maxValue; //maximum allowable value

        Boolean _menuChosen; // flags true if a menu item has already been processed as selected
        Boolean _ifMouseinUse = false; // for future use, currently not in use... hopefully, eventually
        int menuItemPositionPadding; // an integer value concerning the amount of padding(spacing) between menu items being displayed

        String selected_menuItem; //String which will hold the Name of the menu item when it is processed as selected

        Boolean MenuActive; // Boolean Flag if this instance of menu is active or otherwise

        public static Boolean DebugFlag = false;

        /// <summary>
        /// Gets or Sets Menu Active State
        /// </summary>
        public Boolean Active
        {
            get
            {                
                return MenuActive;
            }
            set
            {                
                MenuActive = value;
            }
        }

        /// <summary>
        /// Gets the currently selected menu Item in Vector2
        /// </summary>
        public Vector2 SelectedItemPosition
        {
            get { return _menuItemList.ElementAt(_currentlySelectedMenuItem).MenuItemPosition; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">index relative to MenuItemList</param>
        /// <returns>Vector2 Position of MenuItem</returns>
        public Vector2 ItemPosition(int index)
        {
            return _menuItemList.ElementAt(index).MenuItemPosition;
        }

        public Vector2 MenuStartPosition
        {
            get { return _menuManagerStartPosition; }
        }

        public int GetMMCount
        {
            get { return _menuItemList.Count; }
        }

        public String MenuName
        {
            get { return _menuName; }
        }

        //returns a vector2 dimensions(Width and Height, when the 
        //specified spritefont is used) object of the currently selected menuitem's description
        public Vector2 SelectedItemDimensions(SpriteFont sf)
        {
            return sf.MeasureString(_menuItemList.ElementAt(_currentlySelectedMenuItem).MenuValue);
        } 

        //Getter & Setter for DescriptionFont
        public SpriteFont SetDescriptionFont
        {
            get { return _menuDescriptionFont; }
            set { _menuDescriptionFont = value; }
        }

        /// <summary>
        /// Gets the Menu Color
        /// </summary>
        public Color MenuColor
        {
            get { return _MenuItemsColor; }
        }

        //Gets the menuitem value currently selected
        public String CurrentlySelectedMenuItem()
        {
            return _menuItemList[_currentlySelectedMenuItem].MenuValue;
        }


        #endregion

        #region Constructor Region

        public MenuManager(String MenuName, SpriteFont sf, SpriteFont sf2, Vector2 StartPosition, 
            Vector2 RunTextStartPosition, Boolean DescriptionRunOnOrigin, int ItemPadding, Color MenuItemsColor) // Clean up this code later, add a parameter for Description Vector2 Position later
        {
            //For the time being, using only a highlight texture for selection.
            //Use a pointer in the future, or some animated sprite thing...
            _menuName = MenuName;
            selected_menuItem = String.Empty;
            _menuManagerStartPosition = StartPosition;
            _menuDescriptionDisplayPosition = RunTextStartPosition;
            _menuFont = sf; // make this that it accepts a string name for a font.
            _menuDescriptionFont = sf2; // make this that it accepts a string name for a font.

            _currentlySelectedMenuItem = (int)zeroValue;
            _menuItemList = new List<MenuItem>();
            _MenuManagerReady = false;
            _SelectedItemDescription = String.Empty;
            _menuIncrementValue = Vector2.Zero;
            maxValue = 0f;

            _MenuItemsColor = MenuItemsColor;

            _menuChosen = false;
            _ifMouseinUse = false;
            menuItemPositionPadding = ItemPadding;

            _menuDescription = new RunningText(String.Empty, _menuDescriptionDisplayPosition, _menuDescriptionFont, DescriptionRunOnOrigin, 10f, _MenuItemsColor);

            MenuActive = false;

            if (DebugFlag)
                Console.WriteLine("[MenuManager]Instantiating Menu " + MenuName);
        }

        public MenuManager(String MenuName, SpriteFont sf, SpriteFont sf2, Vector2 StartPosition,
            Vector2 RunTextStartPosition, int ItemPadding, Color MenuItemsColor) // Clean up this code later, add a parameter for Description Vector2 Position later
        {
            //For the time being, using only a highlight texture for selection.
            //Use a pointer in the future, or some animated sprite thing...
            _menuName = MenuName;
            selected_menuItem = String.Empty;
            _menuManagerStartPosition = StartPosition;
            _menuDescriptionDisplayPosition = RunTextStartPosition;
            _menuFont = sf; // make this that it accepts a string name for a font.
            _menuDescriptionFont = sf2; // make this that it accepts a string name for a font.

            _currentlySelectedMenuItem = (int)zeroValue;
            _menuItemList = new List<MenuItem>();
            _MenuManagerReady = false;
            _SelectedItemDescription = String.Empty;
            _menuIncrementValue = Vector2.Zero;
            maxValue = 0f;

            _MenuItemsColor = MenuItemsColor;

            _menuChosen = false;
            _ifMouseinUse = false;
            menuItemPositionPadding = ItemPadding;

            _menuDescription = new RunningText(String.Empty, _menuDescriptionDisplayPosition, _menuDescriptionFont, true, 10f, _MenuItemsColor);

            MenuActive = false;

            if (DebugFlag)
                Console.WriteLine("[MenuManager]Instantiating Menu " + MenuName);
        }

        #endregion

        #region XNA Methods Region

        public void Initialize() 
        {
            _currentlySelectedMenuItem = (int)zeroValue;
            _menuChosen = false;
            selected_menuItem = String.Empty;
        }

        
        public void LoadContent(ContentManager Content)
        {
            //this._Content = new ContentManager(Content.ServiceProvider, "Content");
            //_HighlightTexture = this._Content.Load<Texture2D>(@"MainStartScreenTextures\wPixel"); //FUCKING DOUCHE!
            selected_menuItem = String.Empty;
            _menuChosen = false;
            _currentlySelectedMenuItem = 0;
            
            _SelectedItemDescription = _menuItemList[_currentlySelectedMenuItem].MenuDescription;
            _menuDescription.Initialize(_SelectedItemDescription);
        }        

        public void Update(GameTime gameTime) 
        {
            if (_MenuManagerReady)
            {
                // if mouse is needed
                if (_ifMouseinUse)
                {
                    foreach (var itm in _menuItemList)
                    {
                        if (itm.MenuItemRectangle.Contains((int)ControlHandler.MouseCursorPosition().X, (int)ControlHandler.MouseCursorPosition().Y))                        
                            _currentlySelectedMenuItem = _menuItemList.IndexOf(itm);
                    }
                }
                
                _menuDescription.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            if (_MenuManagerReady)
            {
                foreach (MenuItem item in _menuItemList)
                {
                    if (item.MenuItemNumber.Equals(_currentlySelectedMenuItem) && MenuActive)
                        spriteBatch.DrawString(_menuFont, item.MenuValue, item.MenuItemPosition, _MenuItemsColor);
                    else
                        spriteBatch.DrawString(_menuFont, item.MenuValue, item.MenuItemPosition, InverseColor(_MenuItemsColor));
                    
                    
                    // Code below is for highlighting selected menu using a texture
                    //if (item.MenuItemNumber.Equals(_currentlySelectedMenuItem))
                    //{
                    //    if(!_menuChosen)
                    //        spriteBatch.Draw(_HighlightTexture, item.MenuItemRectangle, _HighLightColor * 0.50f);
                    //    else
                    //        spriteBatch.Draw(_HighlightTexture, item.MenuItemRectangle, InverseColor(_HighLightColor) * 0.50f);
                    //}
                }

                _menuDescription.Draw(spriteBatch);
            }
        }

        public int SelectedIndex
        {
            get { return _currentlySelectedMenuItem; }
        }

        // To Add Sound events
        //Edit* Sound events added outside of class
        public void moveSelectedUp()
        {
            _currentlySelectedMenuItem--;

            if ((float)_currentlySelectedMenuItem < zeroValue)            
                _currentlySelectedMenuItem = (int)maxValue;            
            else            
                _currentlySelectedMenuItem = (int)MathHelper.Clamp((float)_currentlySelectedMenuItem, zeroValue, maxValue);

            _SelectedItemDescription = _menuItemList[_currentlySelectedMenuItem].MenuDescription;
            _menuDescription.Initialize(_SelectedItemDescription);
            
        }

        public void moveSelectedDown()
        {
            _currentlySelectedMenuItem++;

            if ((float)_currentlySelectedMenuItem > maxValue)
                _currentlySelectedMenuItem = (int)zeroValue;            
            else            
                _currentlySelectedMenuItem = (int)MathHelper.Clamp((float)_currentlySelectedMenuItem, zeroValue, maxValue);

            _SelectedItemDescription = _menuItemList[_currentlySelectedMenuItem].MenuDescription;
            _menuDescription.Initialize(_SelectedItemDescription);
        }

        public void itemSelected()
        {
            _menuChosen = true;
            selected_menuItem = _menuItemList[_currentlySelectedMenuItem].MenuValue;
        } // will lock the menu

        public Boolean Selected
        {
            get { return _menuChosen; }
        } // true if menu has been locked as already selected


        #endregion

        #region Local Methods

        public void ResetSelected()
        {
            selected_menuItem = String.Empty;
            _currentlySelectedMenuItem = (int)zeroValue;
            _menuChosen = false;
        }

        public String selectedItem
        {
            get { return selected_menuItem; }
        }

        public void AddMenuItem(params MenuItem[] menuItem)
        {
            if (!_MenuManagerReady)
            {
                foreach (MenuItem item in menuItem)
                {
                    item.MenuItemNumber = _menuItemList.Count;
                    _menuItemList.Add(item);
                    positionMenuItems();
                }
                
            }                
        }

        private void positionMenuItems()
        {            
            if (!_MenuManagerReady)
            {
                for (int i = 1 ; i <= _menuItemList.Count ; i++)
                {
                    var x = i - 1;

                    _menuItemList.ElementAt(x).MenuItemPosition = new Vector2(_menuManagerStartPosition.X, 
                        (_menuManagerStartPosition.Y + (x * menuItemPositionPadding)));

                    _menuItemList.ElementAt(x).MenuItemRectangle =
                        new Rectangle(
                            (int)_menuItemList.ElementAt(x).MenuItemPosition.X, 
                            (int)_menuItemList.ElementAt(x).MenuItemPosition.Y,
                            (int)_menuFont.MeasureString(_menuItemList.ElementAt(x).MenuValue).X, 
                            (int)_menuFont.MeasureString(_menuItemList.ElementAt(x).MenuValue).Y
                            );                    
                    
                    _menuIncrementValue = _menuFont.MeasureString(_menuItemList.ElementAt(x).MenuValue);
                }
            }
        }

        private int MenuManagerItemCount()
        {
            return _menuItemList.Count();
        }

        //This method must be called once to flag the manager as 'Ready'
        //Calling it twice will 'Un-ready' the manager though, so be careful.
        public void MenuManagerReadySwitch()
        {
            _MenuManagerReady = !_MenuManagerReady;
            maxValue = (float)MenuManagerItemCount() - 1;
            _SelectedItemDescription = _menuItemList[_currentlySelectedMenuItem].MenuDescription;
            _menuDescription.Initialize(_SelectedItemDescription);
        }

        private Color InverseColor(Color Colour)
        {
            return new Color(Math.Abs(Colour.R - 255), Math.Abs(Colour.G - 255), Math.Abs(Colour.B - 255));
        }

        public Boolean IndexIsFirstLast()
        {
            return _currentlySelectedMenuItem == 0 ||
                _currentlySelectedMenuItem == _menuItemList.Count - 1;
        }

        #endregion
    }
}
