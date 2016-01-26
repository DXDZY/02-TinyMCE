using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class menuBase
    {
        private string _menuPower;
        public string menuPower
        {
            get { return this._menuPower; }
            set { this._menuPower = value; }
        }
        private string _menuName;
        public string menuName
        {
            get { return this._menuName; }
            set { this._menuName = value; }
        }
        private string _menuNameUrl;
        public string menuNameUrl
        {
            get { return this._menuNameUrl; }
            set { this._menuNameUrl = value; }
        }
        private string _menuParentID;
        public string menuParentID
        {
            get { return this._menuParentID; }
            set { this._menuParentID = value; }
        }
    }
}
