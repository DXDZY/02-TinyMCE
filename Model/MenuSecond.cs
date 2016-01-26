using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MenuSecond
    {
        private string _secondMenuPower;
        public string secondMenuPower
        {
            get { return this._secondMenuPower; }
            set { this._secondMenuPower = value; }
        }
        private string _secondMenuLevelName;
        public string secondMenuLevelName
        {
            get { return this._secondMenuLevelName; }
            set { this._secondMenuLevelName = value; }
        }
        private string _secondMenuName;
        public string secondMenuName
        {
            get { return this._secondMenuName; }
            set { this._secondMenuName = value; }
        }
        private string _secondMenuNameUrl;
        public string secondMenuNameUrl
        {
            get { return this._secondMenuNameUrl; }
            set { this._secondMenuNameUrl = value; }
        }
        private int _freezeMenu = 0;
        public int freezeMenu
        {
            get { return this._freezeMenu; }
            set { this._freezeMenu = value; }
        }
        //private string _secondMenuParentID;
        //public string secondMenuParentID
        //{
        //    get { return this._secondMenuParentID; }
        //    set { this._secondMenuParentID = value; }
        //}
    }
}
