using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class bllContentOperation
    {
        dalContentOperation dco = new dalContentOperation();
        public bool insertContentModel(int menuID, int userID, string contentTitle, string contentText, int submitType)
        {
            return dco.insertContentModel(menuID, userID, contentTitle, contentText, submitType);
        }
        public DataTable getContentModel(int id)
        {
            return dco.getContentModel(id);
        }
        public bool updateContentModel(string contentTitle, string contentText, int submitType, int id)
        {
            return dco.updateContentModel(contentTitle, contentText, submitType, id);
        }
    }
}
