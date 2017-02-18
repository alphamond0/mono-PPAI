#region Namespace Scope Region
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace monOPPAI_Engine.UserInput
{
    public class MenuItem
    {
        #region Fields and Properties Region

        String _mItemValue;
        String _mItemDescription;
        Vector2 _mItemPosition;        
        Rectangle _mItemRectangle;

        int _mItemNumber;

        public String MenuValue
        {
            get { return _mItemValue; }
            set { _mItemValue = value; }
        }

        public String MenuDescription
        {
            get { return _mItemDescription; }
            set { _mItemDescription = value; }
        }

        public Vector2 MenuItemPosition
        {
            get { return _mItemPosition; }
            set
            {
                _mItemPosition.X = value.X;
                _mItemPosition.Y = (int)value.Y;
            }
        }        

        public Rectangle MenuItemRectangle
        {
            get { return _mItemRectangle; }
            set { _mItemRectangle = value; }
        }

        public int MenuItemNumber
        {
            get { return _mItemNumber; }
            set { _mItemNumber = value; }
        }

        #endregion

        #region Constructor Region

        public MenuItem(String itmVal, String itmDesc, Vector2 itmPos, Rectangle itmRect, int itmNum)
        {
            _mItemValue = itmVal;
            _mItemDescription = itmDesc;
            _mItemPosition = itmPos;
            _mItemRectangle = itmRect;
            _mItemNumber = itmNum;
        }

        #endregion

        #region Local Methods

        //sets the origin to the upper right side of the menuitem
        //for use with right text alignment
        public Vector2 getMenuItemOrigin(MenuItem mItem)
        {
            return new Vector2(mItem._mItemRectangle.Width, 0);
        }

        #endregion
    }
}
